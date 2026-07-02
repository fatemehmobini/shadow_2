using UnityEngine;

public class CameraFollow : MonoBehaviour
{   [Header("Targets")]
    [SerializeField] private Transform targetAria;
    [SerializeField] private Transform targetShadow;
    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float offsetY = 0f;
    [SerializeField] private float offsetZ = -10f;
    private Transform currentTarget;
    private Vector3 targetPosition;
    void Start()
    { currentTarget = targetAria;
        if (currentTarget != null)
        {
            targetPosition = new Vector3(
                currentTarget.position.x + offsetX,
                offsetY,
                offsetZ
            );
            transform.position = targetPosition;
        }
    }

    void LateUpdate()
    {if (currentTarget == null) return;
        Vector3 desiredPosition = new Vector3(
            currentTarget.position.x + offsetX,
            offsetY,
            offsetZ
        );
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y = offsetY;
        smoothedPosition.z = offsetZ;
        transform.position = smoothedPosition;
    }
    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}