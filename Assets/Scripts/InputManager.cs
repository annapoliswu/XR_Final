
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{

    public Transform leftAnchor;        // get the transform of the OculusGo Controller device
    public Transform rightAnchor;
    public GameObject indicatorObj; // get the object to use to indicate the proposed teleportation spot
    public GameObject player;
    public float PLAYER_EYE_HEIGHT = 1.0f;  // offset from ground assumed in the y direction
    public float MAX_DISTANCE = 8f;         // max distance for teleportion (after testing can be converted to a constant
    public float TARGET_OFFSET = 0.1f;      // distance to display teleport target object 

    public static UnityAction onLeftTriggerDown = null;
    public static UnityAction onRightTriggerDown = null;

    public GameObject button;
    public CraftingStation craftingStation;

    private void Awake()
    {
        InputManager.onLeftTriggerDown += LeftTriggerDown;
        InputManager.onRightTriggerDown += RightTriggerDown;

        button = craftingStation.button;

    }
    private void OnDestroy()
    {
        InputManager.onLeftTriggerDown -= LeftTriggerDown;
        InputManager.onRightTriggerDown -= RightTriggerDown;

    }
    // Use this for initialization
    void Start()
    {
        indicatorObj.SetActive(false);  // indicator is invisible unless the pointer intersects the ground
    }

    // Update is called once per frame
    void Update()
    {

        Ray leftRay = new Ray(leftAnchor.position, leftAnchor.forward); // cast a ray from the controller out towards where it is pointing
        RaycastHit leftHit;                                     // returns the hit variable to indicate what and where the ray 
                                                                // was intersected if at all

        if (Physics.Raycast(leftRay, out leftHit, MAX_DISTANCE))
        {
            if (leftHit.collider.gameObject.tag == "GroundSurf")
            {

                // valid object was hit
                Vector3 newPosition = new Vector3(leftHit.point.x, leftHit.point.y + TARGET_OFFSET, leftHit.point.z);
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
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger, OVRInput.Controller.Touch))
        {
            if (onLeftTriggerDown != null)
                onLeftTriggerDown();
        }


        Ray rightRay = new Ray(rightAnchor.position, rightAnchor.forward); // cast a ray from the controller out towards where it is pointing
        RaycastHit rightHit;                                     

        if (Physics.Raycast(rightRay, out rightHit, MAX_DISTANCE))
        {
            if (rightHit.collider.gameObject == button )
            {
                //set button color to smth
            }
            else
            {
               //unset button color
            }

        }
        else
        {
            //unset button color
        }

        // check for user input: primary trigger 
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger, OVRInput.Controller.Touch))
        {
            if (onRightTriggerDown != null)
                onRightTriggerDown();
        }

    }

    // function called when user pulls trigger
    private void LeftTriggerDown()
    {
        // refresh hit to get exact location for teleportation
        Ray ray = new Ray(leftAnchor.position, leftAnchor.forward);
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
                player.GetComponent<Rigidbody>().transform.position = newpos;
            }

        }
    }

    private void RightTriggerDown()
    {
        // refresh hit to get exact location for teleportation
        Ray ray = new Ray(rightAnchor.position, rightAnchor.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MAX_DISTANCE))
        {
            if (hit.collider.gameObject.tag == "GroundSurf")
            {
                //set button clicked
            }

        }
    }
}
