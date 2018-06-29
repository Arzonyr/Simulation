using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int VillagerPerPopulationAmount;
    public GameObject UpgradePanel;
    public AudioSource BackgroundMusicSource;
    public AudioClip BackgroundMusic;
    public AudioClip LevelUpAudioClip;
    public AudioClip OnUpgradeClip;
    public Slider VolumeSlider;
    public int ResourceCheat;
    public bool Pause = false;

    private int reachedMilestones = 0;
    private int NPCCount = 1;

	void Start ()
    {
        StartCoroutine(CollectResourceRoutine());
        InitializeBackgroundMusic();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Pause = !Pause;
        if (Input.GetKeyDown(KeyCode.R)) CollectResources(ResourceCheat);
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
        CheckForUpgradePossibilities();
    }

    private void CollectResources(int times)
    {
        for (int i = 0; i < times; i++)
        {
            CollectResources();
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

    public void CheckForUpgradePossibilities()
    {
        foreach (var house in Houses)
        {
            house.CheckForUpgrade();
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
        if(Population.Amount/VillagerPerPopulationAmount > NPCCount)
        {
            NPCCount++;
            Population.SpawnNPC();
        }
    }
    public void OnVolumeChange()
    {
        BackgroundMusicSource.volume = VolumeSlider.value;
    }

}
