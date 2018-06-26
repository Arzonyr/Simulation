using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonStartGame : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Scene01");
    }
}
