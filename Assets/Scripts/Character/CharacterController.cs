using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 캐릭터 배치 플로우 : 위치 -> 방향
public class CharacterController : MonoBehaviour
{
    private CharacterManager characterManager;

    // 캐릭터 배치 관련 변수
    private bool isLocating = false;
    private bool isCanLocated = false;
    private bool isLocated = false;

    private bool isRotating = false;
    private bool isRotated = false;
    
    // 캐싱
    private Camera mainCamera;
    
    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        mainCamera = Camera.main;
        
        characterManager.OnCharacterLocated.AddListener(StartCharacterPlaceInStage);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isLocating && !isLocated)
            {
                UpdateCharPosToCursor(gameObject);
            }
            
            if (isRotating && !isRotated)
            {
                UpdateCharPosToCursor(characterManager.UIManager.DirCursor, 2f);
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (isLocating)
            {
                LocateAtPos();
                
            }
            else if (isRotating)
            {
                SetDirection();
            }
        }
    }
    
    // 캐릭터 배치 플로우 시작 함수
    private void StartCharacterPlaceInStage()
    {
        characterManager.ChangeState(CharState.Wait);
        StartLocateAtPos();
    }

    // 캐릭터 위치 설정 시작
    private void StartLocateAtPos()
    {
        isLocating = true;
        UpdateCharPosToCursor(gameObject);
        
        // Node On
        AstarGrid.Instance.SetActiveNodesByType(characterManager.CharStatus.AutoAttackType, true);
        
        // 시간 느리게
        // ~
    }
    
    // 캐릭터 위치 설정 종료
    private void LocateAtPos()
    {
        isLocating = false;
        
        // Node Off
        AstarGrid.Instance.SetActiveNodesByType(characterManager.CharStatus.AutoAttackType, false);
        
        if (!isCanLocated)
        {
            gameObject.SetActive(false);
        }
        else
        {
            isLocated = true;
            
            StartSetDirection();
        }
    }

    // 캐릭터 방향 설정 시작
    private void StartSetDirection()
    {
        isRotating = true;
        
        // 방향 설정 UI On
        characterManager.UIManager.SetActiveStatusCanvas(true);
        characterManager.UIManager.SetActiveDirSetCanvas(true);
    }
    
    // 캐릭터 방향 설정 종료
    private void SetDirection()
    {
        isRotating = false;
        
        // 방향 설정 UI Off
        characterManager.UIManager.SetActiveDirSetCanvas(false);
        // status UI On
        characterManager.UIManager.SetActiveStatusBars(true);
  
        // temp code
        //isRotated = true;

        // 시간 원래대로
        // ~

        // 소환 애니메이션
        //animController.SetTrigger("Start");
    }
    
    
    // 마우스 커서를 쫒는 함수
    private void UpdateCharPosToCursor(GameObject obj)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f));

        if (groundPlane.Raycast(ray, out var distance))
        {
            Vector3 pos = ray.GetPoint(distance);
            
            Vector3 chaPos = JudgeCharInValidLocationPos(pos);
            chaPos.z = (characterManager.CharStatus.AutoAttackType == AutoAttackType.Melee)? 0.0f : -0.2f;
            
            obj.transform.position = chaPos;
        }
    }
    
    // 마우스 커서를 쫒는 함수 - 일정 거리 이상 움직이면 움직이지 않도록
    private void UpdateCharPosToCursor(GameObject obj, float length)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f));

        if (groundPlane.Raycast(ray, out var distance))
        {
            Vector3 pos = ray.GetPoint(distance);
            pos.z = 0f;

            Vector3 originPos = transform.position;
            float tempLen = Vector2.Distance(originPos, pos);
            if (tempLen <= length)
            {
                obj.transform.position = pos;
            }
            else
            {
                Vector3 direction = (pos - originPos).normalized;  // 방향 벡터 계산
                obj.transform.position = originPos + direction * length;  
            }
        }
    }
    
    // 마우스 커서가 배치 가능한 구역 가까이에 있는지 확인
    private Vector3 JudgeCharInValidLocationPos(Vector3 mousePos)
    {
        List<Node> validLocationNode = AstarGrid.Instance.GetNodesByType(characterManager.CharStatus.AutoAttackType);

        foreach (Node node in validLocationNode)
        {
            float distance = Vector2.Distance(mousePos, node.WorldPos);
            if (distance < 0.5f)
            {
                isCanLocated = true;
                return node.WorldPos;
            }
        }

        isCanLocated = false;
        return mousePos;
    }
}
