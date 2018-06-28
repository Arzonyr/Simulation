using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [HideInInspector]
    public List<House> Houses = new List<House>();
    [HideInInspector]
    public List<Resource> Resources = new List<Resource>();
    [HideInInspector]
    public PopulationManager Population;
    [HideInInspector]
    public Upgrader HouseUpgrader;
    public float TimeBetweenResourceCollection = 5;
    public int[] PopulationMilestones;
    public LayerMask HouseLayer;
    public Animator UpgradePanelAnimator;
    public AudioSource BackgroundMusicSource;
    public AudioClip BackgroundMusic;
    public bool Pause = false;

    private bool upgradePanelEnabled = false;
    private int reachedMilestones = 0;

	void Start ()
    {
        StartCoroutine(CollectResourceRoutine());
        InitializeBackgroundMusic();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Pause = !Pause;

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray,out rayHit,100,HouseLayer))
            {
                if(rayHit.collider.tag != "OverlayButton")
                {
                    HouseUpgrader.CurrentlyClicked = rayHit.collider.GetComponent<House>();
                    HouseUpgrader.OnHouseSelected();
                    UpgradePanelAnimator.SetTrigger("New Trigger");
                    upgradePanelEnabled = true;
                }
            }
            else if(upgradePanelEnabled == true)
            {
                HouseUpgrader.OnHouseDeselected();
                upgradePanelEnabled = false;
            }
        }
    }

    IEnumerator CollectResourceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeBetweenResourceCollection);
            if (!Pause)
            {
                CollectResources();
                UpdateOverlayManually();
            }
        }
    }

    private void CollectResources()
    {
        foreach (var house in Houses)
        {
            house.CollectResource();
        }
    }

    private void UpdateOverlayManually()
    {
        foreach (var resource in Resources)
        {
            resource.UpdateOverlay();
        }
    }

    public void AddScript(MyMonoBehaviour Script)
    {
        switch (Script.OwnScriptType)
        {
            case MyMonoBehaviour.ScriptType.DEFAULT:
                Debug.LogError("unspecified script detected");
                break;
            case MyMonoBehaviour.ScriptType.HOUSE:
                Houses.Add((House)Script);
                break;
            case MyMonoBehaviour.ScriptType.RESOURCE:
                Resources.Add((Resource)Script);
                break;
            case MyMonoBehaviour.ScriptType.UPGRADER:
                HouseUpgrader = (Upgrader)Script;
                break;
            default:
                Debug.LogError("unspecified script detected");
                break;
        }
    }

    private void InitializeBackgroundMusic()
    {
        if (BackgroundMusic != null && BackgroundMusicSource != null)
        {
            BackgroundMusicSource.clip = BackgroundMusic;
            BackgroundMusicSource.Play();
        }
        else Debug.Log("No background music found.");
    }

    public void GainPopulation(int amount)
    {
        Population.AddResources(amount);
        if(reachedMilestones < PopulationMilestones.Length && Population.Amount >= PopulationMilestones[reachedMilestones])
        {
            reachedMilestones++;
            Population.LevelUp();
        }
    }

}
