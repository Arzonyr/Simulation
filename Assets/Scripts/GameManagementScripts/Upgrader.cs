using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MyMonoBehaviour
{
    public Text DescriptionText;
    public Text CostsText;
    public House CurrentlyClicked;

    public void SetDescriptionText()
    {
        if (CurrentlyClicked != null)
        {
            if (CurrentlyClicked.ProductionResource.Equals(MyMonoBehaviour.ResourceType.COAL)|| CurrentlyClicked.ProductionResource.Equals(MyMonoBehaviour.ResourceType.FOOD)) DescriptionText.text = "Name: " + CurrentlyClicked.name + " [Level " + CurrentlyClicked.HouseLevel + "]\n\nResource gain: " + CurrentlyClicked.ResourceGain + " " + CurrentlyClicked.ProductionResource + " on upgrade";
            else DescriptionText.text = "Name: " + CurrentlyClicked.name + " [Level " + CurrentlyClicked.HouseLevel + "]\n\nResource gain: " +CurrentlyClicked.ResourceGain+" "+ CurrentlyClicked.ProductionResource +" every "+gameManager.TimeBetweenResourceCollection+" seconds";
        }
        else DescriptionText.text = "";
    }

    public void SetCostsText()
    {
        if (CurrentlyClicked != null)
        {
            string temp = "Upgrade Cost:\n";
            for (int i = 0; i < CurrentlyClicked.UpgradeResourcesTypes.Count; i++)
            {
                if(CurrentlyClicked.UpgradeCosts[i] > 0)
                {
                    temp = temp + "\n" + CurrentlyClicked.UpgradeResourcesTypes[i].ToString() + ": " + CurrentlyClicked.UpgradeCosts[i];
                }
            }
            CostsText.text = temp;
        }
        else CostsText.text = "";
    }

    public void OnHouseSelected()
    {
        SetDescriptionText();
        SetCostsText();
    }

    public void OnHouseDeselected()
    {
        DescriptionText.text = "";
        CostsText.text = "";
    }

    public void OnClickUpgrade()
    {
        CurrentlyClicked.UpgradeHouse();
        UpdateUpgradeText();
        gameManager.CheckForUpgradePossibilities();
    }

    public void UpdateUpgradeText()
    {
        SetDescriptionText();
        SetCostsText();
    }

}
