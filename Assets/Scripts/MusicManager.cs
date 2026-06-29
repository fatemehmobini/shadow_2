using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    void Awake()
    {    if (instance != null)
        {   Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void StopMusic()
    {if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}