using UnityEngine;
using Cinemachine;

public class CameraZoomBasedOnObjectScale : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _target;
    [SerializeField] private float _baseYOffset = 5f;
    [SerializeField] private float _baseZOffset = -10f;

    private void Update()
    {
        if (_virtualCamera == null || _target == null)
        {
            Debug.LogError("CinemachineVirtualCamera or target object is not assigned to CameraZoomBasedOnObjectScale script!");
            return;
        }
        float targetScale = _target.localScale.x;
        Vector3 newOffset = CalculateNewOffset(targetScale);

        SetCameraOffset(newOffset);
    }

    private Vector3 CalculateNewOffset(float objectScale)
    {
        float newYOffset = _baseYOffset * objectScale;
        float newZOffset = _baseZOffset * objectScale;

        return new Vector3(0f, newYOffset, newZOffset);
    }

    private void SetCameraOffset(Vector3 newOffset)
    {
        _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = newOffset;
    }
}
