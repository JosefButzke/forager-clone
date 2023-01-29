using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WOOD, COAL, IRON, STONE, FRUIT
}

public enum ItemCategory
{
    RESOURCE, CONSUMABLE, TOOL
}

public enum GameState
{
    GAMEPLAY, MENU, CRAFT
}

public enum TabMenuType
{
    SKILLS, INVENTORY, CRAFTING
}

[Serializable]
public struct ResourceSpread
{
    public GameObject resource;
    public int amount;
}

[Serializable]
public struct RecipeRequirementItem
{
    public Item item;
    public int amount;
}

[Serializable]
public struct RecipeIsReady
{
    public RecipeCraft recipe;
    public bool isReady;
}

public class CoreGame : MonoBehaviour
{
    public static CoreGame _instance;
    public PlayerController playerController;
    public GameManager gameManager;
    public Inventory inventory;
    public MenuManager menuManager;

    private void Awake()
    {
        _instance = this;
    }
}
