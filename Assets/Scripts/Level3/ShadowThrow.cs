using UnityEngine;

public class ShadowThrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject aimIndicator;
    [SerializeField] private Camera mainCamera;
    [Header("Settings")]
    [SerializeField] private KeyCode throwKey = KeyCode.E;
    [SerializeField] private float throwSpeed = 15f;
    [SerializeField] private float maxThrowDistance = 8f;
    [SerializeField] private float fixedY = -2.51f;
    [Header("Audio")]
    [SerializeField] private AudioClip hitBossSound;
    private bool isAiming = false;
    private bool isThrowing = false;
    private Vector3 targetPosition;
    private float throwTimer = 0f;
    private AudioSource audioSource;
    void Start()
    { audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        if (aimIndicator != null)
            aimIndicator.SetActive(false);
    }
    
    void Update()
    {if (Input.GetKeyDown(throwKey) && !isThrowing)
        {   StartAiming();
        }
        if (isAiming)
        {   UpdateAim();
        }
        if (Input.GetKeyUp(throwKey) && isAiming)
        {   ThrowShadow();
        }
        if (isThrowing)
        { MoveShadow();
        }
    }
    
    void StartAiming()
    {isAiming = true;
        if (aimIndicator != null)
            aimIndicator.SetActive(true);
        Vector3 startPos = transform.position + new Vector3(3, 0, 0);
        startPos.y = fixedY;
        aimIndicator.transform.position = startPos;
    }
    
    void UpdateAim()
    {Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = mousePos - transform.position;
        float distance = Mathf.Min(direction.magnitude, maxThrowDistance);
        targetPosition = transform.position + direction.normalized * distance;
        targetPosition.y = fixedY;
        aimIndicator.transform.position = targetPosition;
    }
    
    void ThrowShadow()
    {   isAiming = false;
        isThrowing = true;
        throwTimer = 0f;
        if (aimIndicator != null)
            aimIndicator.SetActive(false);
        shadow.SetActive(true);
        shadow.transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
        PlayerController shadowController = shadow.GetComponent<PlayerController>();
        if (shadowController != null)
            shadowController.enabled = false;
        Debug.Log("Shadow thrown to: " + targetPosition);
    }
    
    void MoveShadow()
    {   throwTimer += Time.deltaTime;
        Vector3 currentPos = shadow.transform.position;
        Vector3 targetPos = new Vector3(targetPosition.x, fixedY, targetPosition.z);
        shadow.transform.position = Vector3.MoveTowards(currentPos, targetPos, throwSpeed * Time.deltaTime);
        if (Vector3.Distance(shadow.transform.position, targetPos) < 0.1f)
        {  LandShadow();
        }
        if (throwTimer > 3f)
        {   LandShadow();
        }
    }
    
    void LandShadow()
    {   isThrowing = false;
        shadow.transform.position = new Vector3(targetPosition.x, fixedY, targetPosition.z); 
        PlayerController shadowController = shadow.GetComponent<PlayerController>();
        if (shadowController != null)
            shadowController.enabled = true;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(targetPosition, 0.5f);
        foreach (var hit in hitColliders)
        { if (hit.CompareTag("Boss"))
            {   Boss boss = hit.GetComponent<Boss>();
                if (boss != null)
                {   if (hitBossSound != null && audioSource != null)
                    {   audioSource.PlayOneShot(hitBossSound);
                        Debug.Log("Shadow hit the Boss! Sound played.");
                    }
                    boss.KillBoss();
                    Debug.Log("Shadow hit the Boss! Boss disappeared.");
                }
            }
        }
        
        Debug.Log("Shadow landed at: " + shadow.transform.position);
    }
}