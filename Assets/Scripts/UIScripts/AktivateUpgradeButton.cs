using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AktivateUpgradeButton : MonoBehaviour
{
    public ParticleSystem Stars;
    public GameObject Sign;
    //Hier die goals variable ein setzen, wenn goals achived ist dann OnGoalAchive() ausfuhren
    //OnMouseDown delete...
    // Use this for initialization

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        Sign.GetComponent<Animator>().SetTrigger("New Trigger");
        Stars.Play();
        Sign.GetComponent<Animator>().SetTrigger("New Trigger");
        
    }

    void OnGoalAchive()
    {
        Sign.GetComponent<Animator>().SetTrigger("New Trigger");
        Stars.Play();
    }
}

