using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int frameDelay;

    private int frameTimer;
    private SpriteRenderer sr;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        sr.sprite = sprites[Mathf.FloorToInt(frameTimer / frameDelay) % sprites.Length];
        frameTimer++;
    }
}
