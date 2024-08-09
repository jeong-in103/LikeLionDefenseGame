using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharStatus 
{
    // 초기값
    [SerializeField] private float maxHp;
    [SerializeField] private float maxSp;
    [SerializeField] private float atk;
    [SerializeField] private float def;
    [SerializeField] private float atkSpeed;
    
    //현재값
    private float curHp;
    public float HP
    {
        get => curHp;
        set => curHp = Mathf.Clamp(value, 0f, maxHp);
    }
    
    private float curSp;
    public float SP
    {
        get => curSp;
        set => curSp = Mathf.Clamp(value, 0f, maxSp);
    }
    
    private float curAtk;
    public float ATK
    {
        get => curAtk;
        set => curAtk = value;
    }
    
    private float curDef;
    public float DEF
    {
        get => curDef;
        set => curDef = value;
    }

    private float curAtkSpeed;
    public float AtkSpeed
    {
        get => curAtkSpeed;
        set => curAtkSpeed = value;
    }

    private float damage;

    private float calculateDamage()
    {
        return damage;
    }
}
