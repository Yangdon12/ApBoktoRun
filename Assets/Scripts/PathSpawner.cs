using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> paths = new List<GameObject>();
    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private Playermovement player;
    private int nextIndex;
    private GameObject runningPath;
    private int currentIndex;


    
    private void Update()
    {
        if (runningPath == player.currentPath)
        {
            return;
        }
        PathChanger();
    }

    public void PathChanger()
    {   
        do
        {
            nextIndex = Random.Range(0, paths.Count);
        } while (nextIndex == currentIndex);
        
       
        paths[nextIndex].transform.position = spawnPoint.transform.position;

        runningPath = player.currentPath;
        spawnPoint = paths[nextIndex].transform.GetChild(1).gameObject;
    }
}
