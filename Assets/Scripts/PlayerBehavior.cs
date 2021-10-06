using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rBody;
    private PlayerInput playerInput;
    [SerializeField] private GameObject modelGO;
    [SerializeField] private float speed;
    [SerializeField] private float lookSensitivity;
    private PlayerControl playerControl;
    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        
        playerControl = new PlayerControl();
        playerControl.Player.Enable();
    }

    private void Update()
    {
        Vector2 inputvector = playerControl.Player.move.ReadValue<Vector2>();
        if(inputvector.x != 0)
        rBody.AddForce( transform.right * (inputvector.x * speed),ForceMode.Force);
        if(inputvector.y != 0)
        rBody.AddForce( transform.forward * (inputvector.y* speed) , ForceMode.Force);

        rBody.rotation *= Quaternion.Euler(new Vector3(0f,
            playerControl.Player.lookaround.ReadValue<float>() * lookSensitivity * Time.deltaTime, 0f));
        Debug.Log(playerControl.Player.lookaround.ReadValue<float>());
        
    }

}

