using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;// variable which stores the target in which the camera is facing

    public Vector3 offset; // variable which stores coordinates of the camera 
    public Vector3 offsetRotation;

    public bool useOffsetValues;//a bool variable which enables whether or not Camera Offset Values are used or not

    public float smoothspeed = 5f; // camera speed

    public Transform Camera;
    private Vector3 coords;




    //private float playerinput = GameObject.Find("Player").GetComponent<PlayerControlled>().



    void Start()
    {
        if(!useOffsetValues)
        {
            coords = target.position - transform.position;
        }

    }

    void Update() // changed to late update, smooth illusion -- changed to FixedUpdate, explain in google docs, research how to explain later.
                  // LateUpdate and FixedUpdate not viable, does not work well
    {

        Vector3 desiredPosition = target.position - coords; //Desired Position private variable. Not adding private to the beginning defaults
                                                            // the variable to a private one.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed *Time.deltaTime);

        transform.position = smoothedPosition;
            //target.position - offset;


        transform.LookAt(target);


        if (GameObject.Find("Player").GetComponent<PlayerControlled>().zaxis == true)
        {
           Debug.Log("Camera Rotation");
            coords = offsetRotation;
        }
        if (GameObject.Find("Player").GetComponent<PlayerControlled>().xaxis == true)
        {
            coords = offset;
        }
    }
}
