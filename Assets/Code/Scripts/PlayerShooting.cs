using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject projectileType;
    public Transform cannonTransform;

    public FloatReference atkSpeed;

    public GameObject aim01;
    public GameObject aim02;

    public FloatReference aim01Length;
    public FloatReference aim02Length;

    private bool fired;
    private float atkTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Aim();
    }

    void Shoot()
    {

        atkTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            fired = true;
            atkTime = 0;
            GameObject laserObj = Instantiate(projectileType, cannonTransform.position, cannonTransform.rotation);
        }

        if (atkTime > atkSpeed)
        {
            fired = false;
        }

    }

    void Aim()
    {
        Transform t = cannonTransform;
        Quaternion q = Quaternion.Euler(0f, 0f, 0f);
        t.rotation = t.rotation * q;

        Ray ray = new Ray(t.position, t.forward);
        Vector3 point1 = ray.origin + (ray.direction * aim01Length);
        Vector3 point2 = ray.origin + (ray.direction * aim02Length);
        aim01.transform.position = Camera.main.WorldToScreenPoint(point1);
        aim02.transform.position = Camera.main.WorldToScreenPoint(point2);

        Debug.DrawLine(transform.position, point1, Color.yellow);
        //Debug.DrawLine(transform.position, point2, Color.magenta);
    }
}
