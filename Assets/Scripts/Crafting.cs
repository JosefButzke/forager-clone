using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public GameObject[] craftingList;

    public void Craft(int craftingId)
    {
        CoreGame._instance.gameManager.StartCraftMode(craftingList[craftingId]);
    }
}
