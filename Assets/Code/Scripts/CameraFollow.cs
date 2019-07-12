using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public float maxHeight = 100;
    public float minHeight = 100;

    public float maxWidth = 100;
    public float minWidth = 100;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = target.position;
    }

    void FixedUpdate () {

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        transform.position = new Vector3(Mathf.Clamp(smoothedPos.x, (initialPos.x - minWidth), (initialPos.x + maxWidth)), Mathf.Clamp(smoothedPos.y, (initialPos.y - minHeight), (initialPos.y + maxHeight)), smoothedPos.z);
        transform.LookAt(target);
    }
}
