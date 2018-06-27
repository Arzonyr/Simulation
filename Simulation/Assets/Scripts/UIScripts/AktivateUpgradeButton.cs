using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AktivateUpgradeButton : MonoBehaviour
{
    public ParticleSystem Stars;
    private GameObject sign;
    //Hier die goals variable ein setzen, wenn goals achived ist dann OnGoalAchive() ausfuhren
    //OnMouseDown delete...
    // Use this for initialization

    void Start()
    {
        sign = GameObject.FindGameObjectWithTag("Sign");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        Debug.Log("Ok");
        sign.GetComponent<Animator>().SetTrigger("New Trigger");
        Stars.Play();
        sign.GetComponent<Animator>().SetTrigger("New Trigger");
        
    }

    void OnGoalAchive()
    {
        sign.GetComponent<Animator>().SetTrigger("New Trigger");
        Stars.Play();
    }
}

