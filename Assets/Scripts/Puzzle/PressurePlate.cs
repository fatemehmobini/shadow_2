using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredTag = "Player";
    public AudioClip pressSound;
    public GameObject keyObject;
    [Header("Events")]
    public UnityEvent onActivated;
    public UnityEvent onDeactivated;
    private bool isActive = false;
    private bool isPlayerOn = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private bool keyCollected = false;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(requiredTag))
        {bool isComingFromTop = collision.contacts[0].normal.y < -0.5f;
            if (isComingFromTop)
            {   isPlayerOn = true;
                CheckActivation();
                if (pressSound != null && audioSource != null)
                {audioSource.PlayOneShot(pressSound);
                }
                if (!keyCollected && keyObject != null)
                {CollectKey();
                }
                Debug.Log(requiredTag + " landed on plate from top!");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(requiredTag))
        {
            isPlayerOn = false;
            CheckActivation();
            Debug.Log(requiredTag + " left the plate!");
        }
    }
    private void CheckActivation()
    { bool shouldBeActive = isPlayerOn;
        if (shouldBeActive && !isActive)
        {   isActive = true;
            onActivated.Invoke();
            if (spriteRenderer != null)
                spriteRenderer.color = Color.green;
        }
        else if (!shouldBeActive && isActive)
        {   isActive = false;
            onDeactivated.Invoke();
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
        }
    }
    
    private void CollectKey()
        {   keyCollected = true;
        SpriteRenderer keySprite = keyObject.GetComponent<SpriteRenderer>();
        Collider2D keyCollider = keyObject.GetComponent<Collider2D>();
        if (keySprite != null)
            keySprite.enabled = false;
        if (keyCollider != null)
            keyCollider.enabled = false;
        Debug.Log("Key Collected by " + requiredTag);
        DoorManager doorManager = FindObjectOfType<DoorManager>();
        if (doorManager != null)
        { doorManager.KeyCollected(requiredTag);
        }
        }
    
    public bool IsPlayerOn()
    {return isPlayerOn;
    }
}