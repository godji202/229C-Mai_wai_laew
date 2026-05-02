using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อเปลี่ยนหน้าจอ

public class FinishPoint : MonoBehaviour
{
    // ตั้งชื่อซีนจบที่ต้องการจะไป (ต้องตรงกับชื่อซีนใน Unity)
    public string endingSceneName = "EndGame"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เช็คว่าคนที่เดินมาชนคือ Player ใช่ไหม
        if (collision.CompareTag("Player"))
        {
            Debug.Log("เข้าสู่แสงสว่างแล้ว!");
            SceneManager.LoadScene(endingSceneName);
        }
    }
}