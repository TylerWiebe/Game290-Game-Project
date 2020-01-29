using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Alien_Object : MonoBehaviour
{
    public Sprite alien_sprite;
    Vector3 mouse_position = new Vector3();
    Vector3 alien_sprite_position = new Vector3();
    float angle = 0f;
    private GameObject myObject;
    private GameObject alienBody;
    private GameObject myCamera;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        myObject = GameObject.Find("AlienHead"); //Need this to get alien object's sprite renderer
        alienBody = GameObject.Find("AlienBody"); //Need this to get alien object's sprite renderer
        myCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {

        


        mouse_position = Input.mousePosition;
        Debug.Log(mouse_position);
        alien_sprite_position = Camera.main.WorldToScreenPoint(myObject.transform.position);
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = Input.GetAxis("Vertical") * speed;
        float nextX = transform.position.x + deltaX;
        float nextY = transform.position.y + deltaY;

        mouse_position.x = mouse_position.x - alien_sprite_position.x;
        mouse_position.y = mouse_position.y - alien_sprite_position.y;

        myObject.transform.position = new Vector3(nextX, nextY, 0);
        alienBody.transform.position = new Vector3(nextX, nextY, 0);
        myCamera.transform.position = new Vector3(nextX, nextY, -10);


        angle = Mathf.Atan2(mouse_position.y, mouse_position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
