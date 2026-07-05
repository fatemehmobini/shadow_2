using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;
    private AudioSource audioSource;
    private bool isStoppedForever = false; 
    void Awake()
    {   if (Instance == null)
        {   Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {   Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }
    
    public void StopMusic()
    {   if (audioSource != null)
            audioSource.Stop();
    }
    public void StopMusicForever()
    {   isStoppedForever = true;
        if (audioSource != null)
        {   audioSource.Stop();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }
        Debug.Log("Background music stopped forever!");
    }
    public void PlayMusic()
    {   if (isStoppedForever) return; 
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}