using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public List<Plant> plants;
    public List<Element> elements;
    public Dictionary<HashSet<ElementType>, ElementType> elementDictionary = new Dictionary<HashSet<ElementType>, ElementType>(HashSet<ElementType>.CreateSetComparer());

    public void Awake()
    {
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Fire, ElementType.Water }, ElementType.Air);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Earth, ElementType.Water }, ElementType.Nature);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Fire, ElementType.Earth }, ElementType.Lava);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Air, ElementType.Water },ElementType.Ice);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Fire, ElementType.Air }, ElementType.Lightening);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Lava, ElementType.Air }, ElementType.Gem);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Earth, ElementType.Gem }, ElementType.Metal);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Metal, ElementType.Lightening }, ElementType.Death);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Lightening, ElementType.Air }, ElementType.Light);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Gem, ElementType.Nature }, ElementType.Soul);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Fire, ElementType.Death }, ElementType.Hellfire);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Light, ElementType.Water }, ElementType.HolyWater);
            elementDictionary.Add(new HashSet<ElementType> { ElementType.Earth, ElementType.Soul }, ElementType.MotherEarth);
    }


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


   


    //takes element types and searches dictionary for what type they combine into
    //returns element prefab of that type
    public Element getElement(ElementType elementOne, ElementType elementTwo)
    {
        HashSet<ElementType> elementHash = new HashSet<ElementType> { elementOne, elementTwo };

        ElementType newElementType;
        if (elementDictionary.TryGetValue(elementHash, out newElementType))
        {

        }
        else
        {
            return null;
        }

        foreach (Element element in elements)
        {
            if (element.elementType == newElementType)
            {
                return element;
            }
        }

        return null;

    }


    //same thing but with element inputs
    public Element getElement(Element elementOne, Element elementTwo)
    {
        return getElement(elementOne.elementType, elementTwo.elementType);

    }
}