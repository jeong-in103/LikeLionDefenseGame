using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 WorldPos { get; private set; }
    [field:SerializeField] public bool IsWalkAble { get; private set; }

    public GameObject highlight;
    public GameObject range;

    private void Awake()
    {
        WorldPos = transform.position;
        highlight= this.transform.GetChild(0).gameObject;
        range = this.transform.GetChild(1).gameObject;
    }
}
