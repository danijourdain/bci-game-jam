using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GetPath : MonoBehaviour
{
    // list of all possible path starts
    [SerializeField] public float laneOffset;
    [SerializeField] public Transform[] pathStarts;
    [SerializeField] public Transform pathEnd;
    void Start()
    {
        
    }
    public Transform[] GetEnemyPath(int lane=-1)
    {
        
        // default value means randomly generated
        if (lane == -1)
        {
            lane = Random.Range(0, pathStarts.Length);
        }

        Debug.Log("Selected Lane " + lane);

        Transform pass = pathStarts[lane];
        

        return new Transform[] { pass, pathEnd };
    }
}
