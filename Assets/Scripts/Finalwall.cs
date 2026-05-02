using UnityEngine;

public class Finalwall : MonoBehaviour
{
    public int wallHealth = 3; 

    public void TakeDamageFromStone()
    {
        wallHealth--;

        if (wallHealth <= 0)
        {
            Destroy(gameObject); 
        }
        else
        {
            GetComponent<SpriteRenderer>().color -= new Color(0.2f, 0.2f, 0.2f, 0);
        }
    }
}