using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Controls { mobile, pc }

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private bool isGroundedBool = false;
    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;
    private bool isDead = false;

    public Animator playeranim;
    public Controls controlmode;

    private float moveX;
    public bool isPaused = false;

    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmissions;
    public ParticleSystem ImpactEffect;
    private bool wasonGround;

    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    [Header("Water")]
    public Tilemap waterTilemap;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footEmissions = footsteps.emission;
        if (controlmode == Controls.mobile)
            UIManager.instance.EnableMobileControls();
    }

    private void Update()
    {
        isGroundedBool = IsGrounded();

        if (isGroundedBool)
        {
            // Reset double jump khi chạm đất
            canDoubleJump = true;
            hasDoubleJumped = false;

            if (controlmode == Controls.pc)
                moveX = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump(jumpForce);
                AudioManager.instance.PlayJump();
            }
        }
        else
        {
            if (controlmode == Controls.pc)
                moveX = Input.GetAxis("Horizontal");

            // Double jump: CHỈ 1 lần khi trên không
            if (!hasDoubleJumped && canDoubleJump && Input.GetButtonDown("Jump"))
            {
                Jump(doubleJumpForce);
                hasDoubleJumped = true;
                canDoubleJump = false;
                AudioManager.instance.PlayJump();
            }
        }

        if (!isPaused)
        {
            if (controlmode == Controls.pc && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        SetAnimations();

        if (moveX != 0)
            FlipSprite(moveX);

        if (!wasonGround && isGroundedBool)
        {
            ImpactEffect.gameObject.SetActive(true);
            ImpactEffect.Stop();
            ImpactEffect.transform.position = new Vector2(footsteps.transform.position.x, footsteps.transform.position.y - 0.2f);
            ImpactEffect.Play();
        }
        wasonGround = isGroundedBool;

        CheckWater();
    }

    private void CheckWater()
    {
        if (waterTilemap == null || isDead) return;
        Vector3 footPosition = groundCheck.position; //  dùng chân
        Vector3Int cellPos = waterTilemap.WorldToCell(footPosition);
        //Vector3Int cellPos = waterTilemap.WorldToCell(transform.position);
        if (waterTilemap.HasTile(cellPos))
        {
            isDead = true;
            if (HealthManager.instance.currentHealth > 1)
            {
                HealthManager.instance.currentHealth--;
                HealthManager.instance.DisplayHearts();
                AudioManager.instance.PlayHurt();
                GameManager.instance.RespawnAtCheckpoint();
            }
            else
            {
                HealthManager.instance.HurtPlayer();
            }
        }
    }

    public void ResetDead() { isDead = false; }

    public void SetAnimations()
    {
        if (moveX != 0 && isGroundedBool)
        {
            playeranim.SetBool("run", true);
            footEmissions.rateOverTime = 35f;
        }
        else
        {
            playeranim.SetBool("run", false);
            footEmissions.rateOverTime = 0f;
        }
        playeranim.SetBool("isGrounded", isGroundedBool);
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (direction < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        if (controlmode == Controls.pc)
            moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump(float force)
    {
        isGroundedBool = false; //  chặn spam jump

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        playeranim.SetTrigger("jump");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Đang đứng trên: " + collision.gameObject.name);
    }

    //private void Jump(float force)
    //{
    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
    //    rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    //    playeranim.SetTrigger("jump");
    //}

    //private bool IsGrounded()
    //{
    //    float rayLength = 0.4f; // tăng từ 0.25 lên 0.4
    //    Vector2 rayOrigin = groundCheck.transform.position;
    //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
    //    Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red); // xem ray trong Scene view
    //    return hit.collider != null;
    //}
    private bool IsGrounded()
    {
        float rayLength = 0.25f; // ❗ giảm lại (0.4 đang quá dài)
        Vector2 origin = groundCheck.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);

        Debug.DrawRay(origin, Vector2.down * rayLength, Color.red);

        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "killzone")
            GameManager.instance.Death();
    }

    public void MobileMove(float value) { moveX = value; }

    public void MobileJump()
    {
        if (isGroundedBool)
        {
            Jump(jumpForce);
            AudioManager.instance.PlayJump();
        }
        else if (!hasDoubleJumped && canDoubleJump)
        {
            Jump(doubleJumpForce);
            hasDoubleJumped = true;
            canDoubleJump = false;
            AudioManager.instance.PlayJump();
        }
    }

    public void Shoot() { }
    public void MobileShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
