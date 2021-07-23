using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    // Start is called before the first frame update
    void Start()
    {
        Bake();
    }

    // Update is called once per frame
    void Update()
    {
        Bake();   
    }

    void Bake()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
