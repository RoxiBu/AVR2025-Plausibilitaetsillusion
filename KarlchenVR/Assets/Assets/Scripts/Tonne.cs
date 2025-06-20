using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Tonne : MonoBehaviour
{
    public enum ObjectType{
        Bio,
        Technoemotionen,
        Zeitraum
    }

public ObjectType type;

    public AudioSource audioSource;
    public AudioClip itemThrownInSound;
    public GameMaster gameMaster;




    void OnTriggerEnter(Collider other)
    {   
        audioSource.Play();
        Debug.Log(other.tag + " ist in Tonne " + type);

        if(CheckIfObjectCorrect(other)){
            Debug.Log(other.tag + " wurde korrekt einsortiert in " + type);
            gameMaster.threwItemIntoTonne(true);
            Destroy(other.gameObject);
        } else{
            gameMaster.threwItemIntoTonne(false);
            Debug.Log(other.tag + " wurde leider falsch einsortiert in " + type);
        }
        
    }
    
    private bool CheckIfObjectCorrect(Collider other){
        if ((other.CompareTag("Laserbanane") || other.CompareTag("Pflanze")) && type == ObjectType.Bio){
            return true;
        } else if((other.CompareTag("Dose") || other.CompareTag("Chips")) && type == ObjectType.Technoemotionen){
            return true;
        } else if(other.CompareTag("Zeitkapsel") && type == ObjectType.Zeitraum){
            return true;
        } else {
            return false;
        }
    }


    void Start()
    {   
        audioSource.clip = itemThrownInSound;
    }

    void Update()
    {

    }
}
