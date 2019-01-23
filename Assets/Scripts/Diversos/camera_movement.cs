using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour {

    Vector3 pos;

    // Use this for initialization
    void Start()
    {
        pos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = transform.position;
        temp.x = pos.x;
        temp.y = pos.y;
        transform.position = temp;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}
}
