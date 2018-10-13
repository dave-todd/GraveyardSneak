using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float rayDuration = 1;

    void Start()
    {
        Destroy(gameObject, rayDuration);
    }

}
