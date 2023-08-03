using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterUseToolsController : MonoBehaviour
{
    CharacterLevel characterLevel;
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
    [SerializeField] float toolTimeOut = 1f;
    
    Vector3Int selectedTilePosition;
    bool selectable;
    float timer;

    private void Awake()
    {
        character = GetComponent<Character>();
        rigidbody = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<AttackController>();
        characterLevel = GetComponent<CharacterLevel>();
    }

    private void Update()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;

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
        if (timer > 0f)
            return;

        Item item = toolbarController.GetItemSelected;
        if (item == null)
            return;

        if (item.isWeapon == false)
            return;

        EnergyCost(weaponEnergyCost);

        Vector2 position = rigidbody.position + characterController.lastMotionVector * offsetDistance;

        attackController.Attack(item.damageAmount, characterController.lastMotionVector);

        timer = toolTimeOut;
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
        if (timer > 0f)
            return false;

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

        EnergyCost(GetEnergyCost(item.onAction));

        animator.SetTrigger("act");
        bool isComplete = item.onAction.OnApply(position);

        if (isComplete)
        {
            characterLevel.AddExperience(item.onAction.skillType, item.onAction.experienceGain); ;

            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventory);
            }
        }

        timer = toolTimeOut;

        return isComplete;
    }

    private void UseToolGrid()
    {
        if (timer > 0f)
            return;

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

            EnergyCost(GetEnergyCost(item.onTilemapAction));

            animator.SetTrigger("act");
            bool isComplete = item.onTilemapAction.OnApplyToTilemap(selectedTilePosition, tilemapReadController, item);

            if (isComplete)
            {
                characterLevel.AddExperience(item.onTilemapAction.skillType, item.onTilemapAction.experienceGain); ;

                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventory);
                }
            }
        }

        timer = toolTimeOut;
    }

    private int GetEnergyCost(ToolAction action)
    {
        int energyCost = action.energyCost;
        energyCost -= characterLevel.GetLevel(action.skillType); // Càng lên level, energy cost càng giảm

        if (energyCost < 0)
            energyCost = 1;

        return energyCost;
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
