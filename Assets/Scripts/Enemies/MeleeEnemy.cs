using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private LayerMask playerLayerMask;

    // References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;


    private float cooldownTimer = math.INFINITY;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update() 
    {
        cooldownTimer += Time.deltaTime;

        if (IsPlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !IsPlayerInSight();
        }
    }

    private bool IsPlayerInSight()
    {
        Vector2 origin = enemyCollider.bounds.center + transform.right * attackRange * transform.localScale.x;

        RaycastHit2D hit = Physics2D.BoxCast(origin, attackSize, 0, Vector2.left, 0, playerLayerMask);

        if (hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyCollider.bounds.center + transform.right * attackRange * transform.localScale.x, attackSize);
    }

    private void DamagePlayer()
    {
        if (IsPlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
