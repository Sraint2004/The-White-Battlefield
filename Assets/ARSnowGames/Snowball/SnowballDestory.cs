using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballDestory : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
