using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {


    public List<Checkpoint> checkPoints = new List<Checkpoint>();
    [SerializeField]
    private float closingDistance = 3;
    public Checkpoint lastCheckpoint;
    public Transform player;
    public Action CheckPointChanged;

    private void Start()
    {
        PlayerHealth subscribe = GameObject.FindObjectOfType<PlayerHealth>();
        PlayerHealth.Death += Respawn;
        Checkpoint[] currents = GameObject.FindObjectsOfType<Checkpoint>();
        foreach(Checkpoint current in currents)
        {
            checkPoints.Add(current);
        }
    }

    private void Update()
    {
        CheckForClosePoint();
    }

    public void CheckForClosePoint()
    {
        foreach(Checkpoint point in checkPoints)
        {
            if (Vector3.Distance(point.checkPointPos, player.position) < closingDistance)
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
        player.transform.position = lastCheckpoint.checkPointPos;
        player.gameObject.GetComponent<PlayerHealth>().Heal();
    }

}
