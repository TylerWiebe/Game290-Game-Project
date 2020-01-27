using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien_Object : MonoBehaviour
{
    public Sprite alien_sprite;
    Vector3 mouse_position = new Vector3();
    Vector3 alien_sprite_position = new Vector3();
    float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject myObject = GameObject.Find("Alien"); //Need this to get alien object's sprite renderer
        SpriteRenderer renderer = myObject.GetComponent<SpriteRenderer>(); //Get alien object's sprite renderer
        renderer.sprite = alien_sprite;
        
        myObject.transform.position = new Vector3(0f, 0f, 0f); //not really required at this time
        myObject.transform.localScale = new Vector3(1f, 1f, 1f); //not really required at this time
    }

    // Update is called once per frame
    void Update()
    {
        GameObject myObject = GameObject.Find("Alien"); //Need this to get alien object's sprite renderer
        mouse_position = Input.mousePosition;
        Debug.Log(mouse_position);
        alien_sprite_position = Camera.main.WorldToScreenPoint(myObject.transform.position);

        mouse_position.x = mouse_position.x - alien_sprite_position.x;
        mouse_position.y = mouse_position.y - alien_sprite_position.y;
        angle = Mathf.Atan2(mouse_position.y, mouse_position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
