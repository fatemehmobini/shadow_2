using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    private bool isActive = false;
    private void OnTriggerEnter2D(Collider2D other)
    {   if (other.CompareTag("Player") && !isActive)
        {   isActive = true;
            GameManager.Instance.SetCheckpoint(transform);
            
            Debug.Log("Checkpoint activated at: " + transform.position.x);
        }
    }
}