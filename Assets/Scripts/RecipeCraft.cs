using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Craft", menuName = "Scriptable/recipe_craft", order = 2)]
public class RecipeCraft : ScriptableObject
{
    public RecipeRequirementItem[] requirements;
    public GameObject recipe;
    public int amount;
    public float timeToCreate;
}