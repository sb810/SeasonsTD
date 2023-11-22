using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWithDistanceToTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float fadeStartDistance;
    [SerializeField] private float fadeEndDistance;
    [SerializeField] private bool invertBehavior;

    private SpriteRenderer sr;
    private float baseAlpha;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseAlpha = sr.color.a;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        float alpha = Mathf.Clamp01(Mathf.InverseLerp(fadeStartDistance, fadeEndDistance, distance)) * baseAlpha;
        Color col = sr.color;
        sr.color = new Color(col.r, col.g, col.b, alpha);
    }
}
