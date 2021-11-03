using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingArea : MonoBehaviour
{

    public Soil soil;
    public Element element;
    public int waterAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Element tempElement = other.gameObject.GetComponent<Element>();
        Soil tempSoil = other.gameObject.GetComponent<Soil>();

        if (tempElement != null && element == null){
            element = tempElement;
        }else if(tempSoil != null && soil == null) {
            soil = tempSoil;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        Element tempElement = other.gameObject.GetComponent<Element>();
        Soil tempSoil = other.gameObject.GetComponent<Soil>();

        if (tempElement == element)
        {
            element = null;
        }
        else if (tempSoil == soil )
        {
            soil = null;
        }
    }


    private bool onButtonPressed()
    {
        //check if recipe exists for materials
        //if true, consume and make instance of plant
        return true;
    }
    


}
