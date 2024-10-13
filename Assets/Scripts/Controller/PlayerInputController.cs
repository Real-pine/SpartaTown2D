using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MainController
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;

        CameraFollow cameraFollow = _camera.GetComponent<CameraFollow>();
        if (cameraFollow != null )
        {
            cameraFollow.target = this.transform;
        }

        //이름 UI 팔로우 설정
        NameUIFollow uIFollow = GetComponent<NameUIFollow>();
        if( uIFollow != null )
        {
            uIFollow.target = this.transform;
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }
}
