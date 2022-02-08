using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stealthCamera : MonoBehaviour
{

    public Transform target;

    public float timeOffset;

    [Space(10)]

    public float leftLimit;
    public float rightLimit;

    [Space(10)]

    public float bottomLimit;
    public float topLimit;

    Vector3 startPos, EndPos;
    void Start()
    {
        
    }

    void Update()
    {
        startPos = transform.position;
        EndPos = target.position;


        transform.position = Vector3.Lerp(startPos, EndPos, timeOffset * Time.deltaTime);



        float halfHeight = this.GetComponent<Camera>().orthographicSize;
        float halfWidth = this.GetComponent<Camera>().aspect * halfHeight;
        
        // ugly solution for now....
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit + halfWidth, rightLimit - halfWidth),
                transform.position.y, Mathf.Clamp(transform.position.z, bottomLimit + halfHeight, topLimit - halfHeight));
        
        


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector3(leftLimit, 0, topLimit), new Vector3(rightLimit, 0, topLimit));
        Gizmos.DrawLine(new Vector3(leftLimit, 0, topLimit), new Vector3(leftLimit, 0, bottomLimit));
        Gizmos.DrawLine(new Vector3(rightLimit, 0, topLimit), new Vector3(rightLimit, 0, bottomLimit));
        Gizmos.DrawLine(new Vector3(leftLimit, 0, bottomLimit), new Vector3(rightLimit, 0, bottomLimit));
    }

    // redundant.
    public static void CalculateLimits(Camera aCam, Bounds aArea, out Rect aLimits, out float aMaxHeight)
    {
        var angle = aCam.fieldOfView * Mathf.Deg2Rad * 0.5f;
        Vector2 tan = Vector2.one * Mathf.Tan(angle);
        tan.x *= aCam.aspect;
        Vector3 dim = aArea.extents;
        Vector3 center = aArea.center - new Vector3(0, aArea.extents.y, 0);
        float maxDist = Mathf.Min(dim.x / tan.x, dim.z / tan.y);
        float dist = aCam.transform.position.y - center.y;
        float f = 1f - dist / maxDist;
        dim *= f;
        aMaxHeight = center.y + maxDist;
        aLimits = new Rect(center.x - dim.x, center.z - dim.z, dim.x * 2, dim.z * 2);
    }
}
