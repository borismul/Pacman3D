using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    // Prefabs
    public Maze mazePrefab;
    public CoinSpawner coinSpawnerPrefab;
    public EnemySpawner enemySpawnerPrefab;
	public Playercontroller playercontroller;

    // Instances
	public Maze maze;
	public CoinSpawner coinSpawner;
	public EnemySpawner enemySpawner;

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

        // Initialize the enemy spawner
        enemySpawner = Instantiate(enemySpawnerPrefab) as EnemySpawner;
        enemySpawner.name = "Spawned enemies";
        enemySpawner.setTarget(GameObject.FindGameObjectWithTag("Player").transform);
		enemySpawner.setPlayerController (playercontroller);
        enemySpawner.Spawn(maze);

		playercontroller.setCoinList (coinSpawner.spawnedCoins.Count);

    }
}
