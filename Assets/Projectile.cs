using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Sprite[] angledSprites;
    [SerializeField] private Transform target;
    
    private SpriteRenderer sr;
    private bool dying;
    private Vector3 lastPosition;
    private static readonly int IsDeadAnimHash = Animator.StringToHash("IsDead");


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target)
            lastPosition = target.position;

        if (dying) return;
        Vector3 pos = transform.position;
        Vector3 tPos = lastPosition;
        transform.position = Vector3.MoveTowards(pos, tPos, speed);
        Vector3 dir = tPos - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < 0) angle += 360;

        int spriteIndex = Mathf.FloorToInt(Mathf.Lerp(angledSprites.Length, 0, (angle % 90) / 90));
        int rotationMult = Mathf.FloorToInt(angle / 90) * 90;

        Sprite spr = angledSprites[spriteIndex];
        sr.sprite = spr;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationMult));
        if (transform.position == lastPosition)
        {
            if(target)
                KillEnemy(target);
            else Destroy(gameObject);
        }
    }

    private void KillEnemy(Transform enemy)
    {
        enemy.GetComponent<Animator>().SetBool(IsDeadAnimHash, true);
        enemy.GetComponent<AStarPathfinder>().SetActive(false);
        dying = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        KillEnemy(other.transform);
    }
}