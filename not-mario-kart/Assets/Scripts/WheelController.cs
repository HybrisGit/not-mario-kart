using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelCollider Collider;
    public Transform Model;

    public void Update()
    {
        Vector3 position;
        Quaternion rotation;
        this.Collider.GetWorldPose(out position, out rotation);
        //Vector3 rotatedVector = new Vector3(0, 90, 0);


        this.Model.rotation = rotation;
    }
}
