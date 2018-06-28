using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : Resource {

    public int TownLevel;
    public GameObject NPC;
    public GameObject NPCSpawnlocation;
    public GameObject[] TownStages;

    public override void Start()
    {
        base.Start();
        gameManager.Population = this;
        if(gameManager.PopulationMilestones.Length != TownStages.Length-1)
        {
            Debug.LogError("the amounts of town stages has to be exactly one fewer then population milestones! Otherwise the program will crash!");
        }
        TownStages[0].SetActive(true);
    }

    public void LevelUp()
    {
        if(TownLevel < TownStages.Length && TownLevel >= 1)
        {
            TownStages[TownLevel - 1].SetActive(false);
            TownLevel++;
            TownStages[TownLevel - 1].SetActive(true);
            foreach (var house in gameManager.Houses)
            {
                if (house.ProductionResource.Equals(Resource.ResourceType.COAL))
                {
                    Debug.Log("COAL - " + house.ProductionResource);
                }
                else if (house.ProductionResource.Equals(Resource.ResourceType.FOOD)) return;
                else
                {
                    house.UpgradeCosts[house.UpgradeCosts.Count - (4 - TownLevel)] = 4 - TownLevel == 2 ? house.IronUpgradeCost : house.FoodUpgradeCost;
                    Debug.Log("OTHER - " + house.ProductionResource);
                }
            }
        }
    }

    public void SpawnNPC()
    {
        GameObject.Instantiate(NPC, NPCSpawnlocation.transform.position,Quaternion.identity);
    }
}
