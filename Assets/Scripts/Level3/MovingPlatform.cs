using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float leftLimit = -2f;
    [SerializeField] private float rightLimit = 2f;
    private Vector3 startPosition;
    private float direction = 1f;
    private float fixedY;
    private GameObject currentPassenger;
    void Start()
    {
        startPosition = transform.position;
        fixedY = transform.position.y;
    }
    
    void Update()
    {   float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;
        
        if (newX >= startPosition.x + rightLimit)
        {   newX = startPosition.x + rightLimit;
            direction = -1f;
        }
        else if (newX <= startPosition.x + leftLimit)
        {   newX = startPosition.x + leftLimit;
            direction = 1f;
        }
        transform.position = new Vector3(newX, fixedY, transform.position.z);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shadow"))
        {   currentPassenger = collision.gameObject;
            currentPassenger.transform.SetParent(transform);
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