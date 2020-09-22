using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelCarDetectorStop : MonoBehaviour
{
    public FerrisSpin FerrisSpinReference;
    public int NumberOfRotations = 1;
    public BoxCollider CarDetectorStart;

    private int rotationCounter = 0;
    private BoxCollider thisBoxCollider;
    private bool hasRideStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        thisBoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayersCar")//stop the car
        {
            rotationCounter++;

            if (rotationCounter >= NumberOfRotations)
            {
                FerrisSpinReference.StopRotating();
                rotationCounter = 0;
                //hasRideStarted = false;
                CarDetectorStart.enabled = true;
                thisBoxCollider.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "PlayersCar")
        //{
        //    hasRideStarted = true;
        //}
    }
}
