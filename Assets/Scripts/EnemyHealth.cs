using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string dieAnimation = "sk die";
    
    [Header("Drop System")]
    public GameObject healthPotionPrefab; // ลาก Prefab ของขวดยามาใส่ช่องนี้
    [Range(0, 100)] public float dropChance = 50f; // โอกาสดรอป (เช่น 50%)

    private bool isDead = false;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        if (isDead) return;
        isDead = true;

        // หยุดทุกอย่างเหมือนเดิม
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static; 
        }

        if (GetComponent<EnemyAI>() != null) GetComponent<EnemyAI>().enabled = false;
        if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

        if (anim != null) anim.Play(dieAnimation);

        // --- ส่วนที่เพิ่ม: ระบบสุ่มดรอป ---
        TryDropItem();

        Destroy(gameObject, 2.5f); 
    }

    void TryDropItem()
    {
        // สุ่มเลข 0-100
        float randomValue = Random.Range(0f, 100f);

        // ถ้าเลขที่สุ่มได้ น้อยกว่าโอกาสที่ตั้งไว้ ให้สร้างของ
        if (randomValue <= dropChance)
        {
            if (healthPotionPrefab != null)
            {
                // สร้างขวดยาตรงตำแหน่งที่มอนสเตอร์ตาย
                Instantiate(healthPotionPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}