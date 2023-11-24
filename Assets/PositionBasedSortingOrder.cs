using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PositionBasedSortingOrder : MonoBehaviour
{
    [SerializeField] private bool isStatic;
    private SpriteRenderer sr;
    private int initialOffset;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        initialOffset = sr.sortingOrder;
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStatic) UpdatePosition();
    }

    private void UpdatePosition()
    {
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y*32) + initialOffset;
    }
}
