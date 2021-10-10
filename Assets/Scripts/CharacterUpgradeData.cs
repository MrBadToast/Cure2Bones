using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgradeData : MonoBehaviour
{
    private static CharacterUpgradeData _instance;

    public static CharacterUpgradeData Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public float fullHealth = 0f;
    public float healthRgTime = 0f;
    public float healPerSeconds = 0f;
    public float fullStamina = 0f;
    public float staminaRgTime = 0f;
    public float attackMult = 1.0f;
    public float attackRange = 0f;
    public float chargeDistance = 0.0f;
    public float chargeDiameter = 0.0f;
    public float throwDistance = 0.0f;
    public float throwMult = 1.0f;
    
    
}
