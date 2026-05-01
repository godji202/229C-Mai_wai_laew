using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // ลากตัว Golem มาใส่
    public float minY = 0f;     // ค่า Y ต่ำสุดที่ยอมให้กล้องลงไป (ปรับตามหน้างาน)
    public Vector3 offset = new Vector3(0, 0, -10); // ระยะห่างจากตัวละคร

    void LateUpdate()
    {
        if (player != null)
        {
            // ให้กล้องตามตัวละคร
            float targetY = player.position.y;

            // --- หัวใจสำคัญ: ล็อคไม่ให้กล้องต่ำกว่าพื้น ---
            // ถ้า targetY น้อยกว่า minY ให้ใช้ค่า minY แทน
            float clampedY = Mathf.Max(targetY, minY);

            // อัปเดตตำแหน่งกล้อง
            transform.position = new Vector3(player.position.x, clampedY, offset.z);
        }
    }
}