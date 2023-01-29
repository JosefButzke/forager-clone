using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    public GameObject actionCursor;
    private GameObject interactingObject;

    public float interactionDistance;
    public float playerLifeMax = 10;
    public float playerLife = 10;
    public float playerEnergyMax = 5;
    public float playerEnergy = 3;
    public float timeToDeleteItem = 1f;
    public int playerLevel = 0;

    public float distanceToSpawnResources;
    public float timeToSpawnResource;

    public GameObject objectToCraft;

    public List<IslandManager> islands = new List<IslandManager>();

    public RecipeIsReady[] recipes;

    public void ActiveCursor(GameObject obj)
    {
        if(gameState != GameState.GAMEPLAY)
        {
            return;
        }

        interactingObject = obj;
        if(Vector2.Distance(CoreGame._instance.playerController.transform.position, interactingObject.transform.position) <= interactionDistance)
        {
            actionCursor.transform.position = obj.transform.position;
            actionCursor.SetActive(true);
        }
    }

    public void DisableCursor()
    {
        actionCursor.SetActive(false);
        interactingObject = null;
    }

    public void ObjectHit()
    {
        if(interactingObject == null)
        {
            return;
        }
        if(actionCursor.activeSelf)
        {
            interactingObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void FixedUpdate()
    {
        if(interactingObject != null)
        {
            if (Vector2.Distance(CoreGame._instance.playerController.transform.position, interactingObject.transform.position) <= interactionDistance)
            {
                actionCursor.SetActive(true);
            } else
            {
                actionCursor.SetActive(false);
            }
        }
    }

    public void Loot(Item item, Vector3 position)
    {
        DisableCursor();

        int dir = -1;

        for(int i = 0; i < item.lootAmount; i++)
        {
            GameObject loot = Instantiate(item.lootPrefab, position, transform.localRotation);

            loot.SendMessage("Active", dir, SendMessageOptions.DontRequireReceiver);

            dir *= -1;
        }
    }

    public bool IsNeedEnergy()
    {
        bool needed = playerEnergy < playerEnergyMax;
        return needed;
    }

    public bool IsNeedLife()
    {
        bool needed = playerLife < playerLifeMax;
        return needed;
    }

    public void SetPlayerLife(int amount)
    {
        playerLife += amount;

        if(playerLife > playerLifeMax)
        {
            playerLife = playerLifeMax;
        }
    }

    public void SetPlayerEnergy(int amount)
    {
        playerEnergy += amount;

        if (playerEnergy > playerEnergyMax)
        {
            playerEnergy = playerEnergyMax;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;

        switch(gameState)
        {
            case GameState.MENU:
                DisableCursor();
                break;
            case GameState.CRAFT:
               
                break;
        }
    }

    public bool CanSpawnNewResource(Vector3 position)
    {
        float distance = Vector3.Distance(CoreGame._instance.playerController.transform.position, position);
        return distance >= distanceToSpawnResources;
    }

    public void StartCraftMode(GameObject obj)
    {
        objectToCraft = obj;
        ChangeGameState(GameState.CRAFT);

        foreach (IslandManager im in islands)
        {
            im.CraftMode();
        }

        CoreGame._instance.menuManager.CloseScreenMenu();
    }

    public void SetCraftObject(IslandSlotGrid slot)
    {
        GameObject obj = Instantiate(objectToCraft);
        obj.transform.position = slot.transform.position;
        slot.isBusy = true;
        slot.ShowMask(false);

        ChangeGameState(GameState.GAMEPLAY);

        foreach (IslandManager im in islands)
        {
            im.GamePlayMode();
        }
    }
}
