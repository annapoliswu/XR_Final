using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera target;
    public float startingY = 13;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirect = target.transform.position;
        lookDirect.y = startingY; //if you want the y position to not lookAt
        transform.LookAt(lookDirect);
    }
}
