using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour 
{
    // Prefabs
    public MazeCell cellPrefab;

    // Cells
    public MazeCell[,] cells;

    public void Initialize()
    {
        // Load the level map
        Texture2D levelMap = (Texture2D)Resources.Load("Levels/Level2");

        // Load wall texture
        Texture textureWall = (Texture)Resources.Load("Textures/Wall");
        Texture textureFloor = (Texture)Resources.Load("Textures/Floor");

        // Build a maze cell library
        cells = new MazeCell[levelMap.width, levelMap.height];

        // Loop through each pixel in the levelmap
        for (int i = 0; i < levelMap.width; i++)
        {
            for (int j = 0; j < levelMap.height; j++)
            {
                // Create cell on the location of the pixel and set the height according to the pixel color
                cells[i, j] = Instantiate(cellPrefab) as MazeCell;
                cells[i, j].transform.parent = transform;
                cells[i, j].name = "Maze Cell (" + i + ", " + j + ")";

                // If the cell is a floor element
                if(levelMap.GetPixel(i, j).grayscale > 0)
                    // Render floor texture
                    cells[i, j].transform.GetChild(0).renderer.material.mainTexture = textureFloor;
                else
                    // Render wall texture
                    cells[i, j].transform.GetChild(0).renderer.material.mainTexture = textureWall;

				cells[i, j].transform.localPosition = new Vector3(i*cells[i,j].transform.localScale.x, -levelMap.GetPixel(i, j).grayscale * cells[i, j].transform.localScale.y, j*cells[i,j].transform.localScale.x);
            }
        }

        // Place maze at the center of the screen
        this.transform.localPosition = new Vector3(-levelMap.width / 2, 0, -levelMap.height / 2);
    }

}
