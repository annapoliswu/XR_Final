using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedType { Basic } //here if we want to add other seeds in future
public enum SoilType { Gravel, Sand, Dirt }
public enum ElementType { Fire, Water, Earth, Lava, Air, Wood, Ice, Lightening, Gem, Metal, Soul, Death, Light, Hellfire, HolyWater, MotherEarth }

public abstract class CraftingMaterial : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
