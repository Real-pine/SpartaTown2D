using UnityEngine;

public class MainMovement : MonoBehaviour // 실제로 움직임을 실행하는 실행자
{
    private MainController movementController;
    private Rigidbody2D movementRigidboty;

    private Vector2 movementDirection = Vector2.zero; //초기화설정

    private void Awake()
    {
        // OnmoveEvent에 Move호출
        movementController.OnMoveEnent += Move;
    }

    private void Move(Vector2 direction)
    {
        //이동 방향만 정해두고 실제로 움직이는 것은 아님
        //움직이는 것은 FixedUpdate에서 진행됨
        movementDirection = direction;
    }

    private void FixedUpdate() //물리적인건 FixedUpdate
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5; //캐릭터 스탯관리를 지금 개인과제에서 굳이 만들필요가 있나?
    }
}