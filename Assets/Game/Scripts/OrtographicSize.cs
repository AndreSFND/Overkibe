using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrtographicSize : MonoBehaviour
{

    public SpriteRenderer background = null;

    // Start is called before the first frame update
    void Start() {

        float screenRatio = (float) Screen.width / (float) Screen.height;
        float targetRatio = background.bounds.size.x /  background.bounds.size.y;

        if( screenRatio >= targetRatio ) {

            Camera.main.orthographicSize = background.bounds.size.y / 2;

        } else {

            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = background.bounds.size.y / 2 * differenceInSize;

        }

    }

}
