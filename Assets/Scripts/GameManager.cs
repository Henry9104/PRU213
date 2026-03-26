using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private bool isRespawning = false;

    private Vector3 checkpointPosition;

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;
    private int totalCoins = 0;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (SaveManager.HasSave())
        {
            // Load tiến trình cũ
            coinCount = SaveManager.LoadCoins();
            HealthManager.instance.currentHealth = SaveManager.LoadHealth();
            HealthManager.instance.DisplayHearts();
            playerController.transform.position = SaveManager.LoadPosition();
        }
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        checkpointPosition = playerController.transform.position;
        FindTotalPickups();
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        Debug.Log("Checkpoint saved at: " + position);
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
        AudioManager.instance.PlayCoin();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
    }

    // ── RESPAWN còn tim (rớt nước) ─────────────────────────────────────────────
    public void RespawnAtCheckpoint()
    {
        if (isRespawning) return;
        isRespawning = true;
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // 1. Tắt input player ngay
        Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        // 2. Fade tối
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(1f);

        // 3. Teleport
        playerController.transform.position = checkpointPosition;
        rb.linearVelocity = Vector2.zero;

        // 4. Đợi 1 frame để Unity cập nhật vị trí
        yield return null;
        yield return null;

        // 5. Bật gravity lại
        rb.gravityScale = 4f;

        // 6. Fade sáng
        UIManager.instance.fadeToBlack = false;
        UIManager.instance.fadeFromBlack = true;

        // 7. Đợi fade xong RỒII mới cho CheckWater hoạt động
        yield return new WaitForSeconds(1f);
        playerController.ResetDead();
        isRespawning = false;
    }

    // ── DEATH hết tim ──────────────────────────────────────────────────────────
    public void Death()
    {
        if (isRespawning) return;
        isRespawning = true;

        UIManager.instance.DisableMobileControls();
        UIManager.instance.fadeToBlack = true;
        playerController.gameObject.SetActive(false);
        AudioManager.instance.PlayDeath();

        StartCoroutine(DeathCoroutine());
        Debug.Log("Player Died");
    }

    //private IEnumerator DeathCoroutine()
    //{
    //    yield return new WaitForSeconds(1f);

    //    Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
    //    rb.linearVelocity = Vector2.zero;
    //    rb.gravityScale = 0f;

    //    playerController.transform.position = checkpointPosition;
    //    playerController.gameObject.SetActive(true);

    //    yield return null;
    //    yield return null;

    //    rb.gravityScale = 4f;

    //    // Restore health
    //    HealthManager.instance.currentHealth = 3;
    //    HealthManager.instance.DisplayHearts();

    //    UIManager.instance.fadeToBlack = false;
    //    UIManager.instance.fadeFromBlack = true;

    //    if (playerController.controlmode == Controls.mobile)
    //        UIManager.instance.EnableMobileControls();

    //    yield return new WaitForSeconds(1f);
    //    playerController.ResetDead();
    //    isRespawning = false;
    //}
    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("End Game");
    }

    public void FindTotalPickups()
    {
        pickup[] pickups = GameObject.FindObjectsByType<pickup>(FindObjectsSortMode.None);
        foreach (pickup p in pickups)
            if (p.pt == pickup.pickupType.coin)
                totalCoins++;
    }

    public void LevelComplete()
    {
        // Dừng player
        playerController.enabled = false;
        Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "LEVEL COMPLETE";
        levelCompleteCoins.text = "COINS COLLECTED: " + coinCount + " / " + totalCoins;
    }
}
