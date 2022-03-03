using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatCam : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        #region LOCAL VARS
        float camSpeed = 0.8f;
        float yMargin = 1f; //used to create an offset in Y axis but var name yOffset was taken
        float xOffset = Mathf.Abs(transform.position.x - player.transform.position.x);
        float yOffset = Mathf.Abs(transform.position.y - player.transform.position.y + yMargin);
        bool gotoX = false;
        bool gotoY = false;
        #endregion
        /*
        #region SET YMARGIN
        if(player.myBody.velocity.y)
        {
            yMargin = -yMargin;
        }
        #endregion
        */
        #region CHECK OFFSETS
        if (xOffset > 3f)
        {
            gotoX = true;
        }
        else if(xOffset < 0.1f)
        {
            gotoX = false;
        }

        if (yOffset > 2f)
        {
            gotoY = true;
        }
        else if (yOffset < 0.1f)
        {
            gotoY = false;
        }
        #endregion
        #region LERP TO POS
        if (gotoX)
        {
            //lerps poisiton based on time.deltatime in X
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, camSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if(gotoY)
        {
            //lerps poisiton based on time.deltatime in Y
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y - yMargin, camSpeed * Time.deltaTime), transform.position.z);
        }
        #endregion
    }
}
