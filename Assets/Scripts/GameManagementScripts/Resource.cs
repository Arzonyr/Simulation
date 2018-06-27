using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MyMonoBehaviour {

    public ResourceType OwnResourceType;
    public int Amount;
    public Text TextField;

    public int StandartUpgradeAmount;

    private string textField;

    public override void Start()
    {
        base.Start();
        textField = TextField.text;
    }

    public void AddResources(int amount)
    {
        Amount += amount;
        UpdateOverlay();
    }

    public void RemoveResources(int amount)
    {
        if (CheckForSufficientResources(amount))
        {
            Amount -= amount;
            UpdateOverlay();
        }
        else Debug.LogError("tried removing too much resources /@"+ OwnResourceType.ToString());
    }

    public void UpdateOverlay()
    {
        TextField.text = textField + " " + Amount;
    }

    public bool CheckForSufficientResources(int cost)
    {
        if (Amount >= cost) return true;
        return false;
    }
}
