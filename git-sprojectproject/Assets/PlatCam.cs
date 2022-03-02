using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatCam : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerBody;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        #region LOCAL VARS
        float camSpeedx = 1.5f;
        float camSpeedy = 0.8f;
        float yMargin = 1f; //used to create an offset in Y axis but var name yOffset was taken
        float xOffset = Mathf.Abs(transform.position.x - player.transform.position.x);
        float yOffset = (transform.position.y - player.transform.position.y + yMargin);
        bool gotoX = false;
        bool gotoY = false;
        #endregion

        #region SET YMARGIN
         yMargin = -Mathf.Clamp(playerBody.velocity.y, -8f, 4f);
        #endregion

        #region CHECK OFFSETS
        if (xOffset > 3f)
        {
            gotoX = true;
        }
        else if(xOffset < 0.01f)
        {
            gotoX = false;
        }

        if (yOffset > 1.5f || yOffset < -1.5f)
        {
            gotoY = true;
        }
        else //if (yOffset < 0.01f)
        {
            gotoY = false;
        }
        #endregion

        #region LERP TO POS
        if (gotoX)
        {
            //lerps poisiton based on time.deltatime in X
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, camSpeedx * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if(gotoY)
        {
            //lerps poisiton based on time.deltatime in Y
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y - yMargin, camSpeedy * Time.deltaTime), transform.position.z);
        }
        #endregion
    }
}
