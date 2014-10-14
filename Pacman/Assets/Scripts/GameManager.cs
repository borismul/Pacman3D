using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    // Prefabs
    public Maze mazePrefab;
    public CoinSpawner coinSpawnerPrefab;

    // Instances
    private Maze maze;
    private CoinSpawner coinSpawner;

	// Use this for initialization
	void Start () 
    {
        // Begin the game
        BeginGame();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        coinSpawner.animate();
	}

    private void BeginGame()
    {
        // Create a maze and initialize it.
        maze = Instantiate(mazePrefab) as Maze;
        maze.name = "Maze";
        maze.Initialize();

        // Initialize the coin spawner
        coinSpawner = Instantiate(coinSpawnerPrefab) as CoinSpawner;
        coinSpawner.name = "Spawned coins";
        coinSpawner.Spawn(maze);
    }
}
