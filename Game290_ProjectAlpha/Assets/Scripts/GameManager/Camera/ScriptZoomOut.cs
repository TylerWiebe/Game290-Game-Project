using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptZoomOut : MonoBehaviour
{
    bool needsToZoomOut = true;
    float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (needsToZoomOut)
        {
            Camera.main.orthographicSize += speed/10 * Time.deltaTime;

            speed += 0.05f;

            if (Camera.main.orthographicSize >= 7)
            {
                needsToZoomOut = false;
            }
        }
    }
}
