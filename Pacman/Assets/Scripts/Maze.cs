using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour 
{
    public MazeCell cellPrefab;

    public void Initialize()
    {
        // Load the level map

        Texture2D levelMap = (Texture2D)Resources.Load("Levels/Level2");

        // Build a maze cell library
        MazeCell[,] cells = new MazeCell[levelMap.width, levelMap.height];

        // Loop through each pixel in the levelmap
        for (int i = 0; i < levelMap.width; i++)
        {
            for (int j = 0; j < levelMap.height; j++)
            {
                // Create cell on the location of the pixel and set the height according to the pixel color
                cells[i, j] = Instantiate(cellPrefab) as MazeCell;
                cells[i, j].transform.parent = transform;
                cells[i, j].name = "Maze Cell (" + i + ", " + j + ")";
				cells[i, j].transform.localPosition = new Vector3(i*cells[i, j].transform.localScale.x, -levelMap.GetPixel(i, j).grayscale * cells[i, j].transform.localScale.y, j*cells[i, j].transform.localScale.z);
            }
        }
    }

}
