using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType {Soil, Element, Water}
public enum SoilType { Gravel, Sand, Dirt }
public enum ElementType { Fire, Water, Earth, Lava, Air, Wood, Ice, Lightening, Gem, Metal, Soul, Death, Light, Hellfire, HolyWater, MotherEarth}

public abstract class CraftingMaterialData : ScriptableObject
{
    public GameObject prefab;
    public string materialDescription = "";
    public MaterialType materialType;

}
