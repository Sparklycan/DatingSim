using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtilities : MonoBehaviour
{

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void InstantiatePrefab(GameObject prefab)
    {
        Instantiate(prefab, transform);
    }

}
