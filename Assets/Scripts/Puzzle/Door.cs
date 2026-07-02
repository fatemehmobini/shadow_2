using UnityEngine;

public class Door : MonoBehaviour
{   private SpriteRenderer spriteRenderer;
    private Collider2D doorCollider;
    private bool isOpen = false;

    private void Start()
    {spriteRenderer = GetComponent<SpriteRenderer>();
    doorCollider = GetComponent<Collider2D>();
    }
    public void OpenDoor()
    {   if (isOpen) return;
        isOpen = true;
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (doorCollider != null)
            doorCollider.enabled = false;
        Debug.Log("Door disappeared!");
    }
    public void CloseDoor()
    {if (!isOpen) return;
        isOpen = false;
    }
}