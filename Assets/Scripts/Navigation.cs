using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private int vidasIniciais = 4;
    [SerializeField] float damage;
    float lastAttackTime = 0;
    float attackCoolDown = 2;
    [SerializeField] float stoppingDistance;
    [SerializeField] float followDistance = 2f; // Distância mínima para começar a seguir o player
    [SerializeField] float stopFollowDistance = 5f; // Distância máxima para parar de seguir o player
    GameObject target;
    private NavMeshAgent agent;
    Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
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
            //target.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
