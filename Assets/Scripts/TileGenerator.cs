using UnityEngine;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Worlds[] worlds;
    [SerializeField] private GameObject tilePrefab;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float TileLength = 100;

    [SerializeField] private Transform player;
    private int startTiles = 6;
    private int MaxTiles = 10;
    private int tileCount = 0;
    private int WorldIndex = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int i = 0; i < startTiles; i++)
        {
            if (i < 3)
            {
                //SpawnTile(WorldIndex, 0);
                NextTile();
            }
            else
                SpawnTile(WorldIndex, Random.Range(0, worlds[WorldIndex].tiles.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 100 > spawnPos - (startTiles * TileLength))
        {
            SpawnTile(WorldIndex,Random.Range(0, worlds[WorldIndex].tiles.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int Worldindex, int TileIndex)
    {
        
        GameObject nextTile = Instantiate(worlds[Worldindex].tiles[TileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += TileLength;
        if (tileCount < MaxTiles)
            tileCount++;
        else
        {
            int WI = WorldIndex;

            while (WorldIndex == WI)
                WorldIndex = Random.Range(0, worlds.Length);

            tileCount = 0;
            NextTile();


        }
    }

    private void NextTile()
    {
        GameObject nextTile = Instantiate(tilePrefab, transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += TileLength;
    }
    
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}

[System.Serializable]

public class Worlds {
    public string worldName;
    public GameObject[] tiles;
}
