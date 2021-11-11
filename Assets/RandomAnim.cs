using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnim : MonoBehaviour
{
     Animator anim;

    // Start is called before the first frame update
    void Start()
    {

      anim=GetComponent<Animator>();
      anim.speed = Random.Range(0.0f,4.0f);
    }

    
}

