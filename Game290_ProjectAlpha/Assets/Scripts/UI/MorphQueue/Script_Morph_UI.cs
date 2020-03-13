using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Morph_UI : MonoBehaviour
{
    private GameObject morphQueue;
    private Vector3 morphQueueEulerAngles;
    //class icon gameobjects
    public GameObject classIcon0;
    public GameObject classIcon1;
    public GameObject classIcon2;

    //Image component reference
    private Image image0;
    private Image image1;
    private Image image2;

    //game object positions 
    private Vector3 position0;
    private Vector3 position1;
    private Vector3 position2;

    //hold icon sprites
    public Sprite assassinSprite;
    public Sprite bruiserSprite;
    public Sprite rangedSprite;

    private bool needsToRotateRight = false;
    private bool needsToRotateLeft = false;

    private int angle = 0;

    void Start()
    {
        morphQueue = GameObject.Find("MorphQueue");
        morphQueueEulerAngles = morphQueue.transform.rotation.eulerAngles;

        //Reference Class Icon Game Objects
        classIcon0 = GameObject.Find("Form0");
        classIcon1 = GameObject.Find("Form1");
        classIcon2 = GameObject.Find("Form2");

        //reference image components
        image0 = classIcon0.GetComponent<Image>();
        image1 = classIcon1.GetComponent<Image>();
        image2 = classIcon2.GetComponent<Image>();

        //store positions
        position0 = classIcon0.transform.position;
        position1 = classIcon1.transform.position;
        position2 = classIcon2.transform.position;

        //load sprites from resources folder
        assassinSprite = Resources.Load<Sprite>("art_assassin_character_idle4");
        bruiserSprite = Resources.Load<Sprite>("Brawler Idle animation1");
        rangedSprite = Resources.Load<Sprite>("art_ranged_character_idle2");
    }

    void Update()
    {
        if (needsToRotateLeft)
        {
            //angle to rotate to
            Vector3 endAngleEuler = new Vector3(0, 0, angle);

            if (Vector3.Distance(morphQueue.transform.eulerAngles, endAngleEuler) > 2f)
            {
                //MathF.LerpAngle treats values as degrees and increases instead of decreases the value
                float zLerp = Mathf.LerpAngle(morphQueue.transform.rotation.eulerAngles.z, endAngleEuler.z, Time.deltaTime * 10);
                morphQueue.transform.eulerAngles = new Vector3(0, 0, zLerp);

                //keep class icons facing up
                classIcon0.transform.rotation = Quaternion.Euler(0, 0, 0);
                classIcon1.transform.rotation = Quaternion.Euler(0, 0, 0);
                classIcon2.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                //hard set the rotation of morphQUeue
                morphQueue.transform.eulerAngles = endAngleEuler;
                needsToRotateLeft = false;
            }
        }

        else if (needsToRotateRight)
        {
            //angle to rotate to
            Vector3 endAngleEuler = new Vector3(0, 0, angle);

            if (Vector3.Distance(morphQueue.transform.eulerAngles, endAngleEuler) > 2f)
            {
                morphQueue.transform.eulerAngles = Vector3.Lerp(morphQueue.transform.rotation.eulerAngles, endAngleEuler, Time.deltaTime * 10);

                //keep class icons facing up
                classIcon0.transform.rotation = Quaternion.Euler(0, 0, 0);
                classIcon1.transform.rotation = Quaternion.Euler(0, 0, 0);
                classIcon2.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                morphQueue.transform.eulerAngles = endAngleEuler;
                needsToRotateRight = false;
            }
        }
    }

    public void MorphLeft()
    {
        needsToRotateLeft = true;
        if (angle == 0 || angle == 360)
        {
            angle = 240;
        }

        else if (angle == 240)
        {
            angle = 120;
        }

        else if (angle == 120)
        {
            angle = 0;
        }
    }

    public void MorphRight()
    {
        needsToRotateRight = true;
        if (angle == 0 || angle == 360)
        {
            angle = 120;
        }

        else if (angle == 120)
        {
            angle = 240;
        }

        else if (angle == 240)
        {
            angle = 360;
        }
    }


    //sets up queue when player spawns in
    public void SetQueue(int orderIndex)
    {
        switch (orderIndex)
        {
            //{ 0, 1, 2 }
            case 0:
                image0.sprite = assassinSprite;
                image1.sprite = bruiserSprite;
                image2.sprite = rangedSprite;
                break;
            //{ 0, 2, 1 }
            case 1:
                image0.sprite = assassinSprite;
                image1.sprite = rangedSprite;
                image2.sprite = bruiserSprite;

                break;
            //{ 1, 0, 2 }
            case 2:
                image0.sprite = bruiserSprite;
                image1.sprite = assassinSprite;
                image2.sprite = rangedSprite;
                break;
            //{ 1, 2, 0 }
            case 3:
                image0.sprite = bruiserSprite;
                image1.sprite = rangedSprite;
                image2.sprite = assassinSprite;
                break;
            //{ 2, 1, 0 }
            case 4:
                image0.sprite = rangedSprite;
                image1.sprite = bruiserSprite;
                image2.sprite = assassinSprite;
                break;
            //{ 2, 0, 1 }
            default:
                image0.sprite = rangedSprite;
                image1.sprite = assassinSprite;
                image2.sprite = bruiserSprite;
                break;
        }
    }
}
