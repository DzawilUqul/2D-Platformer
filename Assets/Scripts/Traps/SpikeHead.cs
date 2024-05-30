using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float maxDelay;
    [SerializeField] private LayerMask playerLayerMask;
    private float delay;
    private float movingTime;
    private Vector3 destination;
    private Vector3[] direction = new Vector3[4];

    private bool isAttacking;

    private void OnEnable() 
    {
        StopMovement();
    }

    private void Update() 
    {
        delay += Time.deltaTime;

        if (isAttacking)
        {
            if (delay > maxDelay)
            {
                transform.transform.Translate(destination * Time.deltaTime * speed);
            }
        }
        else
        {
            CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirection();

        for (int i = 0; i < direction.Length; i++)
        {
            Debug.DrawRay(transform.position, direction[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction[i], range, playerLayerMask);

            if (hit.collider != null && !isAttacking)
            {
                Debug.Log(hit.collider);
                isAttacking = true;
                destination = direction[i];
                delay = 0;
            }
        }
    }

    private void CalculateDirection()
    {
        direction[0] = transform.right * range;
        direction[1] = -transform.right * range;
        direction[2] = transform.up * range;
        direction[3] = -transform.up * range;
    }

    private void StopMovement()
    {
        destination = transform.position;
        isAttacking = false;
    }

    private new void OnTriggerEnter2D(Collider2D collision) 
    {
        base.OnTriggerEnter2D(collision);
        StopMovement();
    }
}
