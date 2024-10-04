using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] float offsetDistance = 1.2f;
    [SerializeField] Vector2 attackAreaSize = new Vector2(1f, 1f);

    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Attack(int damageAmount, Vector2 lastMotionVector)
    {
        Vector2 position = rigidbody2D.position + lastMotionVector * offsetDistance;

        Collider2D[] targets = Physics2D.OverlapBoxAll(position, attackAreaSize, 0f);

        foreach (Collider2D collider in targets)
        {
            Damageable damageable = collider.GetComponent<Damageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }
    }
}
