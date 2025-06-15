using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    [System.Serializable]
    public class Step
    {
        [TextArea]
        // Describe what happens in the step
        public string description;

        // declare where the robot should be
        public Robot robotInAction;
        public Robot.RobotPosition robotPos;

        // if there are Voicelines to be played, declare them here
        public AudioClip introductionToStep = null;
        public AudioClip item_correct = null;
        public AudioClip item_wrong = null; 
        public AudioClip endingToStep = null;

        // if an item needs to be put in a tonne, declare that here
        // if not, leave null: Step will auto-finish after voicelines
        public GameObject item = null;
        public GameObject tonne = null;
    }



    public List<Step> steps = new List<Step>();

    void Start()
    {

    }

    void Update()
    {
        if (steps.Count == 0)
        {
            // finished
            return;
        }

        Step current_step = steps[0];

        if (current_step.robotInAction != null && current_step.robotPos != null)
        {
            // if robot is somewhere else, bring him over for the next step
            if (current_step.robotPos != current_step.robotInAction.getPos())
            {
                current_step.robotInAction.moveTo(current_step.robotPos, false);
            }



            // if the robot is talking, dont do antyhing new
            if (current_step.robotInAction.isTalking())
            {
                return;
            }



            // if there is a starting voiceline declared, play it at the beginning
            if (current_step.introductionToStep != null)
            {
                current_step.robotInAction.talk(current_step.introductionToStep);
                current_step.introductionToStep = null;

                return;
            }



            // and wait for him to get where hes supposed to be
            if (!current_step.robotInAction.areYouThereYet())
            {
                return;
            }



            // if there is a item and tonne, wait until thats finished and do the voicelines
            if (current_step.item != null && current_step.tonne != null)
            {
                // todo
                // item spawnen, laufband auslösen, usw...
                // wenn item nicht einsortiert warten, sonst Voiceline für korrekt oder falsch abspielen
            }



            // if there is a finishing voiceline declared, play it at the end
            if (current_step.endingToStep != null)
            {
                current_step.robotInAction.talk(current_step.endingToStep);
                current_step.endingToStep = null;

                return;
            }

            // if we landed here, the step is officially done
            // delete it 
            steps.RemoveAt(0);
        }
        
    }
}
