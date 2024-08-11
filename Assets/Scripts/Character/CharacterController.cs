using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// 캐릭터 배치 플로우 : 위치 -> 방향
public class CharacterController : MonoBehaviour
{
    //캐릭터 기본 정보
    [SerializeField] private CharClass @class;
    
    [Space]
    [SerializeField] private CharStatus charStatus;
    
    [Space]
    [SerializeField] private int blockAbleNumber;       // 저지 가능한 몹 수
    [SerializeField] private int requiredCost;          // 배치 시 필요 코스트
    [SerializeField] private float locatedCoolTime;     // 재배치 쿨타임
    
    // 현재 캐릭터 상태
    private CharState curState = CharState.Idle;
    
    private float curLocatedCoolTime;
    
    // 캐릭터 위치 설정 관련 변수
    private Vector3 nearPos;
    
    private bool isLocating = false;
    private bool isCanLocated = false;
    public bool IsLocated { get; private set; }
    
    // 캐싱
    Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isLocating)
            UpdateCharPosToCursor(gameObject);
    }
    
    public void ChangeState(CharState newState)
    {
        curState = newState;
    }
    
    // 캐릭터 배치 플로우 시작 함수
    public void StartCharacterPlaceInStage()
    {
        IsLocated = false;
        ChangeState(CharState.Idle);
        StartLocatedAtPos();
    }

    // 캐릭터 위치 설정 시작
    public void StartLocatedAtPos()
    {
        // 캐릭터가 마우스 커서 chase
        isLocating = true;
        UpdateCharPosToCursor(gameObject);
        
        // 위치 설정 UI On
        AstarGrid.Instance.SetActiveNodes(charStatus.AutoAttackType, true);
        
        // 시간 느리게
        
    }
    
    // 캐릭터 위치 설정 종료
    public void LocatedAtPos()
    {
        isLocating = false;
        // 위치 설정 UI Off
        AstarGrid.Instance.SetActiveNodes(charStatus.AutoAttackType, false);

        if (!isCanLocated)
        {
            gameObject.SetActive(false);
        }
        else
        {
            IsLocated = true;
        
            StartSetDirection();
        }
    }

    // 캐릭터 방향 설정 시작
    private void StartSetDirection()
    {
        // 방향 설정 UI On
        
    }
    
    // 캐릭터 방향 설정 종료
    private void SetDirection()
    {
        // 방향 설정 UI Off
    }
    
    
    // obj가 마우스 커서를 쫒는 함수
    private void UpdateCharPosToCursor(GameObject obj)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f));

        if (groundPlane.Raycast(ray, out var distance))
        {
            Vector3 pos = ray.GetPoint(distance);
            pos.z = -0.2f;
            obj.transform.position = pos;
        }
    }

    private void Attack()
    {
        
    }

    private void UseSkill()
    {
        
    }

    private void Die()
    {
        IsLocated = false;
    }
}
