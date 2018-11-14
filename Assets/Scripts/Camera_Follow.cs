using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    public Transform target;

    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public float maxHeight = 10;
    public float minHeight = -10;

    public float maxWidth = 10;
    public float minWidth = -10;


    void FixedUpdate () {

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        //smoothedPos.x = 0;

        /*
        if(smoothedPos.y > maxHeight)
        {
            smoothedPos.y = maxHeight;
        }
        else if (smoothedPos.y < minHeight)
        {
            smoothedPos.y = minHeight;
        }

        if (smoothedPos.x > maxWidth)
        {
            smoothedPos.x = maxWidth;
        }
        else if (smoothedPos.x < minWidth)
        {
            smoothedPos.x = minWidth;
        }
        */

        transform.position = new Vector3(Mathf.Clamp(smoothedPos.x, minWidth, maxWidth), Mathf.Clamp(smoothedPos.y, minHeight, maxHeight), smoothedPos.z);
        //transform.position = smoothedPos;

        //transform.LookAt(target);

    }
}
