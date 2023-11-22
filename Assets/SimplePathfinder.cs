using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class SimplePathfinder : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private Animator[] animators;
    private SpriteRenderer[] spriteRenderers;
    private static readonly int SpeedMultiplierAnimHash = Animator.StringToHash("SpeedMultiplier");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animators = GetComponentsInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        agent.SetDestination(target.position);
        Debug.Log(agent.speed + ", " + agent.desiredVelocity);

        foreach (var animator in animators)
        {
            animator.SetFloat(SpeedMultiplierAnimHash, agent.desiredVelocity.magnitude/agent.speed);
        }

        if (agent.desiredVelocity.x is < 0.1f and > -0.1f) return;
        foreach (var spriteRenderer in spriteRenderers)
            spriteRenderer.flipX = agent.desiredVelocity.x < 0;
    }
}