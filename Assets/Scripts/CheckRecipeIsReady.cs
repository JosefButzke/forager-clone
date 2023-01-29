using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CheckRecipeIsReady : MonoBehaviour
{
    public Button button;
    public RecipeCraft recipe;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    public void CheckRecipe()
    {
        bool isReady = CoreGame._instance.gameManager.recipes.First(x => x.recipe == recipe).isReady;

        button.interactable = isReady;
    }
}
