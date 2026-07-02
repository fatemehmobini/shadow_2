using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{   [Header("Patrol Settings")]
    [SerializeField] private float patrolRange = 5f;
    [SerializeField] private float patrolSpeed = 1.5f;
    [Header("Light Settings")]
    [SerializeField] private Light2D enemyLight;
    [SerializeField] private float lightRange = 4f;
    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSound;
    private Vector3 startPosition;
    private float direction = -1f;
    private bool isFacingRight = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;
    private bool isActive = true;
    private bool isPlayingSound = false;
    
    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), 1);
        if (enemyLight != null)
        {enemyLight.transform.localRotation = Quaternion.Euler(0, 0, -360);
        }
    }
    void Update()
    {if (!isActive) return;
        Patrol();
        DetectPlayer();
    }
    void Patrol()
    {transform.position += Vector3.right * direction * patrolSpeed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - startPosition.x) >= patrolRange)
        {direction *= -1;
            Flip();
        }
    }
    
    void Flip()
    {isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        transform.rotation = Quaternion.identity;
        if (enemyLight != null)
        {if (!isFacingRight)
                enemyLight.transform.localRotation = Quaternion.Euler(0, 0, -360);
            else
                enemyLight.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void DetectPlayer()
    {if (enemyLight == null || !isActive || isPlayingSound) return;
        Vector2 lightDirection = -Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(enemyLight.transform.position, lightDirection, lightRange);
        if (hit.collider != null)
        {if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Shadow"))
            {
                Debug.Log("Player detected by " + gameObject.name);
                PlayGameOverSound();
                Disappear();
                GameManager.Instance.GameOver();
            }
        }
    }
    
    void PlayGameOverSound()
    {if (gameOverSound != null && audioSource != null)
        {
            isPlayingSound = true;
            audioSource.PlayOneShot(gameOverSound);
            Invoke(nameof(ResetSoundFlag), 2f);
        }
    }
    
    void ResetSoundFlag()
    {isPlayingSound = false;
    }
    void Disappear()
    {   isActive = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (enemyCollider != null) enemyCollider.enabled = false;
        if (enemyLight != null) enemyLight.enabled = false;
    }
    public void Respawn()
    {   isActive = true;
        isPlayingSound = false;
        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (enemyCollider != null) enemyCollider.enabled = true;
        if (enemyLight != null)
        {   enemyLight.enabled = true;
            enemyLight.transform.localRotation = Quaternion.Euler(0, 0, -360);
        }
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
        direction = -1f;
        isFacingRight = false;
    }
}