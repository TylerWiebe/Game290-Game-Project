using UnityEngine;

public class script_CameraFollow : MonoBehaviour
{
    //Holds target position to move towards
    private Vector3 targetPosition;

    //holds camera position
    private Vector3 currentPosition;

    //holds the name of the player object the camera will follow
    [SerializeField]
    private string playerObjectName = "Player";

    //holds camera follow speed
    [SerializeField]
    private int cameraFollowSpeed = 8;

    // Update is called once per frame
    void LateUpdate()
    {
        currentPosition = transform.position;

        targetPosition.x = GameObject.Find(playerObjectName).transform.position.x;
        targetPosition.y = GameObject.Find(playerObjectName).transform.position.y;
        targetPosition.z = -10;


        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, (float) cameraFollowSpeed / 100);
    }
}
