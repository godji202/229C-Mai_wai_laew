using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // ลากรูปหัวใจทั้ง 5 ดวงมาใส่ในช่องนี้ใน Inspector
    public Image[] hearts; 

    // ฟังก์ชันนี้ต้องชื่อ UpdateHearts ตรงเป๊ะๆ
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // ถ้าเลือด 500 ดวงที่ 0-4 จะเปิด (i < 5)
            // ถ้าเลือดลดเหลือ 400 ดวงที่ 4 จะปิด (i < 4)
            if (i < currentHealth / 100)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}