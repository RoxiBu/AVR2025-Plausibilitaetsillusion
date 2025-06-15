using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip pickUpSound;
    public AudioClip dropSound;



    public void onPickUp()
    {
        audioSource.clip = pickUpSound;
        audioSource.Play();
    }

    public void onDrop()
    {
        audioSource.clip = dropSound;
        audioSource.Play();
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
