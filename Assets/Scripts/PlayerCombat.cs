using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public string attackAnimation = "golem attack";
    public float attackTime = 0.5f; 
    public Transform attackPoint;    
    public float attackRange = 0.5f; 
    public LayerMask enemyLayer;     

    private Animator anim;
    private PlayerMove moveScript;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !moveScript.isAttacking)
        {
            StartCoroutine(PlayAttack());
        }
    }

    IEnumerator PlayAttack()
    {
        moveScript.isAttacking = true; 
        rb.linearVelocity = Vector2.zero; // หยุดกึกทันที
        
        anim.Play(attackAnimation);    

        // เช็คศัตรูในระยะ
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage();
        }
        
        yield return new WaitForSeconds(attackTime); 
        moveScript.isAttacking = false; 
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}