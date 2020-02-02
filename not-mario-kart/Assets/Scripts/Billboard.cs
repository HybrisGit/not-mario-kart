using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public float hoverHeight;

    private void Update()
    {
        Vector3 toCamera = Camera.main.transform.position - this.transform.parent.position;
        Vector3 dirToCamera = toCamera.normalized;
        this.transform.rotation = Quaternion.LookRotation(dirToCamera, Vector3.up);

        if (toCamera.magnitude > hoverHeight)
        {
            this.enabled = true;
            this.transform.position = this.transform.parent.position + dirToCamera * this.hoverHeight;
        }
        else
        {
            this.enabled = false;
        }
    }
}
