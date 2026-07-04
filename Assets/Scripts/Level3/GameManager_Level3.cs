using UnityEngine;
using System.Collections.Generic;

public class GameManager_Level3 : MonoBehaviour
{
    public static GameManager_Level3 Instance;
    [Header("References")]
    [SerializeField] private GameObject aria;
    [SerializeField] private GameObject shadow;
    [SerializeField] private ShadowSwitchManager shadowSwitchManager;
    [SerializeField] private CameraFollow cameraFollow;
    [Header("Checkpoints")]
    [SerializeField] private Transform checkpoint1;
    [SerializeField] private Transform checkpoint2;
    [SerializeField] private Transform checkpoint3;
    [SerializeField] private Transform checkpoint4;
    private Transform currentCheckpoint;
    private Vector3 ariaStartPosition;
    private Vector3 shadowStartPosition;
    private bool isGameOver = false;
    private void Awake()
    {   if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        if (aria != null)
            ariaStartPosition = aria.transform.position;
        if (shadow != null)
            shadowStartPosition = shadow.transform.position;
        
        currentCheckpoint = checkpoint1;
        isGameOver = false;
    }
    
    public void SetCheckpoint(Transform checkpoint)
    {
        if (checkpoint == checkpoint1 || checkpoint == checkpoint2 || 
            checkpoint == checkpoint3 || checkpoint == checkpoint4)
        {
            currentCheckpoint = checkpoint;
            Debug.Log("Checkpoint SAVED at: " + checkpoint.position.x);
        }
    }
    
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Vector3 respawnPos;
        if (currentCheckpoint != null)
            respawnPos = currentCheckpoint.position;
        else
            respawnPos = new Vector3(-4f, -3.5f, 0f);
        Debug.Log("Respawning at: " + respawnPos.x);
        if (aria != null)
        {   aria.transform.position = respawnPos;
            Rigidbody2D rb = aria.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
            
            PlayerController pc = aria.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = true;
        }
        if (shadow != null)
        {
            shadow.transform.position = respawnPos + new Vector3(2, 0, 0);
            Rigidbody2D rb = shadow.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
            shadow.SetActive(false);
            
            PlayerController pc = shadow.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;
        }
        if (shadowSwitchManager != null)
            shadowSwitchManager.ResetShadow();
        if (cameraFollow != null && aria != null)
            cameraFollow.SetTarget(aria.transform);
        
        Invoke(nameof(ResetGameOverFlag), 1.5f);
    }
    
    private void ResetGameOverFlag()
    {   isGameOver = false;
    }
}