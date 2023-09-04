using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public GameObject healthText;
    public bool disableSimulation = false;
    public bool CanTurnInvincible = false;
    public float invincibilityTime = 0.25f;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;
    private float invincibleTimeElapsed = 0f;
    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);


                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
        }
        get
        {
            return _health;
        }
    }
    public bool Targetable
    {
        set
        {
            _tagertable = value;
            if (disableSimulation)
            {
                rb.simulated = false;
            }
            physicsCollider.enabled = value;
        }
        get
        {
            return _tagertable;
        }
    }
    public bool Invincible
    {
        get
        {
            return _invincible;
        }
        set
        {
            _invincible = value;
            if (_invincible == true)
            {
                invincibleTimeElapsed = 0;
            }
            Debug.Log(Invincible);
        }
    }
    float _health = 3;
    bool _tagertable = true;
    bool _invincible = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Health -= damage;
    
            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (CanTurnInvincible)
            {
                Invincible = true;
            }
        }
    }

    public void OnHit(float damage)
    {
        if (Invincible)
        {
            Health -= damage;
        }
    }

    public void OnObjectDestroy()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }
}
