using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_MorphUI : MonoBehaviour
{
    //class icon gameobjects
    [SerializeField]
    private GameObject classIcon0 = null;
    [SerializeField]
    private GameObject classIcon1 = null;
    [SerializeField]
    private GameObject classIcon2 = null;

    //Sprite reference
    private Image image0;
    private Image image1;
    private Image image2;

    //hold icon sprites
    [SerializeField]
    private Sprite assassinIcon = null;
    [SerializeField]
    private Sprite bruiserIcon = null;
    [SerializeField]
    private Sprite rangedIcon = null;


    void Start()
    {
        image0 = classIcon0.GetComponent<Image>();
        image1 = classIcon1.GetComponent<Image>();
        image2 = classIcon2.GetComponent<Image>();
    }

    //sets up queue when player spawns in
    public void SetQueue(int orderIndex)
    {
        switch (orderIndex)
        {
            //{ 0, 1, 2 }
            case 0:
                image0.sprite = assassinIcon;
                image1.sprite = bruiserIcon;
                image2.sprite = rangedIcon;
                break;
            //{ 0, 2, 1 }
            case 1:
                image0.sprite = assassinIcon;
                image1.sprite = rangedIcon;
                image2.sprite = bruiserIcon;
                
                break;
            //{ 1, 0, 2 }
            case 2:
                image0.sprite = bruiserIcon;
                image1.sprite = assassinIcon;
                image2.sprite = rangedIcon;
                break;
            //{ 1, 2, 0 }
            case 3:
                image0.sprite = bruiserIcon;
                image1.sprite = rangedIcon;
                image2.sprite = assassinIcon;
                break;
            //{ 2, 1, 0 }
            case 4:
                image0.sprite = rangedIcon;
                image1.sprite = bruiserIcon;
                image2.sprite = assassinIcon;
                break;
            //{ 2, 0, 1 }
            default:
                image0.sprite = rangedIcon;
                image1.sprite = assassinIcon;
                image2.sprite = bruiserIcon;
                break;
        }
    }
}
