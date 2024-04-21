using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnEnemyDestroyed;
    public GameObject lizi;

    private void OnDestroy()
    {
        Instantiate(lizi, transform.position, Quaternion.identity);
        OnEnemyDestroyed?.Invoke();
    }
}
