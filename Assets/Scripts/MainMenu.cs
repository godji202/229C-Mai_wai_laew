using UnityEngine;
using UnityEngine.SceneManagement; // ใช้สำหรับเปลี่ยนฉาก

public class MainMenu : MonoBehaviour
{
    // ฟังก์ชันสำหรับปุ่ม Start
    public void PlayGame()
    {
        // เปลี่ยน "GamePlay" เป็นชื่อ Scene ด่านแรกของคุณนะครับ
        SceneManager.LoadScene("Tutorial");
    }

    // ฟังก์ชันสำหรับปุ่ม Exit
    public void QuitGame()
    {
        Debug.Log("ออกจากเกมแล้ว!");

        // ส่วนนี้ทำให้กด Exit แล้วหยุดรันใน Unity Editor ได้เลย
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // ส่วนนี้ทำงานเมื่อ Build เป็นไฟล์เกม (.exe) แล้ว
            Application.Quit();
        #endif
    }
}