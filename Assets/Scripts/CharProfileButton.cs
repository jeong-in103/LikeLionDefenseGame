using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharProfileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        onButtonUp = new UnityEvent();
    }

    public void InitCharBtn(Sprite sprite, int id, UnityAction<int> action)
    {
        charBtnIcon.sprite = sprite;
        onButtonDown.AddListener(() => action(id));
        onButtonUp.AddListener(() => gameObject.SetActive(false));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onButtonUp.Invoke();
    }
}
