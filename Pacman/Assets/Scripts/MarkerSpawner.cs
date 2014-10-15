using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerSpawner : MonoBehaviour 
{
    // Prefabs
    public PathMarker pathMarkerPrefab;

    // Instances
    private PathMarker pathMarker;

    // Spawned path marker list
    public List<PathMarker> spawnedPathMarker;

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
                    // Put a path marker on that cell
                    pathMarker = Instantiate(pathMarkerPrefab) as PathMarker;
                    pathMarker.transform.parent = transform;
                    pathMarker.name = "Path marker (" + maze.cells[i, k].transform.localPosition.x + ", " + maze.cells[i, k].transform.localPosition.z + ")";
                    pathMarker.transform.localPosition = new Vector3(maze.cells[i, k].transform.localPosition.x, 0, maze.cells[i, k].transform.localPosition.z);

                    // Add path marker to the list
                    spawnedPathMarker.Add(pathMarker);
                }
            }
        }
    }
}
