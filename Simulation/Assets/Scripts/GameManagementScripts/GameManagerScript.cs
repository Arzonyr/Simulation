using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public List<House> Houses = new List<House>();
    public List<Resource> Resources = new List<Resource>();
    public float TimeBetweenResourceCollection = 5;
    public AudioSource BackgroundMusicSource;
    public AudioClip BackgroundMusic;
    public bool Pause = false;

	void Start ()
    {
        StartCoroutine(CollectResourceRoutine());
        InitializeBackgroundMusic();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Pause = !Pause;
        if (Input.GetKeyDown(KeyCode.U) && !Pause) TryUpgradeAll();
        
    }

    private void TryUpgradeAll()
    {
        foreach (var house in Houses)
        {
            house.UpgradeHouse();
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
            default:
                break;
        }
    }

    private void InitializeBackgroundMusic()
    {
        BackgroundMusicSource.clip = BackgroundMusic;
        BackgroundMusicSource.Play();
    }

}
