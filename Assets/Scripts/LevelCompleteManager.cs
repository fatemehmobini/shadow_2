using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject aria;
    [SerializeField] private GameObject levelCompletePanel;
    [Header("Settings")]
    [SerializeField] private float targetX = 88f;
    private bool levelCompleted = false;
    void Update()
    {if (!levelCompleted && aria != null && aria.transform.position.x >= targetX)
        {
            levelCompleted = true;
            ShowLevelComplete();
        }
    }
    
    void ShowLevelComplete()
    {   if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            Debug.Log("Level Complete! X: " + aria.transform.position.x);
        }
    }
    public void GoToNextLevel()
    {   Debug.Log("Going to Next Level...");
        SceneManager.LoadScene("Level3_Advanced");
    }
}