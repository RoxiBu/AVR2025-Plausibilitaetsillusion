using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Vector3 laufbandPos;
    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody rigidbody;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        rigidbody = GetComponent<Rigidbody>();
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Boden")){
            putObjectOnLaufband();
        }
    }

    public void ResetObject()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = startPos;
        transform.rotation = startRot;
        rigidbody.isKinematic = false;
    }
    
    public void putObjectOnLaufband()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = laufbandPos;
        transform.rotation = startRot;
        rigidbody.isKinematic = false;
    }

    public bool isInRoom()
    {
        return Vector3.Distance(transform.position, laufbandPos) < 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
