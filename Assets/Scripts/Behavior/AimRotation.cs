using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    private MainController mainController;

    private void Awake()
    {
        mainController = GetComponent<MainController>();
    }

    private void Start()
    {
        // ���콺 ��ġ�� ������ OnLookEvent�� ���
        // ���콺 ��ġ�� �޾Ƽ� Rotation�ϴµ� Ȱ��
        mainController.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        // OnLook
        RotateCharacter(newAimDirection);
    }

    private void RotateCharacter(Vector2 direction)
    {
        // ���콺 ��ġ�� �޾Ƽ� Atan2�� ���� ��� -> ���Ȱ��� ��׸��� ��ȯ�ϴ� ��
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // ĳ���� ������
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }
}
