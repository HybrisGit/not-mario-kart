using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    [HideInInspector]
    public Checkpoint[] checkpoints;

    public static RaceTrack Instance;

    public void Awake()
    {
        Instance = this;

        // get checkpoints
        this.checkpoints = this.GetComponentsInChildren<Checkpoint>();
        // setup checkpoints
        for (int i = 0; i < this.checkpoints.Length; ++i)
        {
            this.checkpoints[i].Setup(this, i == this.checkpoints.Length - 1 ? this.checkpoints[0] : this.checkpoints[i + 1]);
        }
    }
}
