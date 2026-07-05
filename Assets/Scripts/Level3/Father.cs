using UnityEngine;

public class Father : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip fatherSound;
    private bool hasTriggered = false;
    private AudioSource audioSource;
    void Start()
    {audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    { if (hasTriggered) return;
        if (other.CompareTag("Player")) 
        {   hasTriggered = true;
            Debug.Log("Aria reached the Father!");

            if (fatherSound != null && audioSource != null)
            {   audioSource.PlayOneShot(fatherSound);
                Debug.Log("Father sound played!");
            }
            Invoke(nameof(ShowWinPanel), 1.5f);
        }
    }
    void ShowWinPanel()
    {   WinManager winManager = FindObjectOfType<WinManager>();
        if (winManager != null)
        {   winManager.ShowWinPanelDirectly();
        }
    }
}