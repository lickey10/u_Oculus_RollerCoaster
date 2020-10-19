using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCoasterController : MonoBehaviour
{
    public AudioSource ScreamAudioSource;
    public AudioClip ScreamAudioClip;

    WaypointsFree.WaypointsTraveler waypointsTraveler;
    bool rideStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        waypointsTraveler = GetComponent<WaypointsFree.WaypointsTraveler>();
        rideStarted = waypointsTraveler.AutoStart;

        if(ScreamAudioSource == null)
            ScreamAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Ride_RollerCoaster")//only control the roller coaster while riding it
        {
            float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);
            //float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);
            float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

            Debug.Log("WEAPON FIRE UPDATE - LT = " + leftTrigger + " RT = " + rightTrigger);

            if (leftTrigger != 0)
            {
                if (!waypointsTraveler.IsMoving)
                    waypointsTraveler.Move(true);

                rideStarted = true;

                Debug.Log("LEFT TRIGGER PRESS DETECTED");
            }

            if (rightTrigger != 0)
            {
                if (!waypointsTraveler.IsMoving)
                    waypointsTraveler.Move(true);

                rideStarted = true;

                Debug.Log("RIGHT TRIGGER PRESS DETECTED");
            }

            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.9f)
            {
                if (!waypointsTraveler.IsMoving)
                {
                    waypointsTraveler.ResetTraveler();
                    waypointsTraveler.Move(true);
                }

                rideStarted = true;

                Debug.Log("Oculus_CrossPlatform_SecondaryIndexTrigger TRIGGER PRESS DETECTED");
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (!waypointsTraveler.IsMoving)
                {
                    waypointsTraveler.ResetTraveler();
                    waypointsTraveler.Move(true);
                }
                else
                    ScreamAudioSource.PlayOneShot(ScreamAudioClip);

                rideStarted = true;

                Debug.Log("Fire1 TRIGGER PRESS DETECTED");
            }

            if (rideStarted && !waypointsTraveler.IsMoving)
            {
                rideStarted = false;

                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
            }
        }
    }
}
