using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public GameObject damageEffect;

    private int MaxHealth = 3;
    public int currentHealth;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite FullHeartSprite;
    [SerializeField] private Sprite EmptyHeartSprite;

    private GameObject Player;

    //private void Awake()
    //{
    // Nếu đã có instance rồi thì destroy cái mới
    //if (instance != null && instance != this)
    //{
    // Destroy(gameObject);
    //return;
    //}
    //instance = this;
    //DontDestroyOnLoad(gameObject);
    //}

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        currentHealth = MaxHealth;
        DisplayHearts();
    }

    public void HurtPlayer()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            DisplayHearts();
            AudioManager.instance.PlayHurt();
        }

        if (currentHealth <= 0)
            GameManager.instance.Death();

        if (damageEffect != null && Player != null)
            Instantiate(damageEffect, Player.transform.position, Quaternion.identity);
    }

    public void DisplayHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue;
            hearts[i].sprite = (i < currentHealth) ? FullHeartSprite : EmptyHeartSprite;
        }
    }
}
