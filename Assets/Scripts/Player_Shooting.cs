using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{

    public GameObject laserPrefab;
    public Transform fireTransform;
    public float velocity;
    public float atkSpeed;

    public GameObject aim01;
    public GameObject aim02;

    private bool fired;
    private float atkTime;

    void Start()
    {
        fired = false;
        atkTime = 0;
    }

    void Update()
    {
        atkTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            fired = true;
            atkTime = 0;
            GameObject laserObj = Instantiate(laserPrefab, fireTransform.position, fireTransform.rotation);
            Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
            laserRB.velocity = fireTransform.forward * velocity;
        }

        if (atkTime > atkSpeed)
        {
            fired = false;
        }

        Transform t = fireTransform;
        Quaternion q = Quaternion.Euler(0f, 0f, 0f);
        t.rotation = t.rotation * q;

        Ray ray = new Ray(t.position, t.forward);
        Vector3 point1 = ray.origin + (ray.direction * 25f);
        Vector3 point2 = ray.origin + (ray.direction * 75f);
        
        aim01.transform.position = Camera.main.WorldToScreenPoint(point1);
        aim02.transform.position = Camera.main.WorldToScreenPoint(point2);

        //Debug.DrawLine(transform.position, point, Color.red);

        /*
        RaycastHit hit;
        Vector3 point;
        Ray ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward));
        point = 
        Debug.DrawLine(transform.position, point, Color.red);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        */

    }

}
