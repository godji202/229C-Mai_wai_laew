using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ThrowableItem throwable = GetComponent<ThrowableItem>();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (throwable != null && throwable.isThrown)
            {
                collision.gameObject.GetComponent<EnemyHealth>()?.TakeDamage();
                Destroy(gameObject); 
            }
        }
        else if (collision.gameObject.CompareTag("Breakable"))
        {
            if (throwable != null && throwable.isThrown)
            {
                BreakableWall wall = collision.gameObject.GetComponent<BreakableWall>();
                if (wall != null)
                {
                    if (wall.wallHealth <= 1)
                    {
                        wall.TakeDamage();
                        Destroy(gameObject); 
                    }
                    else
                    {
                        wall.TakeDamage();
                    }
                }
            }
        }
        // แก้เป็น Finalwall (w เล็ก) ให้ตรงกับชื่อไฟล์ของคุณแล้วครับ
        else if (collision.gameObject.CompareTag("Finalwall"))
        {
            if (throwable != null && throwable.isThrown && throwable.isFinalStone)
            {
                Finalwall fWall = collision.gameObject.GetComponent<Finalwall>();
                if (fWall != null)
                {
                    if (fWall.wallHealth <= 1)
                    {
                        fWall.TakeDamageFromStone();
                        Destroy(gameObject); 
                    }
                    else
                    {
                        fWall.TakeDamageFromStone();
                    }
                }
            }
        }
    }
}