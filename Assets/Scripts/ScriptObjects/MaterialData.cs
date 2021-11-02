using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType {Soil, Element, Water}

public abstract class CraftingMaterialData : ScriptableObject
{
    public GameObject prefab;
    public MaterialType materialType;
    public string materialDescription = "";
}
