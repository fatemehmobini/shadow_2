using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;
    private AudioSource audioSource;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    {
        if (audioSource != null)
            audioSource.Stop();
    }
    
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}