using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPrefabDatabase : MonoBehaviour
{
    public ResourceSpread[] resourceLV1;
    public ResourceSpread[] resourceLV2;
    public ResourceSpread[] resourceLV3;
    public ResourceSpread[] resourceLV4;
    public ResourceSpread[] resourceLV5;
    public int[] levelToUpgrade;

    public List<GameObject> resourceIsland = new List<GameObject>();

    public void CreateIslandResources()
    {
        resourceIsland.Clear();
        
        for(int i = 0; i < levelToUpgrade.Length; i++)
        {
            int playerLevel = CoreGame._instance.gameManager.playerLevel;
            if (playerLevel >= levelToUpgrade[i])
            {
                switch(i)
                {
                    case 0:
                        SpawnResourceByLevel(resourceLV1);
                        break;  
                    case 1:
                        SpawnResourceByLevel(resourceLV1);
                        break;
                    case 2:
                        SpawnResourceByLevel(resourceLV2);
                        break;
                    case 3:
                        SpawnResourceByLevel(resourceLV3);
                        break;
                    case 4:
                        SpawnResourceByLevel(resourceLV4);
                        break;
                    case 5:
                        SpawnResourceByLevel(resourceLV5);
                        break;
                }               
            }
        }
    }

    private void SpawnResourceByLevel(ResourceSpread[] resourcesSpreads)
    {
        int playerLevel = CoreGame._instance.gameManager.playerLevel;
        if (resourceLV4.Length > 0 && playerLevel >= 2)
        {
            foreach (ResourceSpread rs in resourcesSpreads)
            {
                for (int i = 0; i < rs.amount; i++)
                {
                    resourceIsland.Add(rs.resource);
                }
            }
        }
    }
}
