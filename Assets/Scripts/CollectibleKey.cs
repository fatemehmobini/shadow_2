using UnityEngine;

public class CollectibleKey : MonoBehaviour
{
    [Header("Settings")]
    public string keyType = "Aria"; 
    public float heightTolerance = 1.5f;
    private bool isCollected = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D keyCollider;
    private float keyY;
    private void Start()
    {   spriteRenderer = GetComponent<SpriteRenderer>();
        keyCollider = GetComponent<Collider2D>();
        keyY = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   if (isCollected) return;
        bool isCorrectPlayer = false;
        string playerName = "";
        if (keyType == "Aria" && other.CompareTag("Player"))
        {isCorrectPlayer = true;
        playerName = "Aria";
        }
        else if (keyType == "Shadow" && other.CompareTag("Shadow"))
        {isCorrectPlayer = true;
        playerName = "Shadow";
        }
        
        if (isCorrectPlayer)
        {   float characterY = other.transform.position.y;
            float yDifference = Mathf.Abs(characterY - keyY);
            if (yDifference <= heightTolerance)
            {
                CollectKey(playerName);
            }
            else
            {
                Debug.Log(playerName + " Y: " + characterY + ", Key Y: " + keyY + ", Diff: " + yDifference);
            }
        }
    }
    
    private void CollectKey(string playerName)
    {   isCollected = true;
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (keyCollider != null)
            keyCollider.enabled = false;
        Debug.Log("Key Collected by " + playerName + " (" + keyType + " Key)");
        DoorManager doorManager = FindObjectOfType<DoorManager>();
        if (doorManager != null)
        {doorManager.KeyCollected(keyType);
        }
    }
}