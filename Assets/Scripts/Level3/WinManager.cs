using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private AudioClip winSound;
    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    private bool hasWon = false;
    private AudioSource winAudioSource;
    void Start()
    {   winAudioSource = gameObject.AddComponent<AudioSource>();
        winAudioSource.playOnAwake = false;
    }
    public void ShowWinPanelDirectly()
    {   if (hasWon) return;
        hasWon = true;
        if (winPanel != null)
        {   winPanel.SetActive(true);
            Time.timeScale = 0f;
            if (BackgroundMusicManager.Instance != null)
            {   BackgroundMusicManager.Instance.StopMusicForever();
                Debug.Log("Background music stopped forever!");
            }
            if (winSound != null && winAudioSource != null)
            {   winAudioSource.PlayOneShot(winSound);
                Debug.Log("Win sound played!");
            }
            Debug.Log("You Win!");
        }
    }
    public void GoToMainMenu()
    {   Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
       
    }
}