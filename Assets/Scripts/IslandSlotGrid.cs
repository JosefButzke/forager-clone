using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
    public int line;
    public bool isBusy;
    public Collider2D col;
    public SpriteRenderer sprite_render_mask;

    public void Busy(bool value)
    {
        isBusy = value;

        col.enabled = !isBusy;
    }

    public void ShowMask(bool value)
    {
        sprite_render_mask.enabled = value;
    }

    private void OnMouseDown()
    {
        if(CoreGame._instance.gameManager.gameState == GameState.CRAFT && isBusy == false)
        {
            CoreGame._instance.gameManager.SetCraftObject(this);
        }
    }

}
