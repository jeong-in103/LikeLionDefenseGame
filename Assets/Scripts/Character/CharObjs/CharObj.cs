using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharObj", menuName = "Scriptable Object/CharObj")]
public class CharObj : ScriptableObject
{
    public GameObject charGameObj;
    public Sprite charBtnIcon;
}
