using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class SimpleWaypointNavigator: MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float speed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count == 0) return;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, speed);
        
        if (transform.position == waypoints[0].position)
            waypoints.RemoveAt(0);
    }
}