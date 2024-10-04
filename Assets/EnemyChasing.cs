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
    //[SerializeField] GameObject character;
   
    float attackTimer;
    SpawnInsectManager spawnInsectManager;
    bool isInsect;

    private void Start()
    {
        spawnInsectManager = GameManager.instance.gameObject.GetComponent<SpawnInsectManager>();

        if (gameObject.GetComponent<ResourceNode>() == null)
        {
            isInsect = false;
            player = GameManager.instance.player.transform;
        }
        else
        {
            //player = spawnInsectManager.SpawnerPos;
            isInsect = true;
        }

        attackTimer = Random.Range(0, timeToAttack);
    }

    public void UpdatePlayerPos(Vector3 starPosition)
    {
        //this.player = starPosition;
    }


    private void Update()
    {
        //transform.position = Vector3.MoveTowards(
        //    transform.position,
        //    player.position,
        //    speed * Time.deltaTime
        //    );

        //if (isInsect == false)
        //{
        //    Debug.Log("isInsect........ false");
        //    Attack();
        //}
    }

    private void Attack()
    {
        Debug.Log("Attack calledddddddddddd");
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0f)
            return;

        attackTimer = timeToAttack;

        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);

        for (int i = 0; i < targets.Length; ++i)
        {
            Damageable character = targets[i].GetComponent<Damageable>();

            if (character != null)
            {
                Debug.Log("Meet Characterrrrrrrrrrrr");
                character.TakeDamage(damageAmount);
            }
        }
    }
}
