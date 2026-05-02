using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int wallHealth = 2; // ตั้งค่าเลือดกำแพงไว้ 2

    // ฟังก์ชันสำหรับรับดาเมจจากหิน
    public void TakeDamage()
    {
        wallHealth--; // ลดเลือดลงทีละ 1

        if (wallHealth <= 0)
        {
            Destroy(gameObject); // เลือดหมดกำแพงแตก
        }
        else
        {
            // (Optional) อาจจะเปลี่ยนสีกำแพงให้เข้มขึ้น หรือสั่น เพื่อให้รู้ว่าโดนแล้ว 1 ที
            GetComponent<SpriteRenderer>().color = Color.gray; 
        }
    }
}