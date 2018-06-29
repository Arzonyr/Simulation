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
    public int PopulationGainOnUpgrade;

    private bool firstUpdate = true;
    private int IronResourceCost;
    private int FoodResourceCost;
    private AktivateUpgradeButton signActivator;
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
            signActivator = GetComponent<AktivateUpgradeButton>();
            ChangeUpgradeCostsWithoutMultiplier();
        }
	}

    public void CollectResource()
    {
        if(resource == null)
        {
            for (int i = 0; i < UpgradeResourcesTypes.Count; i++)
            {
                foreach (var resources in gameManager.Resources)
                {
                    if (resources.OwnResourceType.Equals(ProductionResource))
                    {
                        resource = resources;
                    }
                }
            }
        }
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
        if (ProductionResource.Equals(ResourceType.IRON) && gameManager.Population.TownLevel < 2) return 0;
        else if (ProductionResource.Equals(ResourceType.FOOD) && gameManager.Population.TownLevel < 3) return 0;
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
                if (!UpgradeResources[i].CheckForSufficientResources(UpgradeCosts[i])) return false;
            }
            return true;
        }
    }

    private void ChangeUpgradeCosts()
    {
        for (int i = 0; i < UpgradeCosts.Count; i++)
        {
            UpgradeCosts[i] =(int)(UpgradeCosts[i]*UpgradeCostMultiplier*HouseLevel / Mathf.Max(HouseLevel - 1,1));
        }
    }

    public void ChangeUpgradeCostsWithoutMultiplier()
    {
        for (int i = 0; i < UpgradeCosts.Count; i++)
        {
            if (UpgradeResourcesTypes[i].Equals(ResourceType.IRON) && gameManager.Population.TownLevel < 2)
            {
                IronResourceCost = UpgradeCosts[i];
                UpgradeCosts[i] = 0;
            }
            else if (UpgradeResourcesTypes[i].Equals(ResourceType.IRON)&&IronResourceCost!= 0)
            {
                UpgradeCosts[i] = IronResourceCost;
            }
            if (UpgradeResourcesTypes[i].Equals(ResourceType.FOOD) && gameManager.Population.TownLevel < 3)
            {
                FoodResourceCost = UpgradeCosts[i];
                UpgradeCosts[i] = 0;
            }
            else if (UpgradeResourcesTypes[i].Equals(ResourceType.FOOD) && FoodResourceCost != 0)
            {
                UpgradeCosts[i] = FoodResourceCost;
            }
            UpgradeCosts[i] = (int)(UpgradeCosts[i] / Mathf.Max(HouseLevel - 1, 1) * HouseLevel);
        }
    }
    
    public void CheckForUpgrade()
    {
        signActivator.UpgradeSignAnimation(TryUpgrade());
    }

    private void OnMouseDown()
    {
        gameManager.UpgradePanel.SetActive(true);
        gameManager.HouseUpgrader.CurrentlyClicked = this;
        gameManager.HouseUpgrader.OnHouseSelected();
    }

}
