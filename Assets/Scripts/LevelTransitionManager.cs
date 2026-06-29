using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private GameObject interactivePanel;
    [SerializeField] private GameObject aria;
    [SerializeField] private GameObject shadow;
    [Header("Settings")]
    [SerializeField] private float targetX = 5f;
    [SerializeField] private float arrowX = 6.5f;
    [SerializeField] private float arrowY = 0f;
    private bool arrowShown = false;
    private bool panelShown = false;
    
    void Update()
    {if (!arrowShown && aria.transform.position.x > targetX && shadow.transform.position.x > targetX)
        {   ShowArrow();
            arrowShown = true;
        }
    }
    
    void ShowArrow()
    { arrowImage.SetActive(true);
        Vector3 worldPos = new Vector3(arrowX, arrowY, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        RectTransform rect = arrowImage.GetComponent<RectTransform>();
        rect.position = screenPos;
    }
    
    public void OnArrowClicked()
    {if (panelShown) return;
        interactivePanel.SetActive(true);
        panelShown = true;
        arrowImage.SetActive(false);
    }
    
    public void GoToInteractiveLevel()
    {SceneManager.LoadScene("Level2_Interactive");
    }
}