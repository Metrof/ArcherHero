
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector3 offset;

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition  = _target.position + offset;
        targetPosition.y = transform.position.y; 
        targetPosition.x = transform.position.x;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}