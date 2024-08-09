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
    private StageController stageController;
    private Camera mainCamera;
    
    private Image charBtnIcon;
    private GameObject character;

    public bool IsButtonDown { get; private set; }
    
    private void Awake()
    {
        charBtnIcon = GetComponent<Image>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(IsButtonDown)
            UpdateCharPosToCursor();
    }

    public void InitCharBtn(Sprite sprite, GameObject _character)
    {
        charBtnIcon.sprite = sprite;
        character = _character;
    }

    private void UpdateCharPosToCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f));

        if (groundPlane.Raycast(ray, out var distance))
        {
            Vector3 pos = ray.GetPoint(distance);
            pos.z = -0.2f;
            character.transform.position = pos;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        IsButtonDown = true;
        character.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsButtonDown = false;
        Debug.Log("up");
    }
}
