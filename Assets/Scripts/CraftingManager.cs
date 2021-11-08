using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public List<Plant> plants;
    public List<Element> elements;
    public Dictionary<HashSet<string>, string> elementDictionary = new Dictionary<HashSet<string>, string>
    {
        { new HashSet<string> { "fire", "water" }, "air"},
        { new HashSet<string> { "earth", "water" }, "wood"},
        { new HashSet<string> { "fire", "earth" }, "lava"},
        { new HashSet<string> { "air", "water" }, "ice"},
        { new HashSet<string> { "fire", "air" }, "lightning"},
        { new HashSet<string> { "lava", "air" }, "gem"},
        { new HashSet<string> { "earth", "gem" }, "metal"},
        { new HashSet<string> { "metal", "lightning" }, "death"},
        { new HashSet<string> { "lightning", "air" }, "light"},
        { new HashSet<string> { "gem", "wood" }, "soul"},
        { new HashSet<string> { "fire", "death" }, "hellfire"},
        { new HashSet<string> { "light", "water" }, "holywater"}
    };

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

    public Element getElement(Element elementOne, Element elementTwo) 
    {
        HashSet<string> elementHash = new HashSet<string> { elementOne.name, elementTwo.name };
        string newElementName;
        if (elementDictionary.TryGetValue(elementHash, out newElementName)){}
        else 
        {
            newElementName = elementOne.name;
        }

        foreach(Element element in elements)
        {
            if(element.name == newElementName)
            {
                return element;
            }
        }

        return elementOne;

        
    }
}