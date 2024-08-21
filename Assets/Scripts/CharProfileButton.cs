using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharProfileButton : MonoBehaviour, IPointerDownHandler
{
    private Button button;
    private Image charBtnIcon;
    
    private UnityEvent onButtonDown;
    private UnityEvent onButtonUp;
    
    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        charBtnIcon = button.gameObject.GetComponent<Image>();

        onButtonDown = new UnityEvent();
    }

    public void InitCharBtn(Sprite sprite, int id)
    {
        charBtnIcon.sprite = sprite;
        onButtonDown.AddListener(() => StageManager.Instance.ActiveCharObjById(id));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown.Invoke();
    }
}
