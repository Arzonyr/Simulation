using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour {

    GameObject sign;
    //resources from GameManager
    private float resources;
    public bool test = false;

    // Use this for initialization
    void Start() {
        sign = GameObject.FindGameObjectWithTag("Sign");

		
	}
	
	// Update is called once per frame
	void Update () {
        if (test == true)
        {
            Debug.Log("Ok");
            sign.GetComponent<Animator>().SetTrigger("New Trigger");
            //sign.gameObject.GetComponent<Animator>().Play(1);
        }

    }
    public static void Upgrade()
    {
        Debug.Log("Upgrade the house");
    }
}
