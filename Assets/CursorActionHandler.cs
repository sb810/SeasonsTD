using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorActionHandler : MonoBehaviour
{
    [SerializeField] private Transform playerNavDestination;
    [SerializeField] private AStarPathfinder pathfinder;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void UpdateDestination(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button != PointerEventData.InputButton.Right) return;
        Vector3 pos = mainCamera.ScreenToWorldPoint(pointerData.position);
        playerNavDestination.position = new Vector3(pos.x, pos.y, 0);
        pathfinder.StartPath();
    }
}
