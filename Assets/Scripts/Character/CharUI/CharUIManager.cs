using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum ArrowDir
{
    Right = 0,
    Left,
    Up,
    Down,
}

public class CharUIManager : MonoBehaviour
{
    [SerializeField] private Canvas statusCanvas;
    [SerializeField] private Canvas dirSetCanvas;
    
    // status
    [SerializeField] private GameObject hpSet;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpBarPadding;
    
    [SerializeField] private GameObject spSet;
    [SerializeField] private Image spBar;
    
    //dir
    [field:SerializeField] public GameObject DirCursor { get; private set; }
    [SerializeField] private GameObject cursorDirArrowSet;
    [SerializeField] private List<GameObject> cursorDirArrows;
    
    [SerializeField] private GameObject charDirArrowSet;
    [SerializeField] private List<GameObject> charDirArrows;

    public void InitUIController()
    {
        // 캐싱
        statusCanvas = GameObject.Find("CharCanvas").GetComponent<Canvas>();
        dirSetCanvas = GameObject.Find("DirSetCanvas").GetComponent<Canvas>();

        hpSet = statusCanvas.transform.GetChild(1).gameObject;
        spSet = statusCanvas.transform.GetChild(2).gameObject;
        
        hpBar = hpSet.transform.GetChild(1).GetComponent<Image>();
        hpBarPadding = hpSet.transform.GetChild(0).GetComponent<Image>();
        
        spBar = spSet.transform.GetChild(0).GetComponent<Image>();

        DirCursor = dirSetCanvas.gameObject.transform.GetChild(1).gameObject;
        cursorDirArrowSet = dirSetCanvas.gameObject.transform.GetChild(0).GetChild(2).gameObject;
        for(int i = 0; i < 4; ++i)
            cursorDirArrows.Add(cursorDirArrowSet.transform.GetChild(i).gameObject);
        
        charDirArrowSet = statusCanvas.gameObject.transform.GetChild(0).gameObject;
        for(int i = 0; i < 4; ++i)
            charDirArrows.Add(charDirArrowSet.transform.GetChild(i).gameObject);

        SetActiveTrueCursorDirArrow(ArrowDir.Right);
        SetActiveCursorDirArrowSet(false);
        
        SetActiveTrueCharDirArrow(ArrowDir.Right);
        SetActiveCharDirArrowSet(false);
        
        SetActiveStatusBars(false);
        
        SetActiveStatusCanvas(false);
        SetActiveDirSetCanvas(false);
    }

    public void SetActiveStatusCanvas(bool active)
    {
        statusCanvas.gameObject.SetActive(active);
    }
    
    public void SetActiveDirSetCanvas(bool active)
    {
        dirSetCanvas.gameObject.SetActive(active);
    }

    public void SetActiveStatusBars(bool active)
    {
        hpSet.SetActive(active);
        spSet.SetActive(active);
    }

    public void SetActiveCursorDirArrowSet(bool active)
    {
        cursorDirArrowSet.SetActive(active);
    }
    
    public void SetActiveTrueCursorDirArrow(ArrowDir dir)
    {
        for (int i = 0; i < charDirArrows.Count; ++i)
        {
            cursorDirArrows[i].SetActive(i == (int)dir);
        }
    }
    
    public void SetActiveCharDirArrowSet(bool active)
    {
        charDirArrowSet.SetActive(active);
    }

    public void SetActiveTrueCharDirArrow(ArrowDir dir)
    {
        for (int i = 0; i < charDirArrows.Count; ++i)
        {
            charDirArrows[i].SetActive(i == (int)dir);
        }
    }
}
