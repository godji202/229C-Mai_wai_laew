using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string dieAnimation = "golem die"; 
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

        // 1. หยุดฟิสิกส์ทันที ไม่ให้ไถล ไม่ให้ร่วง
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static; 
        }

        // 2. ปิด Collider ทันที
        if (GetComponent<Collider2D>() != null) 
            GetComponent<Collider2D>().enabled = false;

        // 3. (สำคัญมาก) ปิดสคริปต์เดินหรือสคริปต์อื่นๆ ในตัวศัตรู
        // เพื่อไม่ให้มันสั่งเล่นท่า Idle/Walk มาทับท่าตาย
        MonoBehaviour[] allScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            // ปิดทุกสคริปต์ยกเว้นอันนี้ (EnemyHealth)
            if (script != this) script.enabled = false;
        }

        // 4. สั่งเล่นท่าตาย (เช็คชื่อใน Animator ให้ตรงเป๊ะ)
        if (anim != null) 
        {
            anim.Play(dieAnimation);
        }

        // ลบตัวตนทิ้งหลังจากแอนิเมชันเล่นจบ
        Destroy(gameObject, 2.5f); 
    }
}