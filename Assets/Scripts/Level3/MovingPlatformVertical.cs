using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float topY = 1.8f;
    [SerializeField] private float bottomY = -2.8f;
    private float direction = 1f;
    private float fixedX;
    private Quaternion fixedRotation;
    private GameObject currentPassenger;
    private bool isMoving = true;
    void Start()
    {
        fixedX = transform.position.x;
        fixedRotation = transform.rotation;
        Vector3 pos = transform.position;
        pos.y = bottomY;
        transform.position = pos;
    }
    
    void Update()
    {
        if (!isMoving) return;
        float newY = transform.position.y + direction * moveSpeed * Time.deltaTime;
        if (newY >= topY)
        {   newY = topY;
            direction = -1f;
        }
        else if (newY <= bottomY)
        {   newY = bottomY;
            direction = 1f;
        }
        transform.position = new Vector3(fixedX, newY, transform.position.z);
        transform.rotation = fixedRotation;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {   if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shadow"))
        {   currentPassenger = collision.gameObject;
            currentPassenger.transform.SetParent(transform);
            currentPassenger.transform.rotation = Quaternion.identity;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shadow"))
        {if (currentPassenger != null)
            {   currentPassenger.transform.SetParent(null);
                currentPassenger = null;
            }
        }
    }
}