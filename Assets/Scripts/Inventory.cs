using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> inventory = new Dictionary<Item, int> ();

    public GameObject InventoryPanel;
    public RectTransform SlotGrid;
    public GameObject SlotPrefab;

    private List<GameObject> inventorySlots = new List<GameObject>();

    public GameObject ItemDescriptionWindow;
    public Image itemDescriptionImage;
    public Text itemDescriptionName;
    public Text itemDescriptionCategory;
    public Text itemDescriptionBonus;
    public Text itemDescriptionDescription;

    public void GetItem(Item item, int amount)
    {
        if(inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        } else
        {
            inventory.Add(item, amount);
        }
    }

    public void UseItem(Item item)
    {
        if(inventory.ContainsKey(item))
        {
            switch (item.category)
            {
                case ItemCategory.CONSUMABLE:
                    if(CoreGame._instance.gameManager.IsNeedLife() && item.canRecoveryLife)
                    {
                        UpdateItemInventory(item);
                        CoreGame._instance.gameManager.SetPlayerLife(item.lifeAmount);                     
                    }
                    if (CoreGame._instance.gameManager.IsNeedEnergy() && item.canRecoveryEnergy)
                    {
                        UpdateItemInventory(item);
                        CoreGame._instance.gameManager.SetPlayerEnergy(item.energyAmount);                        
                    }
                    break;
            }
        }       
    }

    private void UpdateItemInventory(Item item)
    {
        inventory[item] -= 1;
        if (inventory[item] <= 0)
        {
            DeleteItem(item);
        }
        else
        {
            UpdateInventory();
        }
    }

    public void DeleteItem(Item item)
    {
        inventory.Remove(item);
        UpdateInventory();
        DisableItemDescriptionWindow();
    }

    public void UpdateInventory()
    {
        foreach(GameObject s in inventorySlots)
        {
            Destroy(s);
        }

        inventorySlots.Clear();

        foreach (KeyValuePair<Item, int> item in inventory)
        {
            GameObject i = Instantiate(SlotPrefab, SlotGrid);
            inventorySlots.Add(i);
            i.GetComponent<InventorySlot>().UpdateSlot(item.Key, item.Value);
        }
    }

    public void ShowItemDescriptionWindow(Item item)
    {
        itemDescriptionImage.sprite = item.sprite;
        itemDescriptionName.text = item.itemName;
        itemDescriptionCategory.text = item.category.ToString();
        itemDescriptionBonus.text = item.bonus;
        itemDescriptionDescription.text = item.description;

        itemDescriptionBonus.color = Color.yellow;
        itemDescriptionName.color = Color.white;

        switch (item.category)
        {
            case ItemCategory.RESOURCE:
                itemDescriptionCategory.color = Color.gray;
                break;
            case ItemCategory.CONSUMABLE:
                itemDescriptionCategory.color = Color.green;
                break;
            default:
                itemDescriptionName.color = Color.white;
                itemDescriptionCategory.color = Color.white;
                break;
        }

        ItemDescriptionWindow.SetActive(true);
    }

    public void DisableItemDescriptionWindow()
    {
        ItemDescriptionWindow.SetActive(false);
    }
}
