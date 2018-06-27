using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {
    public GameObject prefabPayd;

	void Start () {
        prefabPayd.gameObject.SetActive(true);
	}


    public void StartPrefab()
    {
        prefabPayd.gameObject.SetActive(true);
    }
}
