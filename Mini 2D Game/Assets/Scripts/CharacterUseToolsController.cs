using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUseToolsController : MonoBehaviour
{
    CharacterController2D characterController;
    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1.0f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    public void UseTool()
    {
        Vector2 position = rigidbody.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D collider in colliders)
        {
            HitByToolObject hit = collider.GetComponent<HitByToolObject>();
            
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
