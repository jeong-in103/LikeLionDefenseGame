using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    // 초기 자원
    [Header("Resources")]
    [SerializeField] private List<CharObj> characters; // 장착한 캐릭터 데이터 리스트
    [SerializeField] private int initialCost;
    [SerializeField] private int unitLimit;

    private int cost;
    private int curUnitLimit;

    [Space] 
    // 캐릭터 프로필 UI
    [SerializeField] private List<GameObject> charProfileObj;
    private List<CharProfileButton> charProfileBtn;
    // 캐릭터 오브젝트
    private List<CharacterController> characterControllers;

    private void Awake()
    {
        // 캐릭터 프로필 버튼 캐싱
        charProfileBtn = new List<CharProfileButton>();
        foreach (var obj in charProfileObj)
        {
            charProfileBtn.Add(obj.GetComponentInChildren<CharProfileButton>());
            obj.SetActive(false);
        }
    }

    private void Start()
    {
        InitStage();
    }

    private void Update()
    {
        
    }

    // stage에 할당된 캐릭터 및 UI 초기화
    // 게임 시작 후 1번만 실행
    private void InitStage()
    {
        characterControllers = new List<CharacterController>();
        for(int i = 0; i < characters.Count; ++i)
        {
            // 캐릭터 생성
            GameObject temp = Instantiate(characters[i].charGameObj);
            characterControllers.Add(temp.GetComponent<CharacterController>());
            
            // 캐릭터 UI 초기화
            charProfileObj[i].SetActive(true);
            charProfileBtn[i].InitCharBtn(characters[i].charBtnIcon, temp);
            
            temp.SetActive(false);
        }
    }
    
    private void LocateCharInPosition()
    {
        
    }

    public void TimeSlowDown()
    {
        
    }
}
