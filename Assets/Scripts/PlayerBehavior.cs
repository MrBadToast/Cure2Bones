using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehavior : MonoBehaviour
{
    private static PlayerBehavior _instance;

    public static PlayerBehavior Instance => _instance;

    private Rigidbody rBody;
    private PlayerInput playerInput;
    private SoundModule_Base sound;
    private Animator animator;
    private CharacterUpgradeData _upgradeData;
    [SerializeField] private GameObject modelGO;
    [SerializeField] private Transform footRCO;
    [SerializeField] private TargetDamageArea rushDamageArea;
    [SerializeField] private GameObject chargeDamageArea;
    [SerializeField] private GameObject HitEffectObject;
    [SerializeField] private ParticleSystem SpeedLineEffect;
    [SerializeField] private CinemachineCameraOffset cameraOffset;
    [SerializeField] private Transform cameraTarget;
    //[SerializeField] private GameObject effectOnHit;
    [SerializeField] private float speed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float jumpPower;

    [Space(20)] 
    [SerializeField] private float attackInterval;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackSTMUse;
    [SerializeField] private float maxHP;
    [SerializeField] private float maxSTM;
    [SerializeField] private float STMRegenPersec;
    [SerializeField] private float HPRegenPersec;
    //[SerializeField] private float regenerationTime_HP;
    [SerializeField] private float regenerationTime_STM;
    [SerializeField] private float charge_Speed;
    [SerializeField] private float charge_STMUse;
    [SerializeField] private float charge_power;

    private float currentHP;
    private float currentSTM;
    private float stmRegenCooldownTimer;
    private float money;
    private float DamageTimer;
    private float rushHeat;

    private bool isInvincible = false;

    public float MAXHp => maxHP + _upgradeData.GetValue("FULL_HEALTH");
    public float MAXStm => maxSTM + _upgradeData.GetValue("FULL_STAMINA");
    public float HealPerSeconds => HPRegenPersec + _upgradeData.GetValue("HEAL_PER_SECONDS");
    public float StaminaPerSeconds => STMRegenPersec + _upgradeData.GetValue("STAMINA_PER_SECONDS");
    public float Speed => speed + _upgradeData.GetValue("MOVE_SPEED");
    public float CurrentHp => currentHP;
    public float CurrentStm => currentSTM;
    public float Money => money;
    public float AttackPower => attackPower * _upgradeData.GetValue("MULT_ATTACK");
    public float ChargePower => charge_power;

    public enum CharacterState
    {
        DISABLE,
        NORMAL,
        ATTACKRUSH,
        CHARGE,
        GRAPPLE
    }

    private CharacterState state = CharacterState.DISABLE;
    private PlayerControl playerControl;
    private bool disableControls = false;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("플레이어는 하나만 존재할 수 있습니다" + gameObject.name + "이 제거됩니다.");
            Destroy(this);
        }
        
        
        rBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        sound = GetComponent<SoundModule_Base>();
        animator = GetComponent<Animator>();

        playerControl = new PlayerControl();
        playerControl.Player.Enable();
        playerControl.Player.jump.started += Jump;
        playerControl.Player.interact.started += Interact;
    }

    private void Start()
    {
        _upgradeData = CharacterUpgradeData.Instance;
        Debug.Log(_upgradeData.Debug_DataKeys());
        StageManager.Instance.onGameStarted += EnableCharacter;

        currentHP = MAXHp;
        currentSTM = MAXStm;
        stmRegenCooldownTimer = StaminaPerSeconds;
    }

    private float chargeCoolDown = 1.0f;
    private float walksoundTimer = 0.5f;

    private void FixedUpdate()
    {
        var IsAttcking = playerControl.Player.attack.phase == InputActionPhase.Started;
        var IsCharging = playerControl.Player.charge.phase == InputActionPhase.Started;

        chargeCoolDown -= Time.fixedDeltaTime;

        switch (state)
        {
            case CharacterState.DISABLE:
                break;

            case CharacterState.NORMAL:
                Move(Speed);
                if (IsAttcking && currentSTM >= attackSTMUse)
                {
                    state = CharacterState.ATTACKRUSH;
                    break;
                }

                if (IsCharging && chargeCoolDown < 0f && currentSTM >= charge_STMUse)
                {
                    sound.Play("charge");
                    animator.Play("DASH");
                    SpeedLineEffect.Play();

                    state = CharacterState.CHARGE;
                    currentSTM -= charge_STMUse;

                    chargeCoolDown = 1.0f;
                    chargeTimer = 0.5f;
                    stmRegenCooldownTimer = regenerationTime_STM;
                }

                break;

            case CharacterState.ATTACKRUSH:
                if (currentSTM <= attackSTMUse)
                {
                    state = CharacterState.NORMAL;
                    break;
                }

                Move(Speed / 3);
                PunchRush();
                if (!IsAttcking)
                    state = CharacterState.NORMAL;
                break;

            case CharacterState.CHARGE:
                Charge();
                break;

            case CharacterState.GRAPPLE:
                break;

            default:
                break;
        }

        if (state != CharacterState.CHARGE)
        {
            chargeCoolDown -= Time.deltaTime;
        }

        if (state != CharacterState.ATTACKRUSH)
        {
            rushHeat = Mathf.Lerp(rushHeat, 0, 0.2f);
        }

        cameraOffset.m_Offset = new Vector3(0f, 0f, rushHeat / 2);

        if (stmRegenCooldownTimer < 0)
        {
            currentSTM += StaminaPerSeconds * Time.deltaTime;
            if (currentSTM > MAXStm)
                currentSTM = MAXStm;
        }
        else
        {
            stmRegenCooldownTimer -= Time.deltaTime;
        }

        DamageTimer -= Time.deltaTime;
        walksoundTimer -= Time.deltaTime;
    }

    public bool IsHeadingInteractables()
    {
        var cam = Camera.main;
        var val = Physics.RaycastAll(cam.transform.position, cam.transform.forward, 5.0f);

        Debug.DrawLine(cam.transform.position, (cam.transform.position + cam.transform.forward*5.0f),Color.magenta);

        foreach (var v in val)
        {
            if (v.transform.gameObject.layer == 10)
            {
                Debug.Log("K");
                return true;
            }
        }
        

        return false;

    }
    
    public void Move(float _speed)
    {
        Vector2 inputvector = playerControl.Player.move.ReadValue<Vector2>();
        
        if (inputvector.x != 0)
            rBody.AddForce(transform.right * (inputvector.x * _speed));
        if (inputvector.y != 0)
            rBody.AddForce(transform.forward * (inputvector.y * _speed));

        float x_rot = playerControl.Player.lookaround_x.ReadValue<float>() * lookSensitivity;
        
        transform.rotation *= quaternion.Euler(0f,x_rot,0f);

        // float y_rot = playerControl.Player.lookaround_y.ReadValue<float>() * lookSensitivity;
        //
        // cameraTarget.rotation *= quaternion.Euler( -y_rot,0f,0f);

        if (state == CharacterState.NORMAL)
        {
            if (inputvector.x != 0 || inputvector.y != 0)
                animator.Play("WALK");
            else
            {
                animator.Play("IDLE");
            }
        }

        if (walksoundTimer < 0 && (inputvector.x != 0 || inputvector.y != 0))
        {
            if (Physics.Raycast(footRCO.position, Vector3.down, 0.1f))
            {
                sound.Play("walk");
                walksoundTimer = 12f / _speed;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (state == CharacterState.DISABLE) return;
        bool isGrounded = Physics.Raycast(footRCO.position, Vector3.down, 0.1f);
        if (disableControls || !isGrounded) return;

        rBody.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (state == CharacterState.DISABLE) return;
        
        var cam = Camera.main;
        var val = Physics.RaycastAll(cam.transform.position, cam.transform.forward, 5.0f);

        foreach (var v in val)
        {
            if (v.transform.gameObject.layer == 10)
            {
                v.collider.GetComponent<Interactable_Base>().Interact();
            }
        }
    }

    private float punchTimer = 0f;

    private void PunchRush()
    {
        animator.Play("PUNCH");
        
        
        if (punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;
            return;
        }

        currentSTM -= attackSTMUse;
        var targets = rushDamageArea.GetTargetsInReach();
        rushHeat = Mathf.Lerp(rushHeat, 1, 0.05f);

        if (targets.Count > 0)
        {
            var hit =Instantiate(HitEffectObject, transform.position + transform.forward*2f, quaternion.LookRotation(-transform.forward,Vector3.up));
            hit.GetComponent<SoundModule_Base>().Play("hit");
        }
        foreach (var t in targets)
        {
            if (!t) break;
            var h = new HitData((t.transform.position - transform.position).normalized,20.0f,AttackPower,0.25f);
           t.OnHit(h);
        }
        
        sound.Play("whoosh");
        punchTimer = attackInterval;
        stmRegenCooldownTimer = regenerationTime_STM;
    }

    private float chargeTimer = 0f;
    
    private void Charge()
    {
        if (chargeTimer > 0)
        {
            chargeDamageArea.SetActive(true);
            isInvincible = true;
            rBody.velocity = transform.forward * charge_Speed;
            chargeTimer -= Time.deltaTime;
        }
        else
        {
            chargeDamageArea.SetActive(false);
            isInvincible = false;
            state = CharacterState.NORMAL;
        }
    }
    
    public bool IsTargetInRange()
    {
        return rushDamageArea.GetTargetsInReach().Count != 0;
    }

    public void SetUpgrade(string id, float value)
    {
        
    }
    public void GetMoney(int value)
    {
        money += value;
    }
    
    public bool UseMoney(int value)
    {
        if (value < money)
        {
            money -= value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetDamage(float amount)
    {
        if (DamageTimer > 0 || isInvincible ) return;
        
        if (currentHP < amount)
        {
            currentHP = 0f;
            OnHealthDepleted();
            return;
        }

        currentHP -= amount;
        DamageTimer = 1.0f;
    }

    public void OnHealthDepleted()
    {
        state = CharacterState.DISABLE;
        StageManager.Instance.GameOver();
    }
    
    public void EnableCharacter()
    {
        state = CharacterState.NORMAL;
    }

    public void DisablePlayer()
    {
        state = CharacterState.DISABLE;
    }

    public void EnablePlayer()
    {
        state = CharacterState.NORMAL;
    }
    
}

