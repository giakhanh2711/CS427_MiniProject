using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterUseToolsController : MonoBehaviour
{
    CharacterController2D characterController;
    Character character;
    Rigidbody2D rigidbody;
    ToolbarController toolbarController;
    Animator animator;
    AttackController attackController;

    [SerializeField] float offsetDistance = 1.0f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapReadController tilemapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] ToolAction onTilePickUp;
    [SerializeField] IconHighlight iconHighlight;
    [SerializeField] int weaponEnergyCost = 5;
    
    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<Character>();
        rigidbody = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<AttackController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeaponAction();
        }

        SelectTile();

        CanSelectCheck();

        Marker();

        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }

            UseToolGrid();
        }
    }

    private void WeaponAction()
    {
        Item item = toolbarController.GetItemSelected;
        if (item == null)
            return;

        if (item.isWeapon == false)
            return;

        EnergyCost(weaponEnergyCost);

        Vector2 position = rigidbody.position + characterController.lastMotionVector * offsetDistance;

        attackController.Attack(item.damageAmount, characterController.lastMotionVector);
    }

    private void SelectTile()
    {
        selectedTilePosition = tilemapReadController.GetGridPostition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2 .Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
        iconHighlight.CanSelect = selectable;
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
        iconHighlight.cellPosition = selectedTilePosition;

    }

    private bool UseToolWorld()
    {
        Vector2 position = rigidbody.position + characterController.lastMotionVector * offsetDistance;

        Item item = toolbarController.GetItemSelected;
        if (item == null)
        {
            return false;
        }

        if (item.onAction == null)
        {
            return false;
        }

        EnergyCost(item.onAction.energyCost);

        animator.SetTrigger("act");
        bool isComplete = item.onAction.OnApply(position);

        if (isComplete)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventory);
            }
        }

        return isComplete;
    }

    private void UseToolGrid()
    {
        Debug.Log("UseToolGrid called");
        if (selectable == true)
        {
            Item item = toolbarController.GetItemSelected;
            if (item == null)
            {
                PickUpTile();
                Debug.Log("item = null");
                return;
            }

            if (item.onTilemapAction == null)
            {
                Debug.Log("item.onTilemapAction = null");
                return;
            }

            EnergyCost(item.onTilemapAction.energyCost);

            animator.SetTrigger("act");
            bool isComplete = item.onTilemapAction.OnApplyToTilemap(selectedTilePosition, tilemapReadController, item);
            
            if (isComplete)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventory);
                }
            }
        }
    }

    private void PickUpTile()
    {
        if (onTilePickUp == null)
        {
            return;
        }

        onTilePickUp.OnApplyToTilemap(selectedTilePosition, tilemapReadController, null);
    }

    private void EnergyCost(int energyCost)
    {
        character.GetTired(energyCost);

    }
}
