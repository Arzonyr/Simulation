using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MyMonoBehaviour {

    public string Name;
    public ResourceType ProductionResource;
    private Resource resource;
    public int ProductionAmount;
    public int ResourceGain
    {
        get
        {
            return ResourceGenerationFormula();
        }
    }
    public float ProductionMultiplier;
    public float UpgradeCostMultiplier;
    public int HouseLevel;
    public List<ResourceType> UpgradeResourcesTypes = new List<ResourceType>();
    public List<int> UpgradeCosts = new List<int>();
    public int IronUpgradeCost;
    public int FoodUpgradeCost;
    public int PopulationGainOnUpgrade;

    private bool firstUpdate = true;
    private List <Resource> UpgradeResources = new List<Resource>();

    void Update () {
        if (firstUpdate)
        {
            firstUpdate = false;
            for (int i = 0; i < UpgradeResourcesTypes.Count; i++)
            {
                foreach (var resources in gameManager.Resources)
                {
                    if (resources.OwnResourceType.Equals(UpgradeResourcesTypes[i]))
                    {
                        UpgradeResources.Add(resources);
                    }
                    if (resources.OwnResourceType.Equals(ProductionResource))
                    {
                        resource = resources;
                    }
                }
            }
            gameManager.UpgradePanel.SetActive(false);
            if (ProductionResource.Equals(ResourceType.IRON) || ProductionResource.Equals(ResourceType.FOOD))
            {
                gameObject.SetActive(false);
            }
        }
	}

    public void CollectResource()
    {
        if (!(ProductionResource.Equals(ResourceType.COAL) || ProductionResource.Equals(ResourceType.FOOD)|| ProductionResource.Equals(ResourceType.IRON)))
        {
            resource.AddResources(ResourceGenerationFormula());
        }
        else if(ProductionResource.Equals(ResourceType.IRON) && gameManager.Population.TownLevel > 1)
        {
            resource.AddResources(ResourceGenerationFormula());
        }
    }

    public int ResourceGenerationFormula()
    {
        return (int)(ProductionAmount * HouseLevel * ProductionMultiplier);
    }

    public bool UpgradeHouse()
    {
        if (TryUpgrade())
        {
            for (int i = 0; i < UpgradeResources.Count; i++)
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
        if (ProductionResource.Equals(ResourceType.COAL) || ProductionResource.Equals(ResourceType.FOOD)) 
        {
            resource.AddResources(ResourceGenerationFormula());
        }
        gameManager.GainPopulation(PopulationGainOnUpgrade);
        HouseLevel++;
        ChangeUpgradeCosts();
        gameManager.HouseUpgrader.UpdateUpgradeText();
    }

    private bool TryUpgrade()
    {
        if(UpgradeResourcesTypes.Count != UpgradeCosts.Count)
        {
            Debug.LogError("Upgrade resources and costs have to be the same size! /@" + ProductionResource.ToString());
            return false;
        }
        else
        {
            for (int i = 0; i < UpgradeResources.Count; i++)
            {
                Debug.Log("Resource: " + UpgradeResources[i] + " cost: " + UpgradeCosts[i]);
                if (!UpgradeResources[i].CheckForSufficientResources(UpgradeCosts[i])) return false;
            }
            return true;
        }
    }

    private void ChangeUpgradeCosts()
    {
        for (int i = 0; i < UpgradeCosts.Count; i++)
        {
            UpgradeCosts[i] =(int)(UpgradeCosts[i]/(HouseLevel-1)*UpgradeCostMultiplier*HouseLevel);
        }
    }

    private void OnMouseDown()
    {
        gameManager.UpgradePanel.SetActive(true);
        gameManager.HouseUpgrader.CurrentlyClicked = this;
        gameManager.HouseUpgrader.OnHouseSelected();
    }

}
