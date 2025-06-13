using System.Collections.Generic;
using UnityEngine;

public class EndlessPathManager: MonoBehaviour
{
    public Transform player;
    public List<GameObject> tilePrefabs; // Straight, Left, Right
    public int tilesOnScreen = 6;

    private List<GameObject> activeTiles = new List<GameObject>();
    private Vector3 spawnPosition = Vector3.zero;
    private Quaternion spawnRotation = Quaternion.identity;

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i == 0)
                SpawnTile(tilePrefabs[0]); // Always start with straight
            else
                SpawnRandomTile();
        }
    }

    void Update()
    {
        float distanceToLastTile = Vector3.Distance(player.position, spawnPosition);
        if (distanceToLastTile < 30f)
        {
            SpawnRandomTile();
            DeleteOldestTile();
        }
    }

    void SpawnRandomTile()
    {
        int rand = Random.Range(0, tilePrefabs.Count);
        SpawnTile(tilePrefabs[rand]);
    }

    void SpawnTile(GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, spawnPosition, spawnRotation);
        activeTiles.Add(tile);

        // Find the NextSpawnPoint in this tile
        Transform next = tile.transform.Find("NextSpawnPoint");
        spawnPosition = next.position;
        spawnRotation = next.rotation;
    }

    void DeleteOldestTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
