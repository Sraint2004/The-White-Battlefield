using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanMove : MonoBehaviour
{
    public float timer = 0;
    public float speed = 0.1f;

    // 旋转角度
    public float rotationAngle = 1.2f;

    private void Start()
    {
        transform.LookAt(Camera.main.transform);
        Vector3 angle = transform.localEulerAngles;
        angle.x = 0;
        transform.localEulerAngles = angle;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer % 4 <= 1 || timer % 4 > 3)
        {
            transform.Rotate(new Vector3(0, rotationAngle,rotationAngle));
        }
        else if (timer % 4 > 1 && timer % 4 <= 3)
        {
            transform.Rotate(new Vector3(0, -rotationAngle,-rotationAngle));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SnowBall"))
        {
            Destroy(gameObject);
        }
    }
}