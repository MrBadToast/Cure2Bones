using UnityEngine;

public enum AttackType
{
    NORMAL,
    HEAT,
    FREEZE,
    ELECTRIC
}

public class HitData
{
    public Vector3 _direction = Vector3.zero;
    public float _power = 0f;
    public float _dealtDamage = 0f;
    public float _stiffTime = 0f;
    public AttackType _attackType = AttackType.NORMAL;
    

    public HitData(Vector3 direction, float power, float dealtDamage,float stiffTime, AttackType atkType = AttackType.NORMAL)
    {
        _direction = direction; _power = power; _dealtDamage = dealtDamage;
        _stiffTime = stiffTime;
        _attackType = atkType;
    }
}
