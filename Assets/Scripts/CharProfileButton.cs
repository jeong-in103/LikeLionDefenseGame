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
    private Image charBtnIcon;
    private CharacterController character;

    public bool IsButtonDown { get; private set; }
    
    private void Awake()
    {
        charBtnIcon = GetComponent<Image>();
    }

    public void InitCharBtn(Sprite sprite, GameObject _character)
    {
        charBtnIcon.sprite = sprite;
        character = _character.GetComponent<CharacterController>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        IsButtonDown = true;
        character.gameObject.SetActive(true);
        character.StartCharacterPlaceInStage();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsButtonDown = false;
        character.LocatedAtPos();
    }
}
