using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {
    public GameObject prefabPayd;

	// Use this for initialization
	void Start () {
        prefabPayd.gameObject.SetActive(true);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartPrefab()
    {
        prefabPayd.gameObject.SetActive(true);
    }
}
