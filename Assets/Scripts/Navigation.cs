using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] int maxHealth = 4; // Maximum health of the enemy
    int currentHealth; // Current health of the enemy
    float lastAttackTime = 0;
    float attackCoolDown = 2;
    [SerializeField] float stoppingDistance;
    [SerializeField] float followDistance = 2f;
    [SerializeField] float stopFollowDistance = 5f;
    GameObject target;
    private NavMeshAgent agent;
    Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth; // Set current health to maximum health when the enemy spawns
    }

    // Update is called once per frame
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist < stoppingDistance)
        {
            StopEnemy();
            Attack();
        }
        else if (dist < followDistance)
        {
            GoToTarget();
        }
        else if (dist > stopFollowDistance)
        {
            StopEnemy();
        }
    }

    private void GoToTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        anim.SetBool("IsWalking", true);
    }

    private void StopEnemy()
    {
        agent.isStopped = true;
        anim.SetBool("IsWalking", false);
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCoolDown)
        {
            lastAttackTime = Time.time;
            anim.SetTrigger("Attack");
            SendMessageToPlayer("TakeDamage", damage);
        }
    }

    private void SendMessageToPlayer(string methodName, object value)
    {
        if (target != null)
        {
            target.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= Mathf.RoundToInt(damage); // Reduce the enemy's health by the damage amount
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement death behavior here, such as playing death animation, dropping loot, etc.
        Destroy(gameObject); // Destroy the enemy object when it dies
    }
}
