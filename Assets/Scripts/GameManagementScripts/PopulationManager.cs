using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : Resource {

    public int TownLevel;
    public GameObject[] TownStages;

    public override void Start()
    {
        base.Start();
        gameManager.Population = this;
        if(gameManager.PopulationMilestones.Length != TownStages.Length)
        {
            Debug.LogError("the amounts of town stages has to be the same as population milestones! Otherwise the program will crash!");
        }
        TownStages[0].SetActive(true);
    }

    public void LevelUp()
    {
        if(TownLevel < TownStages.Length && TownLevel >= 1)
        TownStages[TownLevel - 1].SetActive(false);
        TownLevel++;
        TownStages[TownLevel - 1].SetActive(true);
    }

}
