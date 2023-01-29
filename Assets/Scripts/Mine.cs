using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public IslandSlotGrid slot;

    public Item item;
    private int hitAmount;

    private void Start()
    {
        hitAmount = item.hitAmount;
    }

    public void SetSlot(IslandSlotGrid s)
    {
        slot = s;
        transform.position = slot.transform.position;
        GetComponent<SpriteRenderer>().sortingOrder = s.line;
    }

    private void OnMouseOver()
    {
        CoreGame._instance.gameManager.ActiveCursor(this.gameObject);
    }

    private void OnMouseExit()
    {
        CoreGame._instance.gameManager.DisableCursor();
    }

    private void OnHit()
    {
        hitAmount--;
        if (hitAmount <= 0)
        {
            CoreGame._instance.gameManager.Loot(item, transform.position);

            slot.Busy(false);

            Destroy(this.gameObject);
        }
    }
}
