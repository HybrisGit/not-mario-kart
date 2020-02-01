using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [HideInInspector]
    public RaceTrack raceTrack;
    [HideInInspector]
    public Checkpoint nextCheckpoint;

    public void Setup(RaceTrack raceTrack, Checkpoint nextCheckpoint)
    {
        this.raceTrack = raceTrack;
        this.nextCheckpoint = nextCheckpoint;
    }
}
