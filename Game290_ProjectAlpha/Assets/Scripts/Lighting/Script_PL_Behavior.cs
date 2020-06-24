using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Script_PL_Behavior : MonoBehaviour
{

    public GameObject PL;

    private Light2D light2D;

    private bool GetBrighter = true;

    public float maxIntensity;
    public float minIntensity;

    // Start is called before the first frame update
    void Start()
    {
        light2D = PL.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetBrighter)
        {
            light2D.intensity += 0.002f;
            if (light2D.intensity >= maxIntensity)
            {
                GetBrighter = false;
            }
        }
        else
        {
            light2D.intensity -= 0.002f;

            if (light2D.intensity <= minIntensity)
            {
                GetBrighter = true;
            }
        }
       
    }
}
