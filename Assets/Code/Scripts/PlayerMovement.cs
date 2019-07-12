using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool invert_Yaxis = true;
    public FloatReference forward_speed;
    public FloatReference mov_speed;
    public FloatReference maneuver_speed;
    public FloatReference max_angle_pitch;
    public FloatReference max_angle_roll;
    public FloatReference max_angle_yaw;

    public FloatReference clampHeightMax;
    public FloatReference clampHeightMin;

    public FloatReference clampWidthMax;
    public FloatReference clampWidthMin;


    private float input_Vertical;
    private float input_Horizontal;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
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

        transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, initialPos.x - clampWidthMin, initialPos.x + clampWidthMax), Mathf.Clamp(transform.position.y, initialPos.y - clampHeightMin, initialPos.y + clampHeightMax), transform.position.z);
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

    private void maneuverControls()
    {
        Quaternion target = Quaternion.Euler((input_Vertical * (invert_Yaxis ? 1 : -1) * max_angle_pitch), (input_Horizontal * max_angle_yaw), (input_Horizontal * -1 * max_angle_roll));

        transform.rotation = Quaternion.Slerp(transform.rotation, target, maneuver_speed * Time.deltaTime);
    }
}
