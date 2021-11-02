using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingArea : MonoBehaviour
{

    SoilData soil;

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
        CraftingMaterial material = other.gameObject.GetComponent<CraftingMaterial>();
        if (material != null)
        {
            if(material.data.materialType == MaterialType.Soil)
            {
                SoilData sData = (SoilData) material.data; //idk maybe a better way to organize this
            }
        }

    }
}
