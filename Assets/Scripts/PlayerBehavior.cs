using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private float speed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float jumpPower;

    [Space(20)] 
    [SerializeField] private int attackPerSeconds;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackSTMUse;
    [SerializeField] private float maxHP;
    [SerializeField] private float maxSTM;
    [SerializeField] private float regenerationTime_HP;
    [SerializeField] private float regenerationTime_STM;
    [SerializeField] private float charge_Distance;
    [SerializeField] private float charge_Speed;
    [SerializeField] private float charge_STMUse;
    
    
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

    private void Update()
    {
        if (!disableControls)
        {
            Vector2 inputvector = playerControl.Player.move.ReadValue<Vector2>();
            if (inputvector.x != 0)
                rBody.AddForce(transform.right * (inputvector.x * speed), ForceMode.Force);
            if (inputvector.y != 0)
                rBody.AddForce(transform.forward * (inputvector.y * speed), ForceMode.Force);

            rBody.rotation *= Quaternion.Euler(new Vector3(0f,
                playerControl.Player.lookaround.ReadValue<float>() * lookSensitivity * Time.deltaTime, 0f));
        }
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        bool isGrounded = Physics.Raycast(footRCO.position, Vector3.down, 0.1f);
        if (disableControls || !isGrounded) return;

        rBody.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
    }
}

