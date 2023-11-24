using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Transform> potentialTargets;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int attackFrameDelay = 60;
    private int attackFrameTimer;

    private void Awake()
    {
        potentialTargets = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (potentialTargets.Count == 0) return;

        if (attackFrameTimer == 0)
        {
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile proj = instance.GetComponent<Projectile>();
            proj.SetTarget(potentialTargets[0]);
        }

        // Debug.Log(attackFrameTimer);
        
        attackFrameTimer++;
        attackFrameTimer %= attackFrameDelay;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        Debug.Log(other.tag);
        Debug.Log(other.CompareTag("Enemy"));
        if (!other.CompareTag("Enemy")) return;
        potentialTargets.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        potentialTargets.Remove(other.transform);
    }
}
