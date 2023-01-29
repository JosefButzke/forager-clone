using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IslandManager : MonoBehaviour
{
    private IslandPrefabDatabase database;
    private IslandSlotGrid[] islandGrid;
    public int initialResourcesQuantity;
    public int maxResourcesQuantity;

    void Start()
    {
        database = GetComponent<IslandPrefabDatabase>();
        database.CreateIslandResources();

        islandGrid = GetComponentsInChildren<IslandSlotGrid>();

        if(initialResourcesQuantity > 0 && database.resourceIsland.Count > 0)
        {
            for(int i = 0; i < initialResourcesQuantity; i++)
            {
                SpawnNewResource();
            }
        }

        StartCoroutine(SpawnResources());

        CoreGame._instance.gameManager.islands.Add(this);
    }

    void SpawnNewResource()
    {
        int indexGrid = Random.Range(0, islandGrid.Length);
        IslandSlotGrid slot = islandGrid[indexGrid];

        if(slot.isBusy == false)
        {
            if(CoreGame._instance.gameManager.CanSpawnNewResource(slot.transform.position) == true)
            {
                int indexResource = Random.Range(0, database.resourceIsland.Count);
                GameObject resource = Instantiate(database.resourceIsland[indexResource]);

                resource.GetComponent<Mine>().SetSlot(slot);
                slot.Busy(true);
            } else
            {
                SpawnNewResource();
            }
          
        } 
        else if(slot.isBusy == true)
        {
            SpawnNewResource();
        }
    }


    IEnumerator SpawnResources()
    {
        while(true)
        {
            yield return new WaitForSeconds(CoreGame._instance.gameManager.timeToSpawnResource);

            if (CoreGame._instance.gameManager.gameState != GameState.CRAFT) {
                int count = islandGrid.Where(x => x.isBusy == true).Count();

                if (count < maxResourcesQuantity)
                {
                    SpawnNewResource();
                }
            }
           
        }

    }

    public void CraftMode()
    {
        foreach(IslandSlotGrid slotGrid in islandGrid)
        {
            if(slotGrid.isBusy == false)
            {
                slotGrid.ShowMask(true);
            }
        }
    }

    public void GamePlayMode()
    {
        foreach (IslandSlotGrid slotGrid in islandGrid)
        {          
            slotGrid.ShowMask(false);
        }
    }
}
