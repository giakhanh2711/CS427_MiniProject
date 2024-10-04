using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharacterUseToolsController : MonoBehaviour
{
    //CharacterLevel characterLevel;
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
    [SerializeField] GameObject toolImage;
    
    Vector3Int selectedTilePosition;
    bool selectable;
    float timer;
    Sprite defaultToolImage;


    private void Awake()
    {
        character = GetComponent<Character>();
        rigidbody = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<AttackController>();
        defaultToolImage = toolImage.GetComponent<SpriteRenderer>().sprite;
        //characterLevel = GetComponent<CharacterLevel>();
    }

    private void Update()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            //WeaponAction();
        }

        UpdateImageTool();

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

    private void UpdateImageTool()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 newPos = playerPos;
        newPos.y += GameManager.instance.player.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.3f;
        newPos.z = toolImage.transform.position.z;

        toolImage.transform.position = newPos;

        if (toolbarController.GetItemSelected != null)
            toolImage.GetComponent<SpriteRenderer>().sprite = toolbarController.GetItemSelected.icon;
        else
            toolImage.GetComponent<SpriteRenderer>().sprite = defaultToolImage;
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

        CharacterTakeEnergyCost(weaponEnergyCost);

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

        //CharacterTakeEnergyCost(GetEnergyCost(item.onAction));

        animator.SetTrigger("act");
        bool isComplete = item.onAction.OnApply(position);

        if (isComplete)
        {
            CharacterTakeEnergyCost(GetEnergyCost(item.onAction));
            //characterLevel.AddStar(item.onAction.starGain);
            character.ReceiveStar(item.onAction.starGain);

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

            //CharacterTakeEnergyCost(GetEnergyCost(item.onTilemapAction));

            animator.SetTrigger("act");
            bool isComplete = item.onTilemapAction.OnApplyToTilemap(selectedTilePosition, tilemapReadController, item);

            if (isComplete)
            {
                CharacterTakeEnergyCost(GetEnergyCost(item.onTilemapAction));
                //characterLevel.AddStar(item.onTilemapAction.starGain);
                character.ReceiveStar(item.onTilemapAction.starGain);

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
        energyCost -= character.GetLevel(); // Energy cost khi lên level giảm bằng level

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

    private void CharacterTakeEnergyCost(int energyCost)
    {
        character.GetTired(energyCost);

    }
}
