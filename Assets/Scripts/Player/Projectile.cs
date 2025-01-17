using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0 ,0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 3f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Door>(out Door door))
        {
            return;
        }
        else
        {
            print(other);
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        lifeTime = 0;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = - localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivated()
    {
        gameObject.SetActive(false);
    }
}
