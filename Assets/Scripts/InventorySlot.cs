using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image itemImage;
    public Text AmountTxt;
    private bool isDelete;
    public Image DeleteBar;

    private float deltaTime;
    private float percentualDeleteBar;

    public void Update()
    {
        if(isDelete == true)
        {
            deltaTime += Time.deltaTime;
            percentualDeleteBar = deltaTime / CoreGame._instance.gameManager.timeToDeleteItem;
            DeleteBar.fillAmount = percentualDeleteBar;

            if(deltaTime >= CoreGame._instance.gameManager.timeToDeleteItem)
            {
                CoreGame._instance.inventory.DeleteItem(item);
            }
        }
    }

    public void UpdateSlot(Item i, int amount)
    {
        DeleteBar.gameObject.SetActive(false);
        item = i;
        itemImage.sprite = item.sprite;
        AmountTxt.text = amount.ToString();
    }

    public void OnSlotClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if(pointerData.button == PointerEventData.InputButton.Left)
        {
            if(item.category == ItemCategory.CONSUMABLE)
            {
                CoreGame._instance.inventory.UseItem(item);
            }
        }

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            isDelete = true;
            deltaTime = 0;
            DeleteBar.fillAmount = 0.1f;
            DeleteBar.gameObject.SetActive(true);
        }
    }

    public void OnSlotUp(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            isDelete = false;
            DeleteBar.gameObject.SetActive(false);
        }
    }

    public void MouseEnter()
    {
        CoreGame._instance.inventory.ShowItemDescriptionWindow(item);
    }

    public void MouseExit()
    {
        CoreGame._instance.inventory.DisableItemDescriptionWindow();
        isDelete = false;
    }
}
