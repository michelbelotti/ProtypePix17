using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public bool invert_Yaxis = true;
    public float forward_speed = 20;
    public float mov_speed = 25;
    public float maneuver_speed = 5.0f;
    public float max_angle_pitch = 25f;
    public float max_angle_roll = 25f;
    public float max_angle_yaw = 15f;

    public float clampHeightMax = 10f;
    public float clampHeightMin = -10f;

    public float clampWidthMax = 10f;
    public float clampWidthMin = -10f;


    private float input_Vertical;
    private float input_Horizontal;

    private float[] clampHeight;
    private float[] clampWidth;

    private void Start()
    {
        clampHeight = new float[] { transform.position.y + clampHeightMin, transform.position.x + clampHeightMax };
        clampWidth = new float[] { transform.position.y + clampWidthMin, transform.position.x + clampWidthMax };

        Debug.Log("clampHeightMin: " + clampHeight[0]);
        Debug.Log("clampHeightMax: " + clampHeight[1]);
        Debug.Log("clampWidthMin: " + clampWidth[0]);
        Debug.Log("clampWidthMax: " + clampWidth[1]);
    }

    private void FixedUpdate()
    {

        input_Vertical = Input.GetAxis("Vertical");
        input_Horizontal = Input.GetAxis("Horizontal");

        MoveVertAxis();
        MoveHorAxis();

        ManeuverControls();

        //transform.position += transform.forward * forward_speed * Time.deltaTime;
        transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampWidth[0], clampWidth[1]), Mathf.Clamp(transform.position.y, clampHeight[0], clampHeight[1]), transform.position.z);
    }

    private void MoveVertAxis()
    {
        Vector3 direction = new Vector3(0, input_Vertical, 0);
        
        transform.position += (invert_Yaxis ? -1 : 1) * direction * mov_speed * Time.fixedDeltaTime;
    }

    private void MoveHorAxis()
    {
        Vector3 direction = new Vector3(input_Horizontal, 0, 0);
        
        transform.position += direction * mov_speed * Time.fixedDeltaTime;
    }



    private void ManeuverControls()
    {
        Quaternion target = Quaternion.Euler((input_Vertical * (invert_Yaxis ? 1 : -1) * max_angle_pitch), (input_Horizontal * max_angle_yaw), (input_Horizontal * -1 * max_angle_roll));

        transform.rotation = Quaternion.Slerp(transform.rotation, target, maneuver_speed * Time.deltaTime);
    }
}
