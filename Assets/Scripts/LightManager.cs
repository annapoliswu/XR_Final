using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;

    [SerializeField]
    private LightPreset preset;

    [SerializeField, Range(.01f, 60f)]
    private float minutesPerCycle = 1;

    [SerializeField, Range(0f, 60f)]
    private float minutesPassed;

    [Range(0, 24)]
    public float gameHour = 0;       //hours in game, on a 24 hr period


    // update time based on what percentage of time has passed
    private void UpdateLight(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) - 90, 170, 0));
        }  

    }


    // Update is called once per frame
    void Update()
    {
        if(preset != null)
        {
            //if game is playing, update with game time
            if (Application.isPlaying)
            {
                minutesPassed += Time.deltaTime / 60;    // add fraction of minute passed
                minutesPassed %= minutesPerCycle;        // mod resets minutesPassed to 0 once reaches the defined time for 1 cycle
                UpdateLight(minutesPassed / minutesPerCycle);

                gameHour = (24 * (minutesPassed / minutesPerCycle)) % 24 ;
            }
            else //this is to have it update when changing time of day in inspector
            {
                UpdateLight(minutesPassed / minutesPerCycle);
            }
        }
    }


    //optional, makes sure all elements are set
    private void OnValidate()
    {
        if (directionalLight != null)
        {
            return;
        }
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }

}
