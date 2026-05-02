using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healAmount = 100; // เพิ่มเลือดทีละ 1 ดวง (100)

    void OnTriggerEnter2D(Collider2D other)
    {
        // เช็คว่าคนที่มาชนคือ Player ใช่ไหม
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // ถ้าเลือดไม่เต็ม ให้เพิ่มเลือด
            if (playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.Heal(healAmount);
                
                // เก็บยาแล้วให้ยาหายไป
                Destroy(gameObject);
                Debug.Log("เก็บยาฮีลแล้ว! เลือดเพิ่ม: " + healAmount);
            }
        }
    }
}