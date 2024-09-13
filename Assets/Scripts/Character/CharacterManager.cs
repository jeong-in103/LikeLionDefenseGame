using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public int CharId { get; private set; }
    
    // 캐릭터 초기 status
    [field: SerializeField] public CharStatus CharStatus { get; private set; }
    
    #region status property

    private readonly bool[,] attackRange = new bool[3, 3];
    
    private float curHp;
    public float HP
    {
        get => curHp;
        set => curHp = Mathf.Clamp(value, 0f, CharStatus.maxHp);
    }
    
    private float curSp;
    public float SP
    {
        get => curSp;
        set => curSp = Mathf.Clamp(value, 0f, CharStatus.maxSp);
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
    
    private float curLocatedCoolTime;
    public float CurLocatedCoolTime
    {
        get => curLocatedCoolTime;
        set => curLocatedCoolTime = value;
    }

    #endregion
    
    // 캐릭터 상태
    public CharState CurState { get; set; }

    // 캐릭터가 위치한 노드
    public Node CharPlacedNode { get; set; }  
    
    public List<Node> CharAttackableNodes { get; set; }
    
    //캐릭터 애님컨트롤러
    private CharAnimController animController;
    
    // 캐릭터 UI
    public CharUIManager UIManager { get; private set; }
    
    // 이벤트
    public UnityEvent OnCharacterLocated { get; private set; }
    
    private void Awake()
    {
        CharId = this.gameObject.GetInstanceID();
        
        animController = GetComponentInChildren<CharAnimController>();
        UIManager = GetComponent<CharUIManager>();
        
        // attack range init
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                attackRange[i, j] = CharStatus.attackRange[i + j];
            }
        }
        
        // status init
        HP = CharStatus.maxHp;
        SP = CharStatus.maxSp;
        ATK = CharStatus.atk;
        DEF = CharStatus.def;
        AtkSpeed = CharStatus.atkSpeed;
        CurLocatedCoolTime = CharStatus.locatedCoolTime;

        OnCharacterLocated = new UnityEvent();
    }
    
    public void InitCharacter(CharProfileButton profileButton)
    {
        UIManager.InitUIController(profileButton);
    }

    public void SetActiveEvent()
    {
        gameObject.SetActive(true);
        OnCharacterLocated.Invoke();
    }

    public void SetActiveAttackRange(ArrowDir dir)
    {
        CharAttackableNodes = AstarGrid.Instance.SetActiveAttackRange(CharPlacedNode, attackRange, dir);
    }
    
    private void Attack()
    {
        
    }

    private void UseSkill()
    {
        
    }

    private void Die()
    {
        //IsLocated = false;
    }
}
