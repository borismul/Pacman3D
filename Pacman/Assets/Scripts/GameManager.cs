using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    // Prefabs
    public Maze mazePrefab;
    public CoinSpawner coinSpawnerPrefab;
    public MarkerSpawner pathMarkerSpawnerPrefab;

    // Instances
    private Maze maze;
    private CoinSpawner coinSpawner;
    private MarkerSpawner pathMarkerSpawner;

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

        // Initialize the path marker spawner
        pathMarkerSpawner = Instantiate(pathMarkerSpawnerPrefab) as MarkerSpawner;
        pathMarkerSpawner.name = "Spawned path markers";
        pathMarkerSpawner.Spawn(maze);
    }
}
