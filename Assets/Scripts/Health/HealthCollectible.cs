using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent<Health>(out Health health)) 
        {
            health.Heal(healthValue);
            gameObject.SetActive(false);
        }
    }
}
