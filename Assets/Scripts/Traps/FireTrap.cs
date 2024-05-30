using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Health playerHealth;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool isTriggered;
    private bool isActive;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<Health>(out Health _health))
            {
                playerHealth = _health;

                if (isActive)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
            else
            {
                playerHealth = null;
            }

            if(!isTriggered)
            {
                StartCoroutine(ActiveFireTrap());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.TryGetComponent<Health>(out Health _health))
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActiveFireTrap()
    {
        isTriggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        isActive = true;
        spriteRenderer.color = Color.white;
        anim.SetBool("isActive", true);

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        yield return new WaitForSeconds(activeTime);
        anim.SetBool("isActive", false);
        isActive = false;
        isTriggered = false;
    }

    private Health GetPlayerHealth(Health _playerHealth)
    {
        playerHealth = _playerHealth;
        return playerHealth;
    }
}
