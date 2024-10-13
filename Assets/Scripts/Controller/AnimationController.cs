using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected MainController mainController;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        mainController = GetComponent<MainController>();
    }
}
