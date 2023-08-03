using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    Transform player; // Object that enemy will chase
    [SerializeField] float speed;
    [SerializeField] Vector2 attackSize = Vector2.one;
    [SerializeField] int damageAmount = 1;
    [SerializeField] float timeToAttack = 2f;  // Attack every 2 seconds, hp decreases every 2 seconds

    float attackTimer;

    private void Start()
    {
        player = GameManager.instance.player.transform;
        attackTimer = Random.Range(0, timeToAttack);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
            );

        Attack();
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0f)
            return;

        attackTimer = timeToAttack;

        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);

        for (int i = 0; i < targets.Length; ++i)
        {
            // Đụng con nào mà có Component là Character thì mới xử lý
            Damageable character = targets[i].GetComponent<Damageable>();

            if (character != null)
            {
                character.TakeDamage(damageAmount);
            }
        }
    }
}
