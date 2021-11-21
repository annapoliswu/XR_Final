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

    [Header("Crafting Materials")]
    public GameObject gravelLocation;
    public GameObject sandLocation;
    public GameObject dirtLocation;
    public GameObject seedLocation;

    [Header("recipes / some lookup reference here probably")]
    public CraftingManager craftingManager;

    [Header("Misc")]
    public float itemDropHeight = .5f;
    public ParticleSystem sizzle;

    AudioManager audioManager;

    private void Awake()
    {
        SetHint();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sizzle.Pause();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Seed tempSeed = other.gameObject.GetComponent<Seed>();
        Element tempElement = other.gameObject.GetComponent<Element>();
        Soil tempSoil = other.gameObject.GetComponent<Soil>();
        Plant tempPlant = other.gameObject.GetComponent<Plant>();


        if (tempElement != null && elements.Count < 2)
        {
            elements.Add(tempElement);
            print("got element");
            tempElement.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        else if (tempSoil != null && soil == null)
        {
            soil = tempSoil;
            print("got soil");
            soil.transform.position = new Vector3(this.transform.position.x , soil.transform.position.y, this.transform.position.z);
        }
        else if( tempSeed != null && seed == null)
        {
            seed = tempSeed;
            print("got seed");
            seed.transform.position = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z);
        }
        else if (tempPlant != null && plants.Count < 2)
        {
            plants.Add(tempPlant);
            print("got plant");
        }

        SetHint();
    }


    private void OnTriggerExit(Collider other)
    {
        Seed tempSeed = other.gameObject.GetComponent<Seed>();
        Element tempElement = other.gameObject.GetComponent<Element>();
        Soil tempSoil = other.gameObject.GetComponent<Soil>();
        Plant tempPlant = other.gameObject.GetComponent<Plant>();

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
        else if ( plants.Contains(tempPlant) )
        {
            plants.Remove(tempPlant);
            print("removed plant");
        }

        SetHint();
    }

    //eh, for giving hint / set color of crafting station if there is a valid recipe
    private bool IsValidRecipe()
    {
        if (seed != null && soil != null && (elements.Count == 1 && elements[0] != null))
        {
            Plant plant = craftingManager.getPlant(seed, soil, elements[0]);
            if (plant != null) //plant recipe exists
            {
                return true;
            }
        }
        else if (elements.Count == 2 && plants.Count == 0)
        {
            Element newElement = craftingManager.getElement(elements[0], elements[1]);
            if (newElement != null)
            {
                return true;
            }
        }
        else if (plants.Count == 2)
        {
            Element newElement = craftingManager.getElement(plants[0].elementType, plants[1].elementType);
            if (newElement != null)
            {
                return true;
            }
        }
        
        return false;
    }

    private void SetHint()
    {
        if (IsValidRecipe())
        {
            Color color = Color.green;
            color.a = .25f;
            this.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Color color = new Color(.33f, .7f, 1f);
            color.a = .25f;
            this.GetComponent<Renderer>().material.color = color;
        }
    }

    //check if recipe exists for materials. if true, consume and make instance of plant
    //water amount will always be input
    public bool PressButton() 
    {


        if (seed != null && soil != null && (elements.Count == 1 && elements[0] != null) )
        {
            Plant plant = craftingManager.getPlant(seed, soil, elements[0]);
            if (plant != null) //plant recipe exists
            {
                if (soil.soilType == SoilType.Gravel)
                {
                    GameObject newGravel = Instantiate(soil.prefab, new Vector3(gravelLocation.transform.position.x, gravelLocation.transform.position.y, gravelLocation.transform.position.z), Quaternion.identity);
                }
                else if (soil.soilType == SoilType.Sand)
                {
                    GameObject newSand = Instantiate(soil.prefab, new Vector3(sandLocation.transform.position.x, sandLocation.transform.position.y, sandLocation.transform.position.z), Quaternion.identity);
                }
                else if (soil.soilType == SoilType.Dirt)
                {
                    GameObject newDirt = Instantiate(soil.prefab, new Vector3(dirtLocation.transform.position.x, dirtLocation.transform.position.y, dirtLocation.transform.position.z), Quaternion.identity);
                }
                
                GameObject newSeed = Instantiate(seed.prefab, new Vector3(seedLocation.transform.position.x, seedLocation.transform.position.y, seedLocation.transform.position.z), Quaternion.identity);

                Destroy(seed.gameObject);
                Destroy(soil.gameObject);
                Destroy(elements[0].gameObject);
                seed = null;
                soil = null;
                elements.Clear();

                outputItem(plant.prefab);
            }
        }
        else if (elements.Count == 2 && plants.Count == 0) 
        {
            Element newElement = craftingManager.getElement(elements[0], elements[1]);
            if (newElement != null)
            {
                Destroy(elements[0].gameObject);
                Destroy(elements[1].gameObject);
                elements.Clear();

                outputItem(newElement.prefab);
            }
            else
            {
                print("no such combo, don't do anything");
            }
        }
        else if( plants.Count == 2 ) 
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


    //Outputs item and plays effects with it
    private void outputItem(GameObject prefab)
    {
        audioManager.Play("Magic");
        sizzle.Play();
        GameObject obj = Instantiate(prefab, new Vector3(this.transform.position.x, this.transform.position.y + itemDropHeight, this.transform.position.z), Quaternion.identity);
    }


    //TODO: make sure there is a crafting manager referenced and other necessary things
    private void OnValidate()
    {

    }

}
