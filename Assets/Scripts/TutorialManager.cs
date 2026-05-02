using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    void Update()
    {
        // เช็คว่ากดเมาส์ซ้ายไหม
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("C1"); // ชื่อฉากต้องตรงกับใน Build Settings
        }
    }
}