using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playStingOnLoad : MonoBehaviour
{
    public AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS.Play();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
