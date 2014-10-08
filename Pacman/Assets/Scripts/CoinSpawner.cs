using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour 
{
	// Prefabs
    public Coin coinPrefab;

    // Instances
    private Coin coin;

    // Maximum spawned coins
    private int maxSpawnedCoins = 1000;

    // Spawned coin list
    public List<Coin> spawnedCoins;

    public void Spawn(Maze maze)
    {
        // Loop through each floor cell
        for (int droppedCoin = 0; droppedCoin < maxSpawnedCoins; droppedCoin++)
        {
            // Select a random floor cell
            MazeCell floorCell = maze.FloorCells[Random.Range(0, maze.FloorCells.Count)];

            // Put a coin on that cell
            coin = Instantiate(coinPrefab) as Coin;
            coin.transform.parent = transform;
            coin.name = "Coin (" + floorCell.transform.localPosition.x + ", " + floorCell.transform.localPosition.z + ")";
            coin.transform.GetChild(0).renderer.material.color = Color.yellow;
            coin.transform.localPosition = new Vector3(floorCell.transform.localPosition.x, 0, floorCell.transform.localPosition.z);

            // Add coin to the list
            spawnedCoins.Add(coin);
        }
    }

    public void animate()
    {
        // Loop through each coin
        foreach (Coin coin in spawnedCoins)
        {
            // Bounce and rotate coin
            coin.transform.position += new Vector3(0, 0.01f * Mathf.Sin(3f * Time.time), 0);
            coin.transform.Rotate(Vector3.up, 2f);
        }
    }
}
