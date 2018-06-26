using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GN : MonoBehaviour
{

	
    List<Transform> _wayPoints;
    public List<Transform> Waypoints { get { return _wayPoints; } set { _wayPoints = value; } }

    static GN _instance;
    public static GN Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GN)) as GN;
            }
            return _instance;
        }
        set { _instance = value; }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        // Initialise waypoints here
        // _waypoints =
    }
}

