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
        // Loop through each maze cell
        for (int i = 0; i < maze.width; i++)
        {
            for (int k = 0; k < maze.height; k++)
            {
                // Check if  neighbor cells are all floors
                if (maze.cells[i, k].isFloor &&
                    maze.cells[i + 1, k].isFloor &&
                    maze.cells[i - 1, k].isFloor &&
                    maze.cells[i, k + 1].isFloor &&
                    maze.cells[i, k - 1].isFloor &&
                    maze.cells[i + 1, k + 1].isFloor &&
                    maze.cells[i + 1, k - 1].isFloor &&
                    maze.cells[i - 1, k + 1].isFloor &&
                    maze.cells[i - 1, k - 1].isFloor)
                {
                    // Put a coin on that cell
                    coin = Instantiate(coinPrefab) as Coin;
                    coin.transform.parent = transform;
                    coin.name = "Coin (" + maze.cells[i, k].transform.localPosition.x + ", " + maze.cells[i, k].transform.localPosition.z + ")";
                    coin.transform.GetChild(0).renderer.material.color = Color.yellow;
                    coin.transform.localPosition = new Vector3(maze.cells[i, k].transform.localPosition.x, 0, maze.cells[i, k].transform.localPosition.z);

                    // Add coin to the list
                    spawnedCoins.Add(coin);
                }
            }
        }
    }

    public void animate()
    {
        // Loop through each coin
        foreach (Coin coin in spawnedCoins)
        {
            // Bounce and rotate coin
            coin.transform.position += new Vector3(0, 0.01f * Mathf.Sin(2f * Time.time), 0);
            coin.transform.Rotate(Vector3.up, 2f);
        }
    }
}
