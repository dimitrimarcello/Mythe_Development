using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {


    public List<Checkpoint> checkPoints = new List<Checkpoint>();
    [SerializeField]
    private float closingDistance = 3;
    [SerializeField]
    public Checkpoint lastCheckpoint;
    public Transform player;
    public Action CheckPointChanged;

    private void Start()
    {
        PlayerHealth subscribe = GameObject.FindObjectOfType<PlayerHealth>();
        PlayerHealth.Death += Respawn;
        Checkpoint[] currents = GameObject.FindObjectsOfType<Checkpoint>();

        float distance = 10000;

        foreach(Checkpoint current in currents)
        {
            float dis = Vector2.Distance(player.transform.position, current.transform.position);
            if (dis < distance)
            {
                lastCheckpoint = current;
                distance = dis;
            }
            checkPoints.Add(current);
        }

        
    }

    bool respawn = false;

    private void Update()
    {

        if (respawn)
        {
            player = FindObjectOfType<PlayerHealth>().transform;
            player.transform.position = lastCheckpoint.transform.position;
            player.gameObject.GetComponent<PlayerHealth>().Heal();
            respawn = false;
        }

        CheckForClosePoint();
    }

    public void CheckForClosePoint()
    {
        foreach(Checkpoint point in checkPoints)
        {
            if (Vector3.Distance(point.transform.position, player.position) < closingDistance)
            {
                lastCheckpoint = point;
                if(CheckPointChanged != null)
                {
                    CheckPointChanged();
                }
            }
        }
    }

    private void Respawn()
    {

        respawn = true;
    }

}
