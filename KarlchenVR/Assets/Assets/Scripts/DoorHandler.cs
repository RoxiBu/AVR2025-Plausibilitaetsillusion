using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private bool open = false;
    private float doorTimer = 0f;
    private float doorDuration = 1.0f;
    private bool isOpening = false;
    private bool isClosing = false;


    public GameObject left_door;
    public GameObject right_door;

    private Vector3 left_door_closed = Vector3.zero;
    private Vector3 right_door_closed = Vector3.zero;

    private Vector3 left_door_open = Vector3.zero;
    private Vector3 right_door_open = Vector3.zero;

    public AudioClip door_opening;
    public AudioClip door_closing;

    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Someone is trying to go through the door!");
        if (!open)
        {
            open = true;
            isOpening = true;
            isClosing = false;
            doorTimer = 0f;

            audioSource.clip = door_opening;
            audioSource.Play();

            Debug.Log("Door opening...");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Someone went through the door!");
        if (open)
        {
            open = false;
            isClosing = true;
            isOpening = false;
            doorTimer = 0f;

            audioSource.clip = door_closing;
            audioSource.Play();

            Debug.Log("Door closing...");
        }
    }


    void Start()
    {
        left_door_closed = left_door.transform.position;
        right_door_closed = right_door.transform.position;

        left_door_open = left_door_closed + new Vector3(0f, 0f, -1.0f);
        right_door_open = right_door_closed + new Vector3(0f, 0f, 1.0f);
    }


    void Update()
    {
        if (isOpening)
        {
            doorTimer += Time.deltaTime;
            float t = Mathf.Clamp01(doorTimer / doorDuration);

            left_door.transform.position = Vector3.Lerp(left_door_closed, left_door_open, t);
            right_door.transform.position = Vector3.Lerp(right_door_closed, right_door_open, t);

            if (t >= 1f)
                isOpening = false;
        }

        if (isClosing)
        {
            doorTimer += Time.deltaTime;
            float t = Mathf.Clamp01(doorTimer / doorDuration);

            left_door.transform.position = Vector3.Lerp(left_door_open, left_door_closed, t);
            right_door.transform.position = Vector3.Lerp(right_door_open, right_door_closed, t);

            if (t >= 1f)
                isClosing = false;
        }
    }
}
