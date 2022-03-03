using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatCam : MonoBehaviour
{
	#region VARS
    //set up in editor to access player transform
	public GameObject player;
    public Rigidbody2D playerBody;
	#endregion

	// Start is called before the first frame update
	void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        #region LOCAL VARS / MOST VARS
        //different paning speeds for camera
        float camSpeedx = 1.5f;
        float camSpeedy = 0.8f;

        float yMargin = 1f; //used to create an offset in Y axis but var name yOffset was taken

        //Abs is used to always return a positive float
        float xOffset = Mathf.Abs(transform.position.x - player.transform.position.x);

        //not using abs since it causes cam to not ever go past player.y
        float yOffset = (transform.position.y - player.transform.position.y - yMargin);

        //vars for lerping in an axis
        bool gotoX = false;
        bool gotoY = false;

        //used to lerp Y in a direction
        bool chaseYdown = false;
        bool chaseYup = false;
        #endregion

        #region SET YMARGIN
         yMargin = -Mathf.Clamp(playerBody.velocity.y, -8f, 8f);
        #endregion

        #region CHECK OFFSETS
        //check if x is offset or close enough
        if (xOffset > 3f)
        {
            gotoX = true;
        }
        else if(xOffset < 0.01f)
        {
            gotoX = false;
        }

        //check if y is offset or close enough
            //checks if above player.y
        if (yOffset > 1.5f)
        {
            gotoY = true;
            chaseYdown = true;
        }
		else
		{
            chaseYdown = false;
		}
            //checks if below player.y
        if (yOffset < -1.5f)
		{
            gotoY = true;
            chaseYup = true;
		}
        else 
        {
            chaseYup = false;
        }

        //stop chasing in Y if close enough
        if (chaseYup == false && chaseYdown == false){
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
            if (chaseYdown == true)
            {
                //lerps poisiton downwards based on time.deltatime in Y
                transform.position = new Vector3(transform.position.x, 
                    Mathf.Lerp(transform.position.y, player.transform.position.y - yMargin, camSpeedy * Time.deltaTime), 
                    transform.position.z);
            }
            else if (chaseYup == true)
			{
                //lerps poisiton upwards based on time.deltatime in Y
                transform.position = new Vector3(transform.position.x,
                   Mathf.Lerp(transform.position.y, player.transform.position.y - yMargin, camSpeedy * Time.deltaTime),
                   transform.position.z);
            }
          
        }
        #endregion
    }
}
