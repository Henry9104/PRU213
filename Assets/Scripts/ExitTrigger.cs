using System.Collections;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LevelExit(collision.gameObject));
        }
    }

    IEnumerator LevelExit(GameObject player)
    {
        // 1. Dừng player hoàn toàn
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.enabled = false;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        // 2. Đợi chút
        yield return new WaitForSeconds(0.3f);

        // 3. Fade tối nhanh
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(0.8f);

        // 4. Hiện Level Complete
        GameManager.instance.LevelComplete();

        // 5. Fade sáng lại
        UIManager.instance.fadeToBlack = false;
        UIManager.instance.fadeFromBlack = true;
    }
}