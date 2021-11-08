using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingStation : MonoBehaviour
{
    [Header("UI stuff")]
    public Canvas canvas;
    public TextMeshProUGUI text;
    public GameObject button;

    [Header("Crafting Materials")]
    public Seed seed;
    public Soil soil;
    public List<Element> elements;
    public List<Plant> plants;
    public int waterAmount = 0;

    [Header("recipes / some lookup reference here probably")]
    public CraftingManager craftingManager;

    [Header("Misc")]
    public float itemDropHeight = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onButtonPressed();
    }

    private void OnTriggerEnter(Collider other)
    {
        Seed tempSeed = other.gameObject.GetComponent<Seed>();
        Element tempElement = other.gameObject.GetComponent<Element>();
        Soil tempSoil = other.gameObject.GetComponent<Soil>();


        if (tempElement != null && elements.Count < 2)
        {
            elements.Add(tempElement);
            print("got element");
            tempElement.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + itemDropHeight, this.transform.position.z);
        }
        else if (tempSoil != null && soil == null)
        {
            soil = tempSoil;
            print("got soil");
            soil.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + itemDropHeight, this.transform.position.z);
        }
        else if( tempSeed != null && seed == null)
        {
            seed = tempSeed;
            print("got seed");
            seed.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + itemDropHeight, this.transform.position.z);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        Seed tempSeed = other.gameObject.GetComponent<Seed>();
        Element tempElement = other.gameObject.GetComponent<Element>();
        Soil tempSoil = other.gameObject.GetComponent<Soil>();

        if (elements.Contains(tempElement) )
        {
            elements.Remove(tempElement);
            print("removed element");
        }
        else if (tempSoil == soil )
        {
            soil = null;
            print("removed soil");
        }
        else if( tempSeed == seed)
        {
            seed = null;
            print("removed seed");
        }
    }


    //check if recipe exists for materials. if true, consume and make instance of plant
    //water amount will always be input
    private bool onButtonPressed()
    {
    
        //TODO: actually might be better to move this to craftingManager
        //and have one craft call like, Craft(List<CraftingMaterials> materials .. yeah)

        Element element = null;
        switch (elements.Count)
        {
            case 0:
                break;
            case 1:
                element = elements[0];
                break;
            case 2:
                // //replace with function to combine elements
                // element = elements[1];
                break;
        }

        if (seed != null && soil != null && element != null)
        {
            Plant plant = craftingManager.getPlant(seed, soil, element);

            Destroy(seed.gameObject);
            Destroy(element.gameObject);
            Destroy(soil.gameObject);

            GameObject plantObj = Instantiate(plant.prefab, new Vector3(this.transform.position.x , this.transform.position.y + itemDropHeight, this.transform.position.z), Quaternion.identity);

        }
        else if (element != null)
        {
            //return the new element made
        }
        else if( plants.Count == 2)
        {
            Element newElement = craftingManager.getElement(elements[0], elements[1]);
            GameObject elementObj = Instantiate(newElement.prefab, new Vector3(this.transform.position.x , this.transform.position.y + itemDropHeight, this.transform.position.z), Quaternion.identity);
        }
        else
        {
            return false;
        }

        return true;
    }


    //TODO: make sure there is a crafting manager referenced and other necessary things
    private void OnValidate()
    {

    }

}
