using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;
    public int damage = 100;

    [Header("Animations")]
    public string walkAnim = "sk walk"; // ปรับชื่อให้ตรงกับ Animator
    public string idleAnim = "sk idle";
    public string attackAnim = "sk attack";

    private Animator anim;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float lastAttackTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (player == null) player = GameObject.Find("Golem_Player")?.transform;
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackPlayer());
            }
            else
            {
                StopMoving();
            }
        }
        else if (distance <= chaseRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // กลับด้านตัวละครตามทิศทางที่เดิน
        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        anim.Play(walkAnim);
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.Play(idleAnim);
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;
        
        anim.Play(attackAnim);
        lastAttackTime = Time.time;

        // เช็คระยะอีกครั้งก่อนลดเลือด
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        yield return new WaitForSeconds(1.0f); // รอให้ท่าตีเล่นจบ
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}