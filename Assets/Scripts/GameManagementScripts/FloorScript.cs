using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MyMonoBehaviour {

    private void OnMouseDown()
    {
        gameManager.UpgradePanel.SetActive(false);
    }
}
