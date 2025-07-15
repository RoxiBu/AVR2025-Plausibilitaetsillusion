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
                return new Vector3(-4.36f, 1.06f, 15.0f);
            case RobotPosition.behindDoorInHallway:
                return new Vector3(-4.36f, 0.76f, 7.2f);
            case RobotPosition.inFrontOfDoor:
                return new Vector3(-1.15f, 0.74f, 7.2f);
            case RobotPosition.atBioTonne:
                return new Vector3(0.33f, 1.02f, 1.7f);
            case RobotPosition.atTechnoTonne:
                return new Vector3(0.88f, 1.0f, 1.61f);
            case RobotPosition.atZeitRaumTonne:
                return new Vector3(1.45f, 1.05f, 1.54f);
            case RobotPosition.atTonnen:
                return new Vector3(0.88f, 1.1f, 2.4f);
            case RobotPosition.inTheBack:
                return new Vector3(1.0f, 1.15f, 7.11f);
            case RobotPosition.center:
                return new Vector3(0.35f, 1.14f, 4.77f);
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
            current_pos = getCoordinates();
            this.transform.position = current_pos;
        }
        else
        {
            // animate movement in Update()
        }
    }

    public bool areYouThereYet() 
    {
        return Vector3.Distance(current_pos, getCoordinates()) < 0.07f;
    }

    private RobotPosition target_pos = RobotPosition.hiddenInHallway;
    private bool lookingAtPlayer = false;
    public Transform playerCamera;





    public GameObject head;

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
    private Vector3 head_turning_velocity = Vector3.zero;

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
                float smoothTime = 1f;
                float maxSpeed = 5f;
                current_pos = Vector3.SmoothDamp(current_pos, desired_pos, ref movement_velocity, smoothTime, maxSpeed, Time.deltaTime);
                direction_looking = desired_pos - current_pos;
            }
            else // else look at the player
            {
                Vector3 playerPos = playerCamera.position;
                playerPos.y -= 0.8f;
                direction_looking = playerPos - head.transform.position;
            }

            float stiffness = 10f;
            float damping = 4f;

            {
                // now look where youre supposed to - smoothly
                Quaternion targetRotation = Quaternion.LookRotation(direction_looking);
                Quaternion deltaRotation = targetRotation * Quaternion.Inverse(head.transform.rotation);
                deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
                if (angle > 180f) angle -= 360f;

                Vector3 torque = axis.normalized * Mathf.Deg2Rad * angle * stiffness;
                Vector3 dampingForce = turning_velocity * damping;

                Vector3 angularAccel = torque - dampingForce;
                turning_velocity += angularAccel * Time.deltaTime;

                Quaternion deltaQuat = Quaternion.Euler(turning_velocity * Mathf.Rad2Deg * Time.deltaTime);
                head.transform.rotation = deltaQuat * head.transform.rotation;

                // and turn the body slightly too
                deltaQuat.x *= 0.2f;
                deltaQuat.y *= 0.7f;
                deltaQuat.z *= 0.2f;
                transform.rotation = deltaQuat * transform.rotation;
            }


            // and animate hovering with speed factor (time) and strength 
            {
                float time = Time.time * 0.6f;
                float x = Mathf.PerlinNoise(time, 0.1f) - 0.5f;
                float y = Mathf.PerlinNoise(0.1f, time) - 0.5f;
                float z = Mathf.PerlinNoise(time, time) - 0.5f;

                Vector3 noiseOffset = new Vector3(x, y, z);
                Vector3 hoverStrength = new Vector3(0.1f, 0.2f, 0.1f);
                transform.position = current_pos + Vector3.Scale(noiseOffset, hoverStrength);
            }

        }
        else // non-plausible robot
        { 
            
        }
       
       
        //bot Robots:
        // randomly turn the head slighty
        /*float noiseTime = Time.time * 0.7f;
        float nx = Mathf.PerlinNoise(noiseTime, 0.1f) - 0.5f;
        float ny = Mathf.PerlinNoise(0.1f, noiseTime) - 0.5f;
        float nz = Mathf.PerlinNoise(noiseTime, noiseTime) - 0.5f;

        Vector3 noiseEuler = new Vector3(nx * 0.1f, ny * 0.65f, nz * 0.1f); 
        Quaternion noiseRotation = Quaternion.Euler(noiseEuler);

        head.transform.rotation = noiseRotation * head.transform.rotation;
        */
    }
}
