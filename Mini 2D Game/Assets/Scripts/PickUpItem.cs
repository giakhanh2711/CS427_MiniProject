using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform playerTransform; // Player of the game
    Character player;
    //CharacterLevel playerLevel;

    [SerializeField] float speed = 5f; // Speed of the objects move toward the player
    [SerializeField] float pickUpDistance = 1.5f; // Distance that player can pick up

    public Item item;
    public int count = 1;

    private void Awake()
    {
        playerTransform = GameManager.instance.player.transform;
        player = GameManager.instance.player.GetComponent<Character>();
        //playerLevel = GameManager.instance.player.GetComponent<CharacterLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        
        if (distance < 0.1)
        {
            if (GameManager.instance.inventory != null)
            {
                GameManager.instance.inventory.Add(item, count);
                player.ReceiveStar(item.starGainWhenPickup);
                //playerLevel.AddStar(item.starGainWhenPickup);
            }
            Destroy(gameObject);
        }
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }
}
