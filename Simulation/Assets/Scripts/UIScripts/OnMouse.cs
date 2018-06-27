using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour {

    GameObject sign;
    // Use this for initialization
    void Start () {
        sign = GameObject.FindGameObjectWithTag("Sign");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        Debug.Log("Ok");
        sign.GetComponent<Animator>().SetTrigger("New Trigger");
    }
}
