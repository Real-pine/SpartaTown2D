using UnityEngine;

public class NameUIFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private RectTransform rectTransform;
    private Camera mainCamera;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if(target == null || mainCamera == null) return;

        Vector3 worldPosition = target.position + offset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        rectTransform.position = screenPosition;
    }
}