using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] int maxHealth = 4; // Maximum health of the enemy
    int currentHealth; // Current health of the enemy
    float lastAttackTime = 0;
    float attackCoolDown = 2;
    [SerializeField] float stoppingDistance = 1f;
    [SerializeField] float followDistance = 5f;
    [SerializeField] float stopFollowDistance = 10f;
    GameObject target;
    private NavMeshAgent agent;
    Animator anim;
    private int count = 0;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth; // Set current health to maximum health when the enemy spawns

        // Inicia na animaçao de idle
        anim.SetBool("IsWalking", false);
        
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

        // Atualiza a anima��o de acordo com o estado do agente
        if (agent.velocity.magnitude > 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private void GoToTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    private void StopEnemy()
    {
        agent.isStopped = true;
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

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("nivel2");
    }
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("bullet"))
        {
            count = count + 1;
            Debug.Log(count);
            if(count == 5)
            {
                SceneManager.LoadScene("Nivel2");
            }
            
        }
    }
}
