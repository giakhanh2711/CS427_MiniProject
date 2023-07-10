using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player; // Player of the game
    [SerializeField] float speed = 5f; // Speed of the objects move toward the player
    [SerializeField] float pickUpDistance = 1.5f; // Distance that player can pick up

    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        
        if (distance < 0.1)
        {
            Destroy(gameObject);
        }
    }
}
