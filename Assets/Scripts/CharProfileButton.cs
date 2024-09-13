using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharProfileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image charBtnIcon;

    private Animator animator;
    
    private UnityEvent onButtonDown;
    private UnityEvent onButtonUp;
    
    private static readonly int Highlighted = Animator.StringToHash("Highlighted");
    private static readonly int Disabled = Animator.StringToHash("Disabled");

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        charBtnIcon = button.gameObject.GetComponent<Image>();
        animator = GetComponent<Animator>();

        onButtonDown = new UnityEvent();
    }

    public void InitCharBtn(Sprite sprite, int id)
    {
        charBtnIcon.sprite = sprite;
        onButtonDown.AddListener(() => StageManager.Instance.ActiveCharObjById(id));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!StageManager.Instance.isLocating)
        {
            animator.SetTrigger(Highlighted);
            onButtonDown.Invoke();
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(!StageManager.Instance.isLocating)
            animator.SetTrigger(Disabled);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!StageManager.Instance.isLocating)
            animator.SetTrigger(Highlighted);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!StageManager.Instance.isLocating)
            animator.SetTrigger(Disabled);
    }
}
