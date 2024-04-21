using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public GameObject snowballPrefab;
    public float initialVelocity;
    public float upwardForce; // 添加向上的力

    public Transform spawnTf;
    private GameObject currentSnowball;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 表示鼠标左键
        {
            if (currentSnowball == null)
            {
                CreateSnowball();
            }
            else
            {
                LaunchSnowball();
            }
        }
    }

    private void CreateSnowball()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 position = Camera.main.ScreenToWorldPoint(screenCenter);
        float offsetY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height * 0.05f, 0)).y;
        position.y = offsetY;
        float suiji = Random.Range(0.7f, 1.2f);
        snowballPrefab.transform.localScale = new Vector3(suiji, suiji, suiji);
        snowballPrefab.transform.localPosition = Vector3.zero;
        // currentSnowball = Instantiate(snowballPrefab, position + Camera.main.transform.forward * 0.4f, Quaternion.identity);
        currentSnowball = Instantiate(snowballPrefab, spawnTf, false);
        Rigidbody rb = currentSnowball.AddComponent<Rigidbody>();
        SphereCollider collider = currentSnowball.AddComponent<SphereCollider>();
        if (rb != null)
        {
            rb.mass = 5f;
            rb.drag = 0.5f;
            rb.angularDrag = 0.05f;
            rb.useGravity = false;
        }
        else
        {
            Debug.LogError("Failed to add Rigidbody component to snowball!");
        }

        if (collider != null)
        {
            collider.isTrigger = true; // 设置collider为触发器
        }
        else
        {
            Debug.LogError("Failed to add SphereCollider component to snowball!");
        }
        // Destroy(currentSnowball, 5);
    }


    private void LaunchSnowball()
    {
        if (currentSnowball != null)
        {
            Vector3 direction = Camera.main.transform.forward;
            Rigidbody rb = currentSnowball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                currentSnowball.transform.SetParent(null);
                // 将速度向量分解为前方向量和向上方向量，并分别施加速度
                Vector3 forwardVelocity = direction * initialVelocity;
                Vector3 upwardVelocity = Vector3.up * upwardForce;
                rb.velocity = forwardVelocity + upwardVelocity;
                rb.useGravity = true;
            }
            else
            {
                Debug.LogError("Rigidbody component not found on snowball!");
            }

            currentSnowball = null;
        }
    }
}