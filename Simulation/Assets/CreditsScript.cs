﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour {
    public void CallCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
