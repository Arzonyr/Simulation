using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControllScript : MonoBehaviour {

    //Achtung der Code sollte auf main camera oder game manager rein gehen
    /// </summary>
    public Animator panelAnimatior;
    public LayerMask house;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,house))
            {
                panelAnimatior.SetTrigger("New Trigger");
            }
        }
	}

}
