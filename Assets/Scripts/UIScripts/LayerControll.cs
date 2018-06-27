using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControll : MonoBehaviour {
    public GameObject text;
    void start()
    {
        text.gameObject.GetComponent<MeshRenderer>().sortingOrder = 1;
    }
    void Update()
    {

    }
}
