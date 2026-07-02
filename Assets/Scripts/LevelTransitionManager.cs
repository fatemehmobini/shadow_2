using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject aria;
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private GameObject interactivePanel;
    [Header("Settings")]
    [SerializeField] private float targetX = 5f;
    [SerializeField] private float arrowX = 6.5f;
    [SerializeField] private float arrowY = 0f;
    private bool arrowShown = false;
    private bool panelShown = false;
    
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level2_Interactive" || sceneName == "Level3_Advanced")
        {   this.enabled = false;
            Debug.Log("LevelTransitionManager disabled in: " + sceneName);
            return;
        }
    }
    
    void Update()
    {
        if (!this.enabled) return;
        if (aria == null || shadow == null)
        { this.enabled = false;
            return;
        }
        
        if (!arrowShown && aria.transform.position.x > targetX && shadow.transform.position.x > targetX)
        {   ShowArrow();
            arrowShown = true;
        }
    }
    
    void ShowArrow()
    {
        if (arrowImage == null) return;
        arrowImage.SetActive(true);
        Vector3 worldPos = new Vector3(arrowX, arrowY, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        RectTransform rect = arrowImage.GetComponent<RectTransform>();
        if (rect != null)
        { rect.position = screenPos;
        }
    }
    public void OnArrowClicked()
    {   if (panelShown) return;
        if (interactivePanel != null)
        {interactivePanel.SetActive(true);
        }
        panelShown = true;
        if (arrowImage != null)
        {arrowImage.SetActive(false);
        }
    }
    public void GoToInteractiveLevel()
    {   SceneManager.LoadScene("Level2_Interactive");
    }
}