using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //따라갈 타겟
    [SerializeField] private float smoothSpeed = 0.125f; //카메라 이동속도(인스펙터창에서 조절가능)
    [SerializeField] private Vector3 offset; //카메라와 타겟 사이의 오프셋

    private void LateUpdate() //LateUpdate인게 중요
    {
        if(target == null)
        {
            Debug.LogWarning("camera target is not set");
            return;
        }

        //목표 위치 계산 (타겟위치 + 오프셋)
        Vector3 desiredPosition = target.position + offset;

        //Lerp를 이용해 카메라 위치를 부드럽게 이동
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        transform.hasChanged = true;
    }

}
