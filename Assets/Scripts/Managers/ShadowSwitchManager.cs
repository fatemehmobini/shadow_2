using UnityEngine;

public class ShadowSwitchManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject aria;
    [SerializeField] private GameObject shadow;
    [SerializeField] private AudioSource switchSound;
    [SerializeField] private CameraFollow cameraFollow;
    [Header("Settings")]
    [SerializeField] private KeyCode switchKey = KeyCode.Q;
    [SerializeField] private float switchCooldown = 0.3f;
    [SerializeField] private float spawnDistance = 2f;
    [Header("Sound Effects")]
    [SerializeField] private float normalPitch = 1f;
    [SerializeField] private float shadowPitch = 0.7f;
    private bool isShadowActive = false;
    private bool isShadowSpawned = false;
    private float lastSwitchTime = -10f;
    private PlayerController ariaController;
    private PlayerController shadowController;
    private void Start()
    {
        ariaController = aria.GetComponent<PlayerController>();
        shadowController = shadow.GetComponent<PlayerController>();
        shadow.SetActive(false);
        isShadowActive = false;
        isShadowSpawned = false;
        if (ariaController != null)
            ariaController.enabled = true;
        if (shadowController != null)
            shadowController.enabled = false;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(switchKey) && Time.time - lastSwitchTime > switchCooldown)
        {
            SwitchCharacter();
            lastSwitchTime = Time.time;
        }
    }
    
    private void SwitchCharacter()
    {
        if (!isShadowSpawned)
        {
            SpawnShadow();
        }
        else
        {
            ToggleControl();
        }
    }
    
    private void SpawnShadow()
    {
        Vector3 spawnPosition = aria.transform.position;
        float direction = aria.transform.localScale.x > 0 ? 1f : -1f;
        spawnPosition.x += direction * spawnDistance;
        
        shadow.transform.position = spawnPosition;
        shadow.SetActive(true);
        isShadowActive = true;
        isShadowSpawned = true;
        
        if (ariaController != null)
            ariaController.enabled = false;
        if (shadowController != null)
            shadowController.enabled = true;
        
        if (cameraFollow != null)
            cameraFollow.SetTarget(shadow.transform);
        
        PlaySwitchSound(true);
        Debug.Log("Shadow spawned at: " + spawnPosition);
    }
    
    private void ToggleControl()
    {
        isShadowActive = !isShadowActive;
        
        if (isShadowActive)
        {
            if (ariaController != null)
                ariaController.enabled = false;
            if (shadowController != null)
                shadowController.enabled = true;
            
            if (cameraFollow != null)
                cameraFollow.SetTarget(shadow.transform);
            
            PlaySwitchSound(true);
        }
        else
        {
            if (ariaController != null)
                ariaController.enabled = true;
            if (shadowController != null)
                shadowController.enabled = false;
            
            if (cameraFollow != null)
                cameraFollow.SetTarget(aria.transform);
            
            PlaySwitchSound(false);
        }
    }
    
    private void PlaySwitchSound(bool toShadow)
    {
        if (switchSound != null)
        {
            if (toShadow)
                switchSound.pitch = shadowPitch;
            else
                switchSound.pitch = normalPitch;
            
            switchSound.Play();
        }
    }
    public void ResetShadow()
    {
        isShadowActive = false;
        isShadowSpawned = false;
        
        if (shadow != null)
        {shadow.SetActive(false);
        }
        
        if (ariaController != null)
            ariaController.enabled = true;
        if (shadowController != null)
            shadowController.enabled = false;
        
        Debug.Log("Shadow reset to initial state!");
    }
    
    public bool IsShadowActive()
    {return isShadowActive;
    }
}