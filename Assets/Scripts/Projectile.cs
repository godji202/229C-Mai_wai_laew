using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. ถ้าชนโดนศัตรู (เช็คจาก Tag)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // สั่งให้ศัตรูรับความเสียหาย/เล่นท่าตาย
            collision.gameObject.GetComponent<EnemyHealth>()?.TakeDamage();
            
            // ลบของที่ปาทิ้งทันที
            Destroy(gameObject);
        }
        
        // 2. ถ้าชนพื้น (ไม่ต้องทำอะไร ให้มันคาไว้บนพื้นตามที่คุณต้องการ)
        // แต่ถ้าปาแล้วอยากให้หายหลังจากผ่านไปสักพัก (กันรกแมพ) ให้เพิ่มบรรทัดล่างนี้:
        // Destroy(gameObject, 5f); 
    }
}