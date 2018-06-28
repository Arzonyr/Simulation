using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MyMonoBehaviour {

    public string Name;
    public ResourceType ProductionResource;
    private Resource resource;
    public int ProductionAmount;
    public float ProductionMultiplier;
    public float UpgradeCostMultiplier;
    public int HouseLevel;
    public ResourceType[] UpgradeResourcesTypes;
    public int[] UpgradeCosts;
    public int PopulationGainOnUpgrade;

    private bool firstUpdate = true;
    private Resource[] UpgradeResources;

    void Update () {
        if (firstUpdate)
        {
            firstUpdate = false;
            UpgradeResources = new Resource[UpgradeResourcesTypes.Length];
            foreach (var resources in gameManager.Resources)
            {
                if (resources.OwnResourceType.Equals(ProductionResource))
                {
                    resource = resources;
                }
                for (int i = 0; i < UpgradeResourcesTypes.Length; i++)
                {
                    if (resources.OwnResourceType.Equals(UpgradeResourcesTypes[i]))
                    {
                        UpgradeResources[i] = resources;
                    }
                }
            }
        }
	}

    public void CollectResource()
    {
        if (!(ProductionResource.Equals(ResourceType.COAL)))
        resource.AddResources(ResourceGenerationFormula());
    }

    public int ResourceGenerationFormula()
    {
        return (int)(ProductionAmount * HouseLevel * ProductionMultiplier);
    }

    public bool UpgradeHouse()
    {
        if (TryUpgrade())
        {
            for (int i = 0; i < UpgradeResources.Length; i++)
            {
                UpgradeResources[i].RemoveResources(UpgradeCosts[i]);
            }
            OnUpgrade();
            return true;
        }
        else
        {
            //passiert wenn Upgrade fehlschlägt
            Debug.Log("Too expensive /@" +  ProductionResource.ToString());
            return false;
        }
    }

    private void OnUpgrade()
    {
        HouseLevel++;
        if(ProductionResource.Equals(ResourceType.COAL))
        {
            resource.AddResources(ResourceGenerationFormula());
        }
        gameManager.GainPopulation(PopulationGainOnUpgrade);
        ChangeUpgradeCosts();
    }

    private bool TryUpgrade()
    {
        if(UpgradeResourcesTypes.Length != UpgradeCosts.Length)
        {
            Debug.LogError("Upgrade resources and costs have to be the same size! /@" + ProductionResource.ToString());
            return false;
        }
        else
        {
            for (int i = 0; i < UpgradeResources.Length; i++)
            {
                if (!UpgradeResources[i].CheckForSufficientResources(UpgradeCosts[i])) return false;
            }
            return true;
        }
    }

    private void ChangeUpgradeCosts()
    {
        for (int i = 0; i < UpgradeCosts.Length; i++)
        {
            UpgradeCosts[i] =(int)(UpgradeCosts[i]* UpgradeCostMultiplier);
        }
    }
}
