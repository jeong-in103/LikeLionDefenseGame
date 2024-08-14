using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharStatus", menuName = "Scriptable Object/CharStatus")]
public class CharStatus : ScriptableObject
{
    [field: SerializeField] public Sprite CharProfileImage { get; private set; }
    
    [field: SerializeField] public CharClass Class { get; private set; }
    [field: SerializeField] public AutoAttackType AutoAttackType { get; private set; }
    
    // 초기값
    public float maxHp;
    public float maxSp;
    public float atk;
    public float def;
    public float atkSpeed;
    
    [Space]
    public int blockAbleNumber;       // 저지 가능한 몹 수
    public int requiredCost;          // 배치 시 필요 코스트
    public float locatedCoolTime;     // 재배치 쿨타임

    /*
    private float damage;
    
    private float calculateDamage()
    {
        return damage;
    }
    */
}
