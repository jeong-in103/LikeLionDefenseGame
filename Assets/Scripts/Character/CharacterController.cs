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
    private bool isCanRotated = false;
    private bool isRotated = false;

    private ArrowDir charDir = ArrowDir.Right;
    
    // 캐싱
    private Camera mainCamera;
    
    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        mainCamera = Camera.main;

        characterManager.OnCharacterLocated.AddListener(Init);
        characterManager.OnCharacterLocated.AddListener(StartCharacterPlaceInStage);
    }

    private void Init()
    {
        isLocating = false;
        isCanLocated = false;
        isLocated = false;
        isRotating = false;
        isCanRotated = false;
        isRotated = false;
    }

    private void Update()
    {
        if (isLocating)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isLocated)
                {
                    UpdateCharPosToCursor(gameObject);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                LocateAtPos();
            }
        }
        else if (isRotating)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isRotated)
                {
                    UpdateCharPosToCursor(characterManager.UIManager.DirCursor, 2f);                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetDirection();
            }
        }
    }
    
    // 캐릭터 배치 플로우 시작 함수
    private void StartCharacterPlaceInStage()
    {
        characterManager.CurState = CharState.Wait;
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
            StageManager.Instance.isLocating = false;
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
        AstarGrid.Instance.SetActiveFalseAllAttackRange();
        
        // 방향 설정 UI On
        characterManager.UIManager.SetActiveStatusCanvas(true);
        characterManager.UIManager.SetActiveDirSetCanvas(true);
    }
    
    // 캐릭터 방향 설정 종료
    private void SetDirection()
    {
        AstarGrid.Instance.SetActiveFalseAllAttackRange();
        
        if (!isCanRotated)
        {
            characterManager.UIManager.DirCursor.
                transform.position = transform.position + (Vector3.up * 0.2f);
        }
        else
        {
            isRotating = false;
            isRotated = true;
            
            // 시간 원래대로
            // ~
            
            characterManager.UIManager.SetActiveDirSetCanvas(false); // 방향 설정 UI Off
            characterManager.UIManager.SetActiveProfileButtton(false); // profile UI Off
            characterManager.UIManager.SetActiveStatusBars(true); // status UI On
        
            // 활성화
            characterManager.CurState = CharState.Idle;
            //animController.SetTrigger("Start");

            StageManager.Instance.isLocating = false;
        }
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
            
            obj.transform.position = pos;
            Vector3 direction = (pos - originPos).normalized;
            float tempLen = Vector2.Distance(originPos, pos);
            if (tempLen < length - 1f)
            {
                isCanRotated = false;
                characterManager.UIManager.SetActiveCursorDirArrowSet(false);
                characterManager.UIManager.SetActiveCharDirArrowSet(false);
                AstarGrid.Instance.SetActiveFalseAllAttackRange();
            }
            else if (tempLen >= length)
            {
                obj.transform.position = originPos + direction * length;
            }
            else
            {
                isCanRotated = true;
                JudgeCharDirection(direction);
                characterManager.SetActiveAttackRange(charDir);
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
                characterManager.CharPlacedNode = node;
                characterManager.SetActiveAttackRange(charDir);
                return node.WorldPos;
            }
        }

        AstarGrid.Instance.SetActiveFalseAllAttackRange();
        isCanLocated = false;
        return mousePos;
    }
    
    // 방향에 따른 ui 활성화
    private void JudgeCharDirection(Vector3 dir)
    {
        characterManager.UIManager.SetActiveCursorDirArrowSet(true);
        characterManager.UIManager.SetActiveCharDirArrowSet(true);
        
        if (dir.x >= 0.8f)
        {
            charDir = ArrowDir.Right;
            characterManager.UIManager.SetActiveTrueCursorDirArrow(charDir);
            characterManager.UIManager.SetActiveTrueCharDirArrow(charDir);
            gameObject.transform.GetChild(0).
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        else if (dir.x <= -0.8f)
        {
            charDir = ArrowDir.Left;
            characterManager.UIManager.SetActiveTrueCursorDirArrow(charDir);
            characterManager.UIManager.SetActiveTrueCharDirArrow(charDir);
            gameObject.transform.GetChild(0).
                transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
        }
        else if (dir.y >= 0.8f)
        {
            charDir = ArrowDir.Up;
            characterManager.UIManager.SetActiveTrueCursorDirArrow(charDir);
            characterManager.UIManager.SetActiveTrueCharDirArrow(charDir);
        }
        else if (dir.y <= -0.8f)
        {
            charDir = ArrowDir.Down;
            characterManager.UIManager.SetActiveTrueCursorDirArrow(charDir);
            characterManager.UIManager.SetActiveTrueCharDirArrow(charDir);
        }
    }
}
