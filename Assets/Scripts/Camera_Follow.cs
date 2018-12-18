using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_Follow : MonoBehaviour {

    public Transform target;

    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public float maxHeight = 50;
    public float minHeight = 10;


    void FixedUpdate () {

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        smoothedPos.x = 0;
        
        if(smoothedPos.y > maxHeight)
        {
            smoothedPos.y = maxHeight;
        }
        else if (smoothedPos.y < minHeight)
        {
            smoothedPos.y = minHeight;
        }

        transform.position = smoothedPos;
        //transform.LookAt(target);

	}
}
