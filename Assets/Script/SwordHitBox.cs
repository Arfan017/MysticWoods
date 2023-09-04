using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDemage = 1f;
    public float knockbackForce = 15f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(0.095f, -0.051f, 0);
    public Vector3 faceLeft = new Vector3(-0.095f, -0.051f, 0);

    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
            // swordCollider.GetComponent<Collider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2)(collider.gameObject.transform.position -  parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;
            // other.SendMessage("OnHit", swordDemage, knockback);
            damageableObject.OnHit(swordDemage, knockback);
        }
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}