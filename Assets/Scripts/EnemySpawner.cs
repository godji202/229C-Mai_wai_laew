using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;    // ลาก Prefab มอนสเตอร์มาใส่
    public Transform[] spawnPoints;  // ลากจุดเกิดทั้งหมดมาใส่

    void Start()
    {
        // เรียกใช้งานครั้งเดียวตอนเริ่มเกม
        SpawnAllEnemies();
    }

    void SpawnAllEnemies()
    {
        // เช็คความพร้อมของข้อมูล
        if (enemyPrefab == null) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        // วนลูปให้มอนสเตอร์เกิดตามจุด (Spawn Points) ทุกจุดที่เราลากใส่ไว้
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Instantiate(enemyPrefab, point.position, Quaternion.identity);
            }
        }
        
        // หลังจากเกิดครบแล้ว สคริปต์นี้จะไม่ทำอะไรต่อ (ไม่สปอนเพิ่ม)
    }
}