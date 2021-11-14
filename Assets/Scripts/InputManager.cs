
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{

    public Transform anchor;        // get the transform of the OculusGo Controller device
    public GameObject indicatorObj; // get the object to use to indicate the proposed teleportation spot
    public GameObject player;
    public float PLAYER_EYE_HEIGHT = 1.0f;  // offset from ground assumed in the y direction
    public float MAX_DISTANCE = 8f;         // max distance for teleportion (after testing can be converted to a constant
    public float TARGET_OFFSET = 0.1f;      // distance to display teleport target object 

    public static UnityAction onTriggerDown = null;

    private void Awake()
    {
        InputManager.onTriggerDown += TriggerDown;

    }
    private void OnDestroy()
    {
        InputManager.onTriggerDown -= TriggerDown;

    }
    // Use this for initialization
    void Start()
    {
        indicatorObj.SetActive(false);  // indicator is invisible unless the pointer intersects the ground
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(anchor.position, anchor.forward); // cast a ray from the controller out towards where it is pointing
        RaycastHit hit;                                     // returns the hit variable to indicate what and where the ray 
                                                            // was intersected if at all

        if (Physics.Raycast(ray, out hit, MAX_DISTANCE))
        {
            if (hit.collider.gameObject.tag == "GroundSurf")
            {

                // valid object was hit
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + TARGET_OFFSET, hit.point.z);
                indicatorObj.transform.position = newPosition;
                if (!indicatorObj.activeSelf) indicatorObj.SetActive(true); // make sure it is visible
            }
            else
            {
                // object hit is NOT a ground surface suitable for this kind of teleportation
                if (indicatorObj.activeSelf) indicatorObj.SetActive(false); // if nothihng is hit make indicator invisible
            }

        }
        else
        {
            // nothing was hit
            if (indicatorObj.activeSelf) indicatorObj.SetActive(false); // if nothihng is hit make indicator invisible
        }

        // check for user input: primary trigger 
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger, OVRInput.Controller.Touch))
        {
            if (onTriggerDown != null)
                onTriggerDown();
        }

    }

    // function called when user pulls trigger
    private void TriggerDown()
    {
        // refresh hit to get exact location for teleportation
        Ray ray = new Ray(anchor.position, anchor.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MAX_DISTANCE))
        {
            if (hit.collider.gameObject.tag == "GroundSurf")
            {
                float target_x, target_y, target_z;

                target_x = hit.point.x;
                target_z = hit.point.z;
                target_y = hit.point.y + PLAYER_EYE_HEIGHT;

                //transform the player to the hit position 
                Vector3 newpos = new Vector3(target_x, target_y, target_z);
                player.transform.position = newpos;
            }

        }
    }

}
