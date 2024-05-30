using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool isLinearAttack;
    [SerializeField] private bool isParabolicAttack;
    private float lifeTime;
    private Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ActiveProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update() 
    {
        float movementSpeed = speed * Time.deltaTime;

        if (isParabolicAttack)
        {
            transform.Translate(movementSpeed, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 10f);
        }

        if (isLinearAttack)
        {
            transform.Translate(movementSpeed, 0, 0);
            rb.gravityScale = 0;
        }

        lifeTime += Time.deltaTime;

        if (lifeTime >  resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision) 
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
