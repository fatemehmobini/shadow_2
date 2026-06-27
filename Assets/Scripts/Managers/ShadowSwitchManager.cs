using UnityEngine;

public class ShadowSwitchManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject aria;
    [SerializeField] private GameObject shadow;
    [Header("Settings")]
    [SerializeField] private KeyCode switchKey = KeyCode.Q;
    [SerializeField] private float switchCooldown = 0.3f;
    private bool isShadowActive = false;
    private float lastSwitchTime = -10f;
    private PlayerController ariaController;
    private PlayerController shadowController;
    
    private void Start()
    {   ariaController = aria.GetComponent<PlayerController>();
        shadowController = shadow.GetComponent<PlayerController>();
        SetActiveCharacter(false);
    }
    
    private void Update()
    {   if (Input.GetKeyDown(switchKey) && Time.time - lastSwitchTime > switchCooldown)
        {
            SwitchCharacter();
            lastSwitchTime = Time.time;
        }
    }
    
    private void SwitchCharacter()
    {   isShadowActive = !isShadowActive;
        SetActiveCharacter(isShadowActive);
        Debug.Log("Switched to: " + (isShadowActive ? "Shadow" : "Aria"));
    }
    private void SetActiveCharacter(bool shadowActive)
    {    if (ariaController != null)
            ariaController.enabled = !shadowActive;
         if (shadowController != null)
            shadowController.enabled = shadowActive;  
    }
    
    public bool IsShadowActive()
    {return isShadowActive;
    }
}