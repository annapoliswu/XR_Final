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
    public ParticleSystem sizzle;

    // Start is called before the first frame update
    void Start()
    {
        sizzle.Pause();
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
            tempElement.transform.position = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z);
        }
        else if (tempSoil != null && soil == null)
        {
            soil = tempSoil;
            print("got soil");
            soil.transform.position = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z);
        }
        else if( tempSeed != null && seed == null)
        {
            seed = tempSeed;
            print("got seed");
            seed.transform.position = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z);
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

        if (seed != null && soil != null && elements[0] != null)
        {
            Plant plant = craftingManager.getPlant(seed, soil, elements[0]);
            if (plant != null) //plant recipe exists
            {
                Destroy(seed.gameObject);
                Destroy(soil.gameObject);
                Destroy(elements[0].gameObject);
                seed = null;
                soil = null;
                elements.Clear();

                outputItem(plant.prefab);
            }
        }
        else if (elements.Count == 2) 
        {
            Element newElement = craftingManager.getElement(elements[0], elements[1]);
            if (newElement != null)
            {
                outputItem(newElement.prefab);

                Destroy(elements[0].gameObject);
                Destroy(elements[1].gameObject);
                elements.Clear();
            }
        }
        else if( plants.Count == 2) 
        {
            Element newElement = craftingManager.getElement(plants[0].elementType, plants[1].elementType);
            if (newElement != null)
            {
                outputItem(newElement.prefab);
            }
        }
        else
        {
            return false;
        }

        return true;
    }


    private void outputItem(GameObject prefab)
    {
        sizzle.Play();
        GameObject obj = Instantiate(prefab, new Vector3(this.transform.position.x, this.transform.position.y + itemDropHeight, this.transform.position.z), Quaternion.identity);
    }


    //TODO: make sure there is a crafting manager referenced and other necessary things
    private void OnValidate()
    {

    }

}
