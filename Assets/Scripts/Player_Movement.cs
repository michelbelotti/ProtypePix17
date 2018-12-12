using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    //[SerializeField]
    //float angle;

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

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

        input_Vertical = Input.GetAxis("Vertical");
        input_Horizontal = Input.GetAxis("Horizontal");

        MoveVertAxis();
        MoveHorAxis();

        maneuverControls();

        transform.position += transform.forward * forward_speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampWidthMin, clampWidthMax), Mathf.Clamp(transform.position.y, clampHeightMin, clampHeightMax), transform.position.z);
    }

    private void MoveVertAxis()
    {
        Vector3 direction = new Vector3(0, input_Vertical, 0);
        
        transform.position += direction * mov_speed * Time.fixedDeltaTime;
    }

    private void MoveHorAxis()
    {
        Vector3 direction = new Vector3(input_Horizontal, 0, 0);
        //Vector3 direction = new Vector3(input_Horizontal, 0, 0);
        
        transform.position += direction * mov_speed * Time.fixedDeltaTime;
        //transform.position += direction * mov_speed * Time.fixedDeltaTime;

    }



    private void maneuverControls()
    {
        Quaternion target = Quaternion.Euler((input_Vertical * -1 * max_angle_pitch), (input_Horizontal * max_angle_roll), (input_Horizontal * -1 * max_angle_yaw));

        transform.rotation = Quaternion.Slerp(transform.rotation, target, maneuver_speed * Time.deltaTime);
    }
}
