using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainManu;
    public GameObject SkillsTab;
    public GameObject InventoryTab;
    public GameObject CraftingTab;
    public GameObject[] CraftingList;

    public Button LeftButton;
    public Button SkillsTabButton;
    public Button InventoryTabButton;
    public Button CraftingTabButton;
    public Button RightButton;

    private TabMenuType currentMenu;

    public void OpenScreenMenu(int tab)
    {
        MainManu.SetActive(true);
        CoreGame._instance.gameManager.ChangeGameState(GameState.MENU);

        if (!Enum.IsDefined(typeof(TabMenuType), tab))
        {
            return;
        }     
        
        SkillsTab.SetActive(false);
        InventoryTab.SetActive(false);
        CraftingTab.SetActive(false);

        SkillsTabButton.GetComponent<Image>().color = ButtonColorDisable(SkillsTabButton.GetComponent<Image>().color);
        InventoryTabButton.GetComponent<Image>().color = ButtonColorDisable(InventoryTabButton.GetComponent<Image>().color);
        CraftingTabButton.GetComponent<Image>().color = ButtonColorDisable(CraftingTabButton.GetComponent<Image>().color);

        currentMenu = (TabMenuType)tab;

        switch (currentMenu)
        {
            case TabMenuType.SKILLS:
                SkillsTab.SetActive(true);
                SkillsTabButton.GetComponent<Image>().color = ButtonColorActive(SkillsTabButton.GetComponent<Image>().color);
                LeftButton.GetComponent<Image>().color = ButtonColorDisable(LeftButton.GetComponent<Image>().color);
                RightButton.GetComponent<Image>().color = ButtonColorActive(RightButton.GetComponent<Image>().color);
                break;
            case TabMenuType.INVENTORY:
                InventoryTab.SetActive(true);
                CoreGame._instance.inventory.UpdateInventory();
                InventoryTabButton.GetComponent<Image>().color = ButtonColorActive(InventoryTabButton.GetComponent<Image>().color);
                LeftButton.GetComponent<Image>().color = ButtonColorActive(LeftButton.GetComponent<Image>().color);
                RightButton.GetComponent<Image>().color = ButtonColorActive(RightButton.GetComponent<Image>().color);
                break;
            case TabMenuType.CRAFTING:
                CraftingTab.SetActive(true);
                CheckRecipes();
                CraftingTabButton.GetComponent<Image>().color = ButtonColorActive(CraftingTabButton.GetComponent<Image>().color);
                RightButton.GetComponent<Image>().color = ButtonColorDisable(RightButton.GetComponent<Image>().color);
                LeftButton.GetComponent<Image>().color = ButtonColorActive(LeftButton.GetComponent<Image>().color);
                break;
            default:
                InventoryTab.SetActive(true);
                InventoryTabButton.GetComponent<Image>().color = ButtonColorActive(InventoryTabButton.GetComponent<Image>().color);
                break;
        }
    }

    public void CloseScreenMenu()
    {
        if (CoreGame._instance.gameManager.gameState != GameState.CRAFT)
        {
            CoreGame._instance.gameManager.ChangeGameState(GameState.GAMEPLAY);
        }

        MainManu.SetActive(false);
    }

    public void OpenScreenMenuLeft()
    {
        OpenScreenMenu(Convert.ToInt32(currentMenu) - 1);
    }

    public void OpenScreenMenuRight()
    {
        OpenScreenMenu(Convert.ToInt32(currentMenu) + 1);
    }

    private Color ButtonColorActive(Color color)
    {
        Color newColor = new Color(color.r, color.g, color.b, 1f);
        return newColor;
    }

    private Color ButtonColorDisable(Color color)
    {
        Color newColor = new Color(color.r, color.g, color.b, 0.1f);
        return newColor;
    }

    private void CheckRecipes()
    {
        foreach(GameObject craftItem in CraftingList)
        {
            craftItem.GetComponent<CheckRecipeIsReady>().CheckRecipe();
        }
    }
}
