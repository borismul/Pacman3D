using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

    public Maze mazePrefab;
    private Maze mazeInstance;

	// Use this for initialization
	void Start () 
    {
        // Begin the game
        BeginGame();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void BeginGame()
    {
        // Create a maze and initialize it.
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.name = "Maze";
        mazeInstance.Initialize();
    }
}
