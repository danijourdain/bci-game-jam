using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GetPath : MonoBehaviour
{
    // list of all possible path starts
    [SerializeField] private Transform[] pathStarts;
    [SerializeField] private Transform pathEnd;

    public Transform[] GetEnemyPath(int lane=-1)
    {
        // default value means randomly generated
        if (lane == -1)
        {
            lane = Random.Range(0, pathStarts.Length);
        }

        Debug.Log("Selected Lane " + lane);

        return new Transform[] { pathStarts[lane], pathEnd };
    }
}
