using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject popUp;
    public GameObject textObject;

    private TextMeshProUGUI text;

    public GameObject codex;

    private static string[] helloResponses = new string[] { "Hello", "Hi" };
    private static string[] hintResponses = new string[] { "Hint1", "Hint2" };
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
        text = textObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player) 
    {
    	if (player.gameObject.tag == "Player")
    	{
            popUp.SetActive(true);
            textObject.SetActive(true);
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
                int rnd = rand.Next(5);
                string newText = responses["Say hello"][rnd];
                text.text = "hello";
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.X)) {
                int rnd = rand.Next(5);
                string newText = responses["Ask for a hint"][rnd];
                text.text = newText;
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.A)) {
                bool code = codex.activeSelf;
                codex.SetActive(!code);
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.B)) {
                popUp.SetActive(false);
                textObject.SetActive(false);
            }
        }
    }
}
