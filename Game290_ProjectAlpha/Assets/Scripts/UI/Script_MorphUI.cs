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

    //game object positions 
    private Vector3 position0;
    private Vector3 position1;
    private Vector3 position2;

    //Image component reference
    private Image image0;
    private Image image1;
    private Image image2;

    //hold icon sprites
    [SerializeField]
    private Sprite assassinSprite = null;
    [SerializeField]
    private Sprite bruiserSprite = null;
    [SerializeField]
    private Sprite rangedSprite = null;

    //border game object reference
    [SerializeField]
    private GameObject leftBorder = null;
    [SerializeField]
    private GameObject rightBorder = null;

    //border positions
    private Vector3 leftBorderPosition;
    private Vector3 rightBorderPosition;



    void Start()
    {
        //reference image components
        image0 = classIcon0.GetComponent<Image>();
        image1 = classIcon1.GetComponent<Image>();
        image2 = classIcon2.GetComponent<Image>();

        //store positions
        position0 = classIcon0.transform.position;
        position1 = classIcon1.transform.position;
        position2 = classIcon2.transform.position;

        //store border position
        leftBorderPosition = leftBorder.transform.position;
        rightBorderPosition = rightBorder.transform.position;
    }

    public void MorphRight()
    {
        StartCoroutine(MorphRightMove());
    }

    IEnumerator MorphRightMove()
    {
        int count = 0;
        while (count < 25)
        {
            classIcon0.transform.position += new Vector3(2, 0, 0);
            classIcon1.transform.position += new Vector3(2, 0, 0);
            classIcon2.transform.position += new Vector3(2, 0, 0);

            yield return new WaitForSeconds(.01f);
            count += 1;
        }
    }

    public void MorphLeft()
    {
        StartCoroutine(MorphLeftMove());
    }

    IEnumerator MorphLeftMove()
    {
        int count = 0;
        while (count < 25)
        {
            classIcon0.transform.position += new Vector3(-2, 0, 0);
            classIcon1.transform.position += new Vector3(-2, 0, 0);
            classIcon2.transform.position += new Vector3(-2, 0, 0);

            yield return new WaitForSeconds(.01f);
            count += 1;
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
