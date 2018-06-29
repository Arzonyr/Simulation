using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AktivateUpgradeButton : MonoBehaviour
{
    public ParticleSystem Stars;
    public GameObject Sign;
    private bool isUp = false;

    public void UpgradeSignAnimation(bool up)
    {
        if (isUp == up) return;
        Sign.GetComponent<Animator>().SetTrigger("New Trigger");
        Stars.Play();
        Sign.GetComponent<Animator>().SetTrigger("New Trigger");
        isUp = !isUp;
    }

    void OnGoalAchive()
    {
        Sign.GetComponent<Animator>().SetTrigger("New Trigger");
        Stars.Play();
    }
}

