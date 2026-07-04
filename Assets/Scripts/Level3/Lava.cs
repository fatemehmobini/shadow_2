using UnityEngine;

public class Lava : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;
    private bool hasTriggered = false;
    
    void Start()
    {audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        hasTriggered = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return; 
        
        if (other.CompareTag("Player") || other.CompareTag("Shadow"))
        {
            hasTriggered = true;
            Debug.Log(other.name + " fell into lava!");
            
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
            
            GameManager.Instance.GameOver();
            Invoke(nameof(ResetTrigger), 1.5f);
        }
    }
    void ResetTrigger()
    {   hasTriggered = false;
    }
}