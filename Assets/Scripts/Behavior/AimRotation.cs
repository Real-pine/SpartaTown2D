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
        // 마우스 위치가 들어오는 OnLookEvent에 등록
        // 마우스 위치를 받아서 Rotation하는데 활용
        mainController.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        // OnLook
        RotateCharacter(newAimDirection);
    }

    private void RotateCharacter(Vector2 direction)
    {
        // 마우스 위치를 받아서 Atan2로 각도 계산 -> 라디안값을 디그리로 변환하는 식
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 캐릭터 뒤집기
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }
}
