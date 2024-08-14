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
    private CharState curState = CharState.Idle;
    public void ChangeState(CharState newState)
    {
        curState = newState;
    }

    // 캐릭터가 위치한 노드
    public Node CharPlacedNode { get; set; }  
    
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

        // status init
        HP = CharStatus.maxHp;
        SP = CharStatus.maxSp;
        ATK = CharStatus.atk;
        DEF = CharStatus.def;
        AtkSpeed = CharStatus.atkSpeed;
        CurLocatedCoolTime = CharStatus.locatedCoolTime;

        OnCharacterLocated = new UnityEvent();
    }
    
    public void InitCharacter()
    {
        UIManager.InitUIController();
    }

    public void SetActiveEvent()
    {
        OnCharacterLocated.Invoke();
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
