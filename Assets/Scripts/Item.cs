using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Scriptable/item", order = 1)]
public class Item : ScriptableObject
{
    public ItemType type;
    public ItemCategory category;
    public string itemName;
    public Sprite sprite;
    public int lootAmount;
    public int hitAmount;
    [TextArea(3,10)]
    public string description;
    [TextArea(3, 10)]
    public string bonus;

    public int lifeAmount;
    public bool canRecoveryLife;

    public int energyAmount;
    public bool canRecoveryEnergy;

    public GameObject lootPrefab;
}
