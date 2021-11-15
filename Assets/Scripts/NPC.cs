using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject popUp;
    public GameObject textObject;

    private TextMeshPro text;

    public GameObject codex;

    private static string[] helloResponses = new string[] { "Hello, welcome to my greenhouse", "Welcome, happy crafting", "Careful traveller, element breeding is a dangerous pursuit" };
    private static string[] hintResponses = new string[] { "Try and combine elements with different soil types and a seed", "Try and breed two plants or elements together to generate a new element", "Not all combinations work of elements work, think carefully", "Try to create the three top tier plants: Mother Earth, Hell Fire, and Holy Water", "With just fire, water, and earth, you can create air, nature, and lava elements" };
    Dictionary<string, string[]> responses = new Dictionary<string, string[]>(){
        {"Say hello", helloResponses},
        {"Ask for a hint", hintResponses} 
    };

    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        popUp.SetActive(false);
        textObject.SetActive(false);
        codex.SetActive(false);
        text = textObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player) 
    {
    	if (player.gameObject.tag == "Player")
    	{
            popUp.SetActive(true);
            textObject.SetActive(true);
            text.text = "Press Y to greet\nPress X to ask for a hint\nPress B at any time to go back";
    	}
    	
    }

    void OnTriggerExit(Collider player) 
    {
    	if (player.gameObject.tag == "Player")
    	{
            popUp.SetActive(false);
            textObject.SetActive(false);
    	}
    	
    }

    void Update()
    {
        if (popUp.activeSelf) {
            if (OVRInput.GetDown(OVRInput.RawButton.Y)) {
                int rnd = rand.Next(3);
                string newText = responses["Say hello"][rnd];
                text.text = newText;
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.X)) {
                int rnd = rand.Next(5);
                string newText = responses["Ask for a hint"][rnd];
                text.text = newText;
            }
            // else if (OVRInput.GetDown(OVRInput.RawButton.A)) {
            //     bool code = codex.activeSelf;
            //     codex.SetActive(! code);
            //     popUp.SetActive(code);
            //     textObject.SetActive(code);
            // }
            else if (OVRInput.GetDown(OVRInput.RawButton.B)) {
                bool active = popUp.activeSelf;
                popUp.SetActive(true);
                textObject.SetActive(true);
                text.text = "Press Y to greet\nPress X to ask for a hint\nPress B at any time to go back";
            }
        }
    }
}
