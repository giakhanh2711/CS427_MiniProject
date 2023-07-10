using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionController : MonoBehaviour
{
    [SerializeField] float offsetDistance = 1.0f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeReference] HighlightController marker;

    CharacterController2D characterController;
    Rigidbody2D rigidbody;
    Character character;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        SetMarker();

        if (Input.GetMouseButtonDown(1))
        {
            CharacterInteract();
        }
    }

    public void SetMarker()
    {
        Vector2 position = rigidbody.position + characterController.lastMotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D collider in colliders)
        {
            InteractableObject obj = collider.GetComponent<InteractableObject>();
            if (obj != null)
            {
                marker.MarkerAppear(obj.gameObject);
                return;
            }
        }

        marker.MarkerHide();
    }

    public void CharacterInteract()
    {
        Vector2 position = rigidbody.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D collider in colliders)
        {
            InteractableObject interactedObject = collider.GetComponent<InteractableObject>();

            if (interactedObject != null)
            {
                interactedObject.BeInteracted(character);
                break;
            }
        }
    }
}
