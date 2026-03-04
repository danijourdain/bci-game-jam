using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GetPath : MonoBehaviour
{
    public static GetPath Instance {get; private set;}

    // list of all possible path starts
    [SerializeField] public float laneOffset;
    [SerializeField] public Transform[] pathStarts;
    [SerializeField] public Transform pathEnd;

    private static Transform[] staticPathStarts;
    private static Transform staticPathEnd;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            staticPathStarts = pathStarts;
            staticPathEnd = pathEnd;
        }
    }

    public static Transform[] GetEnemyPath(int lane=-1)
    {
        
        // default value means randomly generated
        if (lane == -1)
        {
            lane = Random.Range(0, staticPathStarts.Length);
        }

        Transform pass = staticPathStarts[lane];
        

        return new Transform[] { pass, staticPathEnd };
    }
}
