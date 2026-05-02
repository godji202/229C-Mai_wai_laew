using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ลากตัวละคร Golem_Player มาใส่
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // กำหนดขอบเขตกล้อง
    public float minY = -2f; // ค่าต่ำสุดที่กล้องจะลงไปได้ (ปรับเพื่อไม่ให้ทะลุพื้น)
    public float minX = 0f;  // ค่าซ้ายสุดของแมพ

    void LateUpdate()
    {
        if (target == null) return;

        // คำนวณตำแหน่งที่กล้องควรจะเป็น
        Vector3 desiredPosition = target.position + offset;
        
        // ล็อคค่า Y ไม่ให้ต่ำกว่าที่เรากำหนด (กันทะลุพื้น)
        float clampedY = Mathf.Max(desiredPosition.y, minY);
        // ล็อคค่า X ไม่ให้ย้อนกลับไปซ้ายสุด
        float clampedX = Mathf.Max(desiredPosition.x, minX);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);
        
        // ทำให้กล้องเคลื่อนที่นุ่มนวล
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
}