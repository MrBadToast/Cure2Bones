using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject modelGO;
    [SerializeField] private Transform footRCO;
    [SerializeField] private PunchArea punchArea;
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
    [SerializeField] private float regenerationTime_HP;
    [SerializeField] private float regenerationTime_STM;
    [SerializeField] private float charge_Distance;
    [SerializeField] private float charge_Speed;
    [SerializeField] private float charge_STMUse;

    private float currentHP;
    private float currentSTM;

    public float MAXHp => maxHP;
    public float MAXStm => maxSTM;
    public float CurrentHp => currentHP;
    public float CurrentStm => currentSTM;


    public enum CharacterState
    {
        DISABLE,
        NORMAL,
        ATTACKRUSH,
        CHARGE,
        GRAPPLE
    }

    public CharacterState state = CharacterState.NORMAL;
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
        
        playerControl = new PlayerControl();
        playerControl.Player.Enable();
        playerControl.Player.jump.started += Jump;
    }

    private float chargeCoolDown = 1.0f;
    
    private void Update()
    {
        var IsAttcking = playerControl.Player.attack.phase == InputActionPhase.Started;
        var IsCharging = playerControl.Player.charge.phase == InputActionPhase.Started;

        chargeCoolDown -= Time.time;
        
        switch (state)
        {
            case CharacterState.DISABLE:
                break;

            case CharacterState.NORMAL:
                Move(speed);
                if (IsAttcking)
                {
                    state = CharacterState.ATTACKRUSH;
                    break;
                }

                if (IsCharging && chargeCoolDown < 0f)
                {
                    state = CharacterState.CHARGE;
                    chargeCoolDown = 1.0f;
                }
                break;
            
            case CharacterState.ATTACKRUSH:
                Move(speed/2);
                PunchRush();
                if (!IsAttcking)
                    state = CharacterState.NORMAL;
                break;
            
            case CharacterState.CHARGE:
                
                break;
            
            case CharacterState.GRAPPLE:
                break;
            
            default:
                break;
        }
    }

    public void Move(float _speed)
    {
        Vector2 inputvector = playerControl.Player.move.ReadValue<Vector2>();
        
        if (inputvector.x != 0)
            rBody.AddForce(transform.right * (inputvector.x * _speed), ForceMode.Force);
        if (inputvector.y != 0)
            rBody.AddForce(transform.forward * (inputvector.y * _speed), ForceMode.Force);

        float x_rot = playerControl.Player.lookaround_x.ReadValue<float>() * lookSensitivity * Time.deltaTime;
        //float y_rot = 
        
        Quaternion rot = Quaternion.Euler(new Vector3(0f, x_rot, 0f));

        rBody.rotation *= rot;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        bool isGrounded = Physics.Raycast(footRCO.position, Vector3.down, 0.1f);
        if (disableControls || !isGrounded) return;

        rBody.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
    }

    private float punchTimer = 0f;

    private void PunchRush()
    {
        
        if (punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;
            return;
        }
        
        Debug.Log("Punch");
        
        var targets = punchArea.GetTargetsInReach();
        foreach (var t in targets)
        {
           // Instantiate(effectOnHit, t.transform.position, quaternion.identity);
            t.OnHit();
        }

        punchTimer = attackInterval;
    }

    public bool IsTargetInRange()
    {
        return punchArea.GetTargetsInReach().Count != 0;
    }
    
}

