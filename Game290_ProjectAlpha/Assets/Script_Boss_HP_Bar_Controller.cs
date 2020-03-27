using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Boss_HP_Bar_Controller : MonoBehaviour
{

    Script_Boss_Object myScript;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        myScript = GameObject.Find("Boss").GetComponent<Script_Boss_Object>();
        mySlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.value = (int)((myScript.getHitPoints() / myScript.getMaxHitPoints())*100);
    }
}
