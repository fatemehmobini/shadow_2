using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Boss boss;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip winSound; 
    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    private bool hasWon = false;
    private AudioSource winAudioSource;
    void Start()
    {   winAudioSource = gameObject.AddComponent<AudioSource>();
        winAudioSource.playOnAwake = false;
    }
    void Update()
    {   if (!hasWon && boss != null && boss.IsDead())
        {hasWon = true;
        ShowWinPanel();
        }
    }
    void ShowWinPanel()
    {   if (winPanel != null)
        {   winPanel.SetActive(true);
            Time.timeScale = 0f;
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
                Debug.Log("Background music stopped.");
            }
            if (winSound != null && winAudioSource != null)
            {
                winAudioSource.PlayOneShot(winSound);
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