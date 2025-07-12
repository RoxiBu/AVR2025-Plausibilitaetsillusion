using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    [System.Serializable]
    public class Step
    {
        private string description;

        private Robot robotInAction;
        private Robot.RobotPosition robotPos;

        // wenn eine VoiceLine != null ist, dann werde ich sie an der entsprechenden Stelle auslösen
        // wenn eine VoiceLine = null ist, werde ich nichts sagen und einfach weiter machen
        private AudioClip introductionToStep = null;
        private AudioClip item_correct = null;
        private AudioClip item_wrong = null;

        // wenn item != null, muss die Person das Item noch in die richtige Tonne tun. Ich warte, bis das passiert ist
        // wenn item = null, sehe ich es als erledigt an. Ich warte nicht und führe fort
        private GameObject item = null;
        private GameObject tonne = null; // ist quasi unnötig im Moment
        private int timesThrewWrong = 0;

        // setter
        public Step setDescription(string desc)
        {
            this.description = desc;
            return this;
        }

        public Step theRobot(Robot robot)
        {
            this.robotInAction = robot;
            return this;
        }

        public Step isHere(Robot.RobotPosition pos)
        {
            this.robotPos = pos;
            return this;
        }

        public Step saying(AudioClip clip)
        {
            this.introductionToStep = clip;
            return this;
        }

        public Step waitingForItem(GameObject item)
        {
            this.item = item;
            return this;
        }

        public Step toBeThrownInTonne(GameObject tonne)
        {
            this.tonne = tonne;
            return this;
        }

        public Step saysOnCorrectTonne(AudioClip clip)
        {
            this.item_correct = clip;
            return this;
        }

        public Step saysOnWrongTonne(AudioClip clip)
        {
            this.item_wrong = clip;
            return this;
        }

        public Step threwInRight()
        {
            if (item_correct != null)
            {
                robotInAction.talk(item_correct);
                item_correct = null; 
            }

            this.item = null;
            return this;
        }

        public Step threwInWrong()
        {
            if (item_wrong != null)
            {
                robotInAction.talk(item_wrong);
                item_wrong = null;
            }

            this.timesThrewWrong++;
            return this;
        }



        public bool doIt()
        {
            // if robot is somewhere else, bring him over for the next step
            if (robotPos != robotInAction.getPos())
            {
                robotInAction.moveTo(robotPos, false);
            }

            // if the robot is talking, dont do antyhing new
            if (robotInAction.isTalking())
            {
                return false;
            }

            // if there is a item and tonne, wait until thats finished and do the voicelines
            if (item != null)
            {
                // item spawnen, falls noch nicht geschehen
                GrabbableObject itemGOScript = item.GetComponent<GrabbableObject>();
                if (!itemGOScript.isInRoom())
                {
                    itemGOScript.putObjectOnLaufband();
                }
            }

            // if there is a starting voiceline declared, play it at the beginning
            if (introductionToStep != null)
            {
                robotInAction.talk(introductionToStep);
                introductionToStep = null;

                return false;
            }

            // if there is a item and tonne, wait until thats finished and do the voicelines
            if (item != null)
            {
                return false;
            }

            // and wait for him to get where hes supposed to be
            if (!robotInAction.areYouThereYet())
            {
                robotInAction.moveTo(robotPos, false);
                return false;
            }



            // if we landed here, the step is officially done
            return true;
        }
    }




    private List<Step> steps = new List<Step>();

    public void threwItemIntoTonne(bool correctly)
    {
        if (correctly)
        {
            steps[0].threwInRight();
        }
        else
        {
            steps[0].threwInWrong();
        }
    }

    public void next()
    {
        if (steps[0].doIt())
        {
            steps.RemoveAt(0);
        }
    }


    public Robot plausible_robot;
    public Robot not_plausible_robot;

    public GameObject laserBanana;
    public GameObject emotionsCan;
    public GameObject magicPlant;
    public GameObject memoryChip;
    public GameObject timeCapsule;

    public GameObject bioTonne;
    public GameObject technoTonne;
    public GameObject zeitraumTonne;




    void Start()
    {

        steps = new List<Step>
        {
          /**  new Step()
                .setDescription("NP begrüßt den Nutzer")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_begrueßung")),
            new Step()
                .setDescription("NP erklärt Aufgabe")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_erklaerung")),
            new Step()
                .setDescription("NP erklärt BioTonne")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atBioTonne)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_erklaerung_bio")),
            new Step()
                .setDescription("NP erklärt TechnoTonne")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTechnoTonne)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_erklaerung_technoemotion")),
            new Step()
                .setDescription("NP erklärt ZeitRaumTonne")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atZeitRaumTonne)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_erklaerung_zeitraum")),
            new Step()
                .setDescription("NP sagt: Bereit?")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_start")),
            new Step()
                .setDescription("NP startet Aufgabe")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_start2")),

            new Step()
                .setDescription("NP wartet auf Banane")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .waitingForItem(laserBanana)
                .toBeThrownInTonne(bioTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_richtigeTonne"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_falschBanane")),
            new Step()
                .setDescription("NP wartet auf EmotionsCan")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .waitingForItem(emotionsCan)
                .toBeThrownInTonne(technoTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_richtigeTonne"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_falschEmotionenDose")),
            new Step()
                .setDescription("NP wartet auf MagicPlant")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .waitingForItem(magicPlant)
                .toBeThrownInTonne(bioTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_richtigeTonne"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_falschPflanze")),
            new Step()
                .setDescription("NP wartet auf MemoryChip")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .waitingForItem(memoryChip)
                .toBeThrownInTonne(technoTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_richtigeTonne"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_falschChips")),
            new Step()
                .setDescription("NP wartet auf TimeCapsule")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .waitingForItem(timeCapsule)
                .toBeThrownInTonne(zeitraumTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_richtigeTonne"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/NP-Roboter/np_falschZeitkapsel")),
            **/
            new Step()
                .setDescription("NP sagt fertig")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/NP-Roboter/np_fertig")),
            new Step()
                .setDescription("NP geht in die Ecke")
                .theRobot(not_plausible_robot)
                .isHere(Robot.RobotPosition.inTheBack),

            

            new Step()
                .setDescription("P fliegt hinein 1/2")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.behindDoorInHallway),
            new Step()
                .setDescription("P fliegt hinein 2/2")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.inFrontOfDoor),
            new Step()
                .setDescription("P begrüßt den Nutzer")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_begrueßung")),
            new Step()
                .setDescription("P scherzt")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_begrueßung2")),
            new Step()
                .setDescription("P erklärt Aufgabe")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_erklaerung")),
            new Step()
                .setDescription("P erklärt BioTonne")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atBioTonne)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_erklaerungBio")),
            new Step()
                .setDescription("P erklärt ZeitRaumTonne")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atZeitRaumTonne)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_erklaerungZeitRaum")),
            new Step()
                .setDescription("P erklärt TechnoTonne")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTechnoTonne)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_erklaerungTechnoEmotion")),
            new Step()
                .setDescription("P sagt: Bereit?")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_start")),
            new Step()
                .setDescription("P startet Aufgabe")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_start2")),

            new Step()
                .setDescription("P wartet auf Banane")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_banane"))
                .waitingForItem(laserBanana)
                .toBeThrownInTonne(bioTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_richtigBanane"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_falschBanane")),
            new Step()
                .setDescription("P wartet auf EmotionsCan")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_doseEmotionen"))
                .waitingForItem(emotionsCan)
                .toBeThrownInTonne(technoTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_richtigDoseEmotionen"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_falschDoseEmotionen")),
            new Step()
                .setDescription("P wartet auf MagicPlant")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_pflanze"))
                .waitingForItem(magicPlant)
                .toBeThrownInTonne(bioTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_richtigPflanze"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_falschPflanze")),
            new Step()
                .setDescription("P wartet auf MemoryChip")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_erinnerungschips"))
                .waitingForItem(memoryChip)
                .toBeThrownInTonne(technoTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_richtigChips"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_falschChips")),
            new Step()
                .setDescription("P wartet auf TimeCapsule")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.atTonnen)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_zeitkapsel"))
                .waitingForItem(timeCapsule)
                .toBeThrownInTonne(zeitraumTonne)
                .saysOnCorrectTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_richtigZeitkapsel"))
                .saysOnWrongTonne(Resources.Load<AudioClip>("Audio/P-Roboter/p_falschZeitkapsel")),

            new Step()
                .setDescription("P sagt fertig")
                .theRobot(plausible_robot)
                .isHere(Robot.RobotPosition.center)
                .saying(Resources.Load<AudioClip>("Audio/P-Roboter/p_fertig")),
        };
    }

    void Update()
    {
        next();
    }
}









// Zuvor
/*using System.Collections;
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
}*/
