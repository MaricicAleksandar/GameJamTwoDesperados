using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Enemy aggroDetection;
    float currentSpeed = 0;
    private Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        aggroDetection = GetComponent<Enemy>();
        aggroDetection.OnAggro += Enemy_OnAggro;
    }

    private void Enemy_OnAggro(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if(target != null)
        {
            navMeshAgent.SetDestination(target.position);
            float currentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
        }
    }
}
