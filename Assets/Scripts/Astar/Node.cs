using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 WorldPos { get; private set; }
    [field:SerializeField] public bool IsWalkAble { get; private set; }

    private void Awake()
    {
        WorldPos = transform.position;
    }
}
