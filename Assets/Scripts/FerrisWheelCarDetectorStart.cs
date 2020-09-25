using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelCarDetectorStart : MonoBehaviour
{
    public FerrisSpin FerrisSpinReference;
    //public int NumberOfRotations = 1;
    public BoxCollider CarDetectorStop;

    private BoxCollider thisBoxCollider;
    //private bool hasRideStarted = false;

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
        if(other.gameObject.tag == "PlayersCar")//enable CarDetectorStop
        {
            CarDetectorStop.enabled = true;
            thisBoxCollider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
