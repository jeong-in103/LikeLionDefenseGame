using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // 게임 정보
    [Header("Game Info")] 
    [SerializeField] private int maxEnemyNum;
    [SerializeField] private int maxTowerHp;

    private int curEnemyNum;
    private int curTowerHp;
    
    // 초기 자원
    [Header("Resources")]
    [SerializeField] private List<GameObject> characters; // 장착한 캐릭터 데이터 리스트
    [SerializeField] private int initialCost;
    [SerializeField] private int unitLimit;

    private int curCost;
    private int curUnitLimit;

    [Space] 
    // 캐릭터 프로필 UI
    [SerializeField] private List<GameObject> charProfileObj; // 초기화 할때만 사용
    private List<CharProfileButton> charProfileBtn;
    // 캐릭터 매니져
    private Dictionary<int, CharacterManager> characterDictionary;

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
        charProfileBtn = new List<CharProfileButton>();
        foreach (var button in charProfileObj)
        {
            charProfileBtn.Add(button.GetComponentInChildren<CharProfileButton>());
            button.SetActive(false);
        }
        
        characterDictionary = new Dictionary<int, CharacterManager>();
        for(int i = 0; i < characters.Count; ++i)
        {
            // 캐릭터 생성
            CharacterManager temp = Instantiate(characters[i]).GetComponent<CharacterManager>();
            
            // 캐릭터 UI 초기화
            charProfileObj[i].SetActive(true);
            charProfileBtn[i].InitCharBtn(temp.CharStatus.CharProfileImage, temp.CharId, ActiveCharObjById);
            
            // 캐릭터 초기화
            temp.InitCharacter();
            temp.gameObject.SetActive(false);
            
            characterDictionary.Add(temp.CharId, temp);
        }
    }

    private void ActiveCharObjById(int id)
    {
        if (characterDictionary.TryGetValue(id, out var temp))
        {
            temp.gameObject.SetActive(true);
            temp.SetActiveEvent();
        }
        else
            Debug.LogWarning("is no GameManager matching that Id");
    }
    
    private void LocateCharInPosition()
    {
        
    }

    public void TimeSlowDown()
    {
        
    }
}
