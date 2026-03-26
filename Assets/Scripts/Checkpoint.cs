using UnityEngine;

/// <summary>
/// Gắn script này vào GameObject Checkpoint trong scene.
/// Thêm Collider2D (Is Trigger = true) vào cùng GameObject.
/// Khi player chạm vào, vị trí respawn sẽ được cập nhật.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    [Header("Visual")]
    public Animator checkpointAnim;         // (tuỳ chọn) Animator của cờ/biển checkpoint
    public string activateAnimTrigger = "activate"; // Tên trigger animation

    [Header("Effect")]
    public GameObject activateEffect;       // (tuỳ chọn) particle khi kích hoạt

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;

            // Lưu vị trí respawn vào GameManager
            GameManager.instance.SetCheckpoint(transform.position);

            // Phát animation (nếu có)
            if (checkpointAnim != null)
                checkpointAnim.SetTrigger(activateAnimTrigger);

            // Phát hiệu ứng (nếu có)
            if (activateEffect != null)
                Instantiate(activateEffect, transform.position, Quaternion.identity);

            Debug.Log("Checkpoint activated at: " + transform.position);
        }
    }
}
