using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandom : MonoBehaviour
{
    Animator anim;
    public string animation;
    public float startSpeed;
    public float endSpeed;

    // Start is called before the first frame update
    void Start()
    {

      anim=GetComponent<Animator>(); 
      anim.speed = Random.Range(startSpeed, endSpeed);
      anim.Play(animation,-1,Random.Range(0.0f,1.0f));
      	
    }
}

    
