using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float leftLimit = 42f;
    [SerializeField] private float rightLimit = 89f;
    [SerializeField] private float fixedY = -2.51f;
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound; 
    [SerializeField] private AudioClip gameOverSound;
    private float direction = 1f;
    private SpriteRenderer spriteRenderer;
    private bool isActive = true;
    private bool isDead = false;
    private AudioSource audioSource;
    private Collider2D bossCollider;
    
    void Start()
    {    audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossCollider = GetComponent<Collider2D>();
        spriteRenderer.enabled = true;
        bossCollider.enabled = true;
        isActive = true;
        isDead = false;
        transform.position = new Vector3(42f, fixedY, transform.position.z);
    }
    
    void Update()
    {   if (!isActive || isDead) return;
        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;
        if (newX >= rightLimit)
        {
            newX = rightLimit;
            direction = -1f;
            FlipX();
        }
        else if (newX <= leftLimit)
        {   newX = leftLimit;
            direction = 1f;
            FlipX();
        }
        transform.position = new Vector3(newX, fixedY, transform.position.z);
    }
    
    void FlipX()
    {   Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void KillBoss()
    {
        if (isDead || !isActive) return;
        isDead = true;
        isActive = false;
        spriteRenderer.enabled = false;
        bossCollider.enabled = false;
        if (deathSound != null && audioSource != null)
        {   audioSource.PlayOneShot(deathSound);
            Debug.Log("Boss killed by shadow!");
        }
        Debug.Log("Boss disappeared!");
    }
    void OnTriggerEnter2D(Collider2D other)
    {   if (isDead || !isActive) return;
        if (other.CompareTag("Player"))
        { Debug.Log("Aria hit the Boss!");
            if (gameOverSound != null && audioSource != null)
            {audioSource.PlayOneShot(gameOverSound);}
            GameManager.Instance.GameOver();
        }
    }
    public void Respawn()
    {   isActive = true;
        isDead = false;
        spriteRenderer.enabled = true;
        bossCollider.enabled = true;
        transform.position = new Vector3(42f, fixedY, transform.position.z);
        direction = 1f;
        Debug.Log("Boss respawned!");
    }
    public bool IsDead()
    {return isDead;
    }
}