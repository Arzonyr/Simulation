using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : Resource {

    public int TownLevel;
    public GameObject NPC;
    public GameObject NPCSpawnlocation;
    public GameObject[] TownStages;
    public GameObject LevelUpParticle;
    public GameObject LevelUpText;
    public GameObject IronMine;
    public GameObject Farm;

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

            StartCoroutine(LevelUpAnimation());

            foreach (var house in gameManager.Houses)
            {
                house.ChangeUpgradeCostsWithoutMultiplier();
            }

            if(TownLevel == 2) IronMine.SetActive(true);
            else if (TownLevel == 3) Farm.SetActive(true);
        }
    }

    public IEnumerator LevelUpAnimation()
    {
        LevelUpParticle.SetActive(true);
        LevelUpText.SetActive(true);
        yield return new WaitForSeconds(seconds: LevelUpParticle.GetComponent<ParticleSystem>().duration);
        LevelUpParticle.SetActive(false);
        LevelUpText.SetActive(false);
    }

    public void SpawnNPC()
    {
        GameObject.Instantiate(NPC, NPCSpawnlocation.transform.position,Quaternion.identity);
    }
}
