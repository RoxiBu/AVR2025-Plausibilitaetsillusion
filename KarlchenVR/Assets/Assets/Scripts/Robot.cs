using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Robot : MonoBehaviour
{

    // differentiate robot behaviour in this script
    public enum Behaviour
    {
        plausible,
        not_plausible
    }

    public Behaviour behave = Behaviour.plausible;



    // positions where the robot can be
    public enum RobotPosition
    {
        hiddenInHallway,
        behindDoorInHallway,
        inFrontOfDoor,
        atBioTonne,
        atTechnoTonne,
        atZeitRaumTonne,
        atTonnen,
        inTheBack,
        center
    }

    private Vector3 getCoordinates()
    {
        switch (target_pos)
        {
            case RobotPosition.hiddenInHallway:
                return new Vector3(-4.36f, 1.36f, 15.0f);
            case RobotPosition.behindDoorInHallway:
                return new Vector3(-4.36f, 1.06f, 7.2f);
            case RobotPosition.inFrontOfDoor:
                return new Vector3(-1.15f, 1.04f, 7.2f);
            case RobotPosition.atBioTonne:
                return new Vector3(0.33f, 1.32f, 1.7f);
            case RobotPosition.atTechnoTonne:
                return new Vector3(0.88f, 1.3f, 1.61f);
            case RobotPosition.atZeitRaumTonne:
                return new Vector3(1.45f, 1.25f, 1.54f);
            case RobotPosition.atTonnen:
                return new Vector3(0.88f, 1.3f, 2.4f);
            case RobotPosition.inTheBack:
                return new Vector3(1.0f, 1.15f, 7.11f);
            case RobotPosition.center:
                return new Vector3(0.35f, 1.44f, 4.77f);
            default:
                return Vector3.zero;
        }
    }

    public RobotPosition getPos()
    {
        return target_pos;
    }

    public void moveTo(RobotPosition new_pos, bool new_lookingAtPlayer)
    {
        target_pos = new_pos;
        lookingAtPlayer = new_lookingAtPlayer;

        if (behave == Behaviour.not_plausible)
        {
            // teleport instantly
            this.transform.position = getCoordinates();
        }
        else
        {
            // animate movement
            // todo 
        }
    }

    public bool areYouThereYet() 
    {
        return Vector3.Distance(current_pos, getCoordinates()) < 0.07f;
    }

    private RobotPosition target_pos = RobotPosition.hiddenInHallway;
    private bool lookingAtPlayer = false;
    public Transform playerCamera;







    public AudioSource mouth;

    public void talk(AudioClip voiceline)
    {
        mouth.clip = voiceline;
        mouth.Play();
    }

    public bool isTalking()
    {
        return mouth.isPlaying;
    }





    private Vector3 current_pos = Vector3.zero;
    private Vector3 movement_velocity = Vector3.zero;
    private Vector3 turning_velocity = Vector3.zero;

    void Start()
    {
        this.transform.position = getCoordinates();
        current_pos = getCoordinates();
    }

    void Update()
    {
        if (behave == Behaviour.plausible)
        {

            Vector3 desired_pos = getCoordinates();
            Vector3 direction_looking = Vector3.forward;

            // if the position isnt the desired one, move there and look in that direction
            if (!areYouThereYet())
            {
                //Vector3 direction = current_pos - desired_pos;
                //current_pos = Vector3.Slerp(current_pos, desired_pos, moveSpeed * Time.deltaTime);
                
                float smoothTime = 1f;
                float maxSpeed = 5f;
                current_pos = Vector3.SmoothDamp(current_pos, desired_pos, ref movement_velocity, smoothTime, maxSpeed, Time.deltaTime);

                direction_looking = current_pos - desired_pos;
            }
            else // else just roughly look at player
            {
                direction_looking = current_pos - playerCamera.position;
            }

            // now look where youre supposed to - smoothly
            float stiffness = 10f;
            float damping = 4f;
            Quaternion targetRotation = Quaternion.LookRotation(direction_looking);
            Quaternion deltaRotation = targetRotation * Quaternion.Inverse(transform.rotation);
            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
            if (angle > 180f) angle -= 360f;

            Vector3 torque = axis.normalized * Mathf.Deg2Rad * angle * stiffness;
            Vector3 dampingForce = turning_velocity * damping;

            Vector3 angularAccel = torque - dampingForce;
            turning_velocity += angularAccel * Time.deltaTime;

            Quaternion deltaQuat = Quaternion.Euler(turning_velocity * Mathf.Rad2Deg * Time.deltaTime);
            transform.rotation = deltaQuat * transform.rotation;

            // and animate hovering with speed factor (time) and strength factor (Vector3.scale)
            float time = Time.time * 0.6f;
            float x = Mathf.PerlinNoise(time, 0.1f) - 0.5f;
            float y = Mathf.PerlinNoise(0.1f, time) - 0.5f;
            float z = Mathf.PerlinNoise(time, time) - 0.5f;

            Vector3 offset = Vector3.Scale(new Vector3(x, y, z), new Vector3(0.1f, 0.2f, 0.1f));
            transform.position = current_pos + offset;
            //transform.position = current_pos;
        }
    }
}
