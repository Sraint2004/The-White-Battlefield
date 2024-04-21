using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSnow : MonoBehaviour
{

    float snowAmount = 0.0f;
    [SerializeField] private Material[] materials;

    private void Awake()
    {
        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].SetFloat("_SnowAmount", 0.0f);
        }
    }

    private void Update()
    {
        snowAmount = Mathf.Min(0.71f, 0.2f + (Time.time / 15.0f) % 1.2f);

        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].SetFloat("_SnowAmount", snowAmount);
        }
    }
}




    

