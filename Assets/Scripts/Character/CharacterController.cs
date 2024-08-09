using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //캐릭터 기본 정보
    [SerializeField] private CharClass position;
    
    [Space]
    [SerializeField] private CharStatus charStatus;
    
    [Space]
    [SerializeField] private int blockableNumber;
    [SerializeField] private int requiredCost;
    [SerializeField] private float locatedCoolTime;
    private float curLocatedCoolTime;
    
    private CharState curState = CharState.Idle;

    public bool IsLoctable { get; private set; }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        IsLoctable = false;
        ChangeState(CharState.Idle);
    }

    public void ChangeState(CharState newState)
    {
        curState = newState;
    }

    private void Attack()
    {
        
    }

    private void UseSkill()
    {
        
    }

    private void Die()
    {
        
    }
}
