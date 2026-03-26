using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum pickupType { coin, health }

    public pickupType pt;
    [SerializeField] GameObject PickupEffect;

    private bool collected = false; // Tránh collect 2 lần

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;
        if (!collision.CompareTag("Player")) return;

        collected = true;

        if (pt == pickupType.coin)
        {
            GameManager.instance.IncrementCoinCount();
            if (PickupEffect != null)
                Instantiate(PickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (pt == pickupType.health)
        {
            if (HealthManager.instance.currentHealth < 3)
            {
                HealthManager.instance.currentHealth++;
                HealthManager.instance.DisplayHearts();
            }
            if (PickupEffect != null)
                Instantiate(PickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
