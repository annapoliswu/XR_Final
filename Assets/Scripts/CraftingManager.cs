using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public List<Plant> plants;

    public Plant getPlant(Seed seed, Soil soil, Element element)
    {
        foreach(Plant plant in plants)
        {
            if(plant.seedType == seed.seedType &&
                plant.soilType == soil.soilType &&
                plant.elementType == element.elementType
                )
            {
                return plant;
            }
        }
        return null;
    }
}
