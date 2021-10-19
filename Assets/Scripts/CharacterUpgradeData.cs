using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class CharacterUpgradeData : SerializedMonoBehaviour
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

    [SerializeField] private Dictionary<string, float> _data;

    public void SetValue(string name, float value)
    {
        if (_data.ContainsKey(name))
            _data[name] = value;
        else
        {
            _data.Add(name,value);
        }
    }

    public float GetValue(string name)
    {
        if (!_data.ContainsKey(name))
        {
            Debug.LogError(name + " : Key does not exist in upgrade data");
            return 0.0f;
        }

        return _data[name];
    }

    public string Debug_DataKeys()
    {
        string s = String.Empty;
        s += "KEYS IN UPGRADE DATA \r\n";
        foreach (var key in _data.Keys)
        {
            s += key + "\r\n";
        }
        s += "END OF COLLECTION";
        
        return s;
    }

    // public float fullHealth = 0f;
    // public float healPerSeconds = 0f;
    // public float fullStamina = 0f;
    // public float staminaPerSeconds = 0f;
    // public float attackMult = 1.0f;
    // public float chargeDistance = 0.0f;
    // public float throwDistance = 0.0f;
    // public float throwMult = 1.0f;

}
