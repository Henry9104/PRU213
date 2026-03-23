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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>().gameObject;

        currentHealth = MaxHealth;
        DisplayHearts();
    }

    public void HurtPlayer()
    {
        if (currentHealth > 0)
        {
            currentHealth--;

            DisplayHearts();

            AudioManager.instance.PlayHurt(); // 🔊 âm thanh
        }

        if (currentHealth <= 0)
        {
            GameManager.instance.Death();
        }

        Instantiate(damageEffect, Player.transform.position, Quaternion.identity);
    }

    public void DisplayHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = FullHeartSprite;  // ❤️
            else
                hearts[i].sprite = EmptyHeartSprite; // 🖤
        }
    }
}