using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class MirrorWorld : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private KeyCode mirrorKey = KeyCode.M;
    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private Sprite lightBackground;
    [SerializeField] private Sprite darkBackground;
    [Header("Lighting")]
    [SerializeField] private Color lightAmbient = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color darkAmbient = new Color(0.5f, 0.5f, 0.6f, 1f);
    [Header("Camera")]
    [SerializeField] private Color lightCameraColor = new Color(0.1f, 0.1f, 0.2f);
    [SerializeField] private Color darkCameraColor = new Color(0.02f, 0.02f, 0.05f);
    [Header("Platform Color")]
    [SerializeField] private Color lightPlatformColor = Color.white; 
    private bool isDarkMode = true;
    private Camera mainCamera;
    private Light2D globalLight;
    private Dictionary<SpriteRenderer, Color> originalColors = new Dictionary<SpriteRenderer, Color>();
    private List<SpriteRenderer> platforms = new List<SpriteRenderer>();
    void Start()
    {
        mainCamera = Camera.main;
        globalLight = FindObjectOfType<Light2D>();
        SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
        foreach (var sr in allSprites)
        {   if (sr.gameObject.CompareTag("Player") || sr.gameObject.CompareTag("Shadow"))
                continue;
            if (sr == backgroundSprite)
                continue;
            platforms.Add(sr);
            originalColors[sr] = sr.color;
        }
        Debug.Log("Found " + platforms.Count + " platforms");
        SetDarkMode();
    }
    void Update()
    {   if (Input.GetKeyDown(mirrorKey))
        {
            ToggleMirror();
        }
    }
    void ToggleMirror()
    {
        isDarkMode = !isDarkMode;
        Debug.Log("Mirror World: " + (isDarkMode ? "Dark Mode" : "Light Mode"));
        
        if (isDarkMode)
            SetDarkMode();
        else
            SetLightMode();
    }
    void SetLightMode()
    {   if (backgroundSprite != null && lightBackground != null)
            backgroundSprite.sprite = lightBackground;
        if (mainCamera != null)
            mainCamera.backgroundColor = lightCameraColor;
        if (globalLight != null)
        {   globalLight.color = lightAmbient;
            globalLight.intensity = 1f;
        }
        foreach (var sr in platforms)
        {   if (sr != null)
                sr.color = lightPlatformColor;
        }
    }
    void SetDarkMode()
    {   if (backgroundSprite != null && darkBackground != null)
            backgroundSprite.sprite = darkBackground;
        if (mainCamera != null)
            mainCamera.backgroundColor = darkCameraColor;
        
        if (globalLight != null)
        {   globalLight.color = darkAmbient;
            globalLight.intensity = 0.5f;
        }
        foreach (var kvp in originalColors)
        {   if (kvp.Key != null)
                kvp.Key.color = kvp.Value;
        }
    }
}