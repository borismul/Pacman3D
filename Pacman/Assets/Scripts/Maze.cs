using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour 
{
    // Prefabs
    public MazeCell cellPrefab;

    // Instances
    public MazeCell[,] cells;

    // List of floor cells
    public List<MazeCell> FloorCells;

    // Width and height
    public int width;
    public int height;

    public void Initialize()
    {
        // Initalize new list of floor cells
        FloorCells = new List<MazeCell>();

        // Load the level map
		Texture2D levelMap = (Texture2D)Resources.Load("Levels/kleinlevel");

        // Set width and height
        width = levelMap.width;
        height = levelMap.height;

        // Load wall texture
        Texture textureWall = (Texture)Resources.Load("Textures/Wall");
        Texture textureFloor = (Texture)Resources.Load("Textures/Floor");

        // Build a maze cell library
        cells = new MazeCell[levelMap.width, levelMap.height];

        // Loop through each pixel in the levelmap
        for (int i = 0; i < levelMap.width; i++)
        {
            for (int k = 0; k < levelMap.height; k++)
            {
                // Create cell on the location of the pixel and set the height according to the pixel color
                cells[i, k] = Instantiate(cellPrefab) as MazeCell;
                cells[i, k].transform.parent = transform;
                cells[i, k].name = "Maze Cell (" + i + ", " + k + ")";
                cells[i, k].transform.localPosition = new Vector3(i * cells[i, k].transform.localScale.x, -levelMap.GetPixel(i, k).grayscale * cells[i, k].transform.localScale.y, k * cells[i, k].transform.localScale.z);

                // If the cell is a floor element
                if (levelMap.GetPixel(i, k).grayscale > 0)
                {
                    // Render floor texture
                    cells[i, k].transform.GetChild(0).renderer.material.mainTexture = textureFloor;

                    // Add cell to floor cell list
                    FloorCells.Add(cells[i, k]);

                    // Set as floor cell
                    cells[i, k].isFloor = true;
                }
                else
                {
                    // Render wall texture
                    cells[i, k].transform.GetChild(0).renderer.material.mainTexture = textureWall;
                }

		    }
        }
    }

}
