using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoilData", menuName = "Crafting Materials/Soil")]
public class SoilData : CraftingMaterialData
{
    public SoilType soilType;

    public SoilData()
    {
        materialType = MaterialType.Soil;
    }
}

public enum SoilType {Gravel, Sand, Dirt}