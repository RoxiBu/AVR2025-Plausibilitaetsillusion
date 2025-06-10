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
        atBioTonne,
        atTechnoTonne,
        atZeitRaumTonne,
        inTheBack,
        center
    }

    public Vector3 getCoordinates()
    {
        switch (pos)
        {
            case RobotPosition.hiddenInHallway:
                return Vector3.zero;
            case RobotPosition.atBioTonne:
                return new Vector3(-1, 1, 0);
            case RobotPosition.atTechnoTonne:
                return new Vector3(1, 1, 0);
            case RobotPosition.atZeitRaumTonne:
                return new Vector3(-1, -1, 0);
            case RobotPosition.inTheBack:
                return new Vector3(1, -1, 0);
            case RobotPosition.center:
                return new Vector3(0, 0, 0);
            default:
                return Vector3.zero;
        }
    }

    public void setPosition(RobotPosition new_pos)
    {
        pos = new_pos;

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

    private RobotPosition pos = RobotPosition.hiddenInHallway;




    void Start()
    {
        this.transform.position = getCoordinates();
    }



    void Update()
    {
        if (behave == Behaviour.plausible)
        {
            // animate hovering with speed factor
            float time = Time.time * 0.3f;
            float x = Mathf.PerlinNoise(time, 0.1f) - 0.5f;
            float y = Mathf.PerlinNoise(0.1f, time) - 0.5f;
            float z = Mathf.PerlinNoise(time, time) - 0.5f;
            // and strength factor
            Vector3 offset = Vector3.Scale(new Vector3(x, y, z), new Vector3(0.1f, 0.2f, 0.1f));
            transform.position = getCoordinates() + offset;
        }
    }
}
