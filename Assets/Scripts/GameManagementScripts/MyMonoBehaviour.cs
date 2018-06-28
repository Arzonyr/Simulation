using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyMonoBehaviour : MonoBehaviour {

    protected GameManagerScript gameManager;

    public enum ScriptType { DEFAULT, HOUSE, RESOURCE, UPGRADER}
    public enum ResourceType { DEFAULT, STONE, WOOD, GOLD, IRON, FOOD, COAL, POPULATION }

    public ScriptType OwnScriptType;

	public virtual void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        gameManager.AddScript(this);
	}
}
