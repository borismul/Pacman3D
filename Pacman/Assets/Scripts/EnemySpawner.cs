using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour 
{
    // Prefabs
    public Enemy enemyPrefab;

    // Instances
    private Enemy enemy;

    // Spawned path marker list
    public List<Enemy> spawnedEnemies;

    // Maximum number of enemies
    private int maxEnemies = 10;

    // Enemy target
    Transform Target;

	private Playercontroller playercontroller;

    public void setTarget(Transform target)
    {
        this.Target = target;
    }

	public void setPlayerController(Playercontroller playercontroller)
	{
		this.playercontroller = playercontroller;
	}

    public void Spawn(Maze maze)
    {
        System.Random rnd = new System.Random();

        for (int i = 0; i < maxEnemies; i++)
        {
            int r = rnd.Next(maze.FloorCells.Count);

            // Put a coin on that cell
            enemy = Instantiate(enemyPrefab) as Enemy;
            enemy.transform.parent = transform;
            enemy.name = "Enemy: spawned at (" + maze.FloorCells[r].transform.localPosition.x + ", " + maze.FloorCells[r].transform.localPosition.z + ")";
            enemy.transform.localPosition = new Vector3(maze.FloorCells[r].transform.localPosition.x, 4, maze.FloorCells[r].transform.localPosition.z);
            enemy.setTarget(Target);
			enemy.setPlayerController(playercontroller);

            // Add coin to the list
            spawnedEnemies.Add(enemy);
        }
    }
	
}
