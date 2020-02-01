using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [HideInInspector]
    public Checkpoint[] checkpoints;

    public void Awake()
    {
        // get checkpoints
        this.checkpoints = this.GetComponentsInChildren<Checkpoint>();
        // register to checkpoints  
        foreach (Checkpoint checkpoint in this.checkpoints)
        {
            checkpoint.raceTrack = this;
        }
    }
}
