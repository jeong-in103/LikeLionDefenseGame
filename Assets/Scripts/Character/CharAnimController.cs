using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }
}
