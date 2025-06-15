using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Tonne : MonoBehaviour
{
    public enum Type
    {
        bio,
        technoemotionen,
        zeitraum
    }

    public Type type;



    public AudioSource audioSource;
    public AudioClip itemThrownInSound;




    void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        // todo - despawn the item and progress the GameMaster
    }


    void Start()
    {
        audioSource.clip = itemThrownInSound;
    }

    void Update()
    {

    }
}
