using UnityEngine;
using UnityEngine.Tilemaps;

public class Bot : MonoBehaviour {
    public Tilemap tilemap;           // Reference to the Tilemap component
    public TileBase sandTile;         // Tile for sand (what grass turns into)
    public float stepDelay = 1f;      // Delay between each movement (in seconds)

    private Vector3Int currentTilePos;  // The bug's current tile position in the grid
    private bool isMoving = false;      // Whether the bug is currently moving or not

    // Matrix reference to interact with tiles (1 = Grass, 0 = Sand)
    private int[,] mapMatrix;

    void Start() {
        // Initialize the bug's position on the grid (starting tile)
        currentTilePos = new Vector3Int(0, 0, 0);  // Start at (0, 0)

        // Align the bug to the exact world position of the tile (center it on the tile)
        Vector3 startPosition = tilemap.GetCellCenterWorld(currentTilePos);
        transform.position = startPosition;  // Place the bug at the center of the tile

        // Get the map matrix from the MapGenerator
        mapMatrix = FindObjectOfType<MapGenerator>().mapMatrix;

        // Start moving the bug on a timed basis
        InvokeRepeating("MoveBug", stepDelay, stepDelay);
    }

    // Function to move the bug from one tile to another
    void MoveBug() {
        if (!isMoving) {
            // Process the tile the bug is currently on
            

            // Pick a new tile and move to it
            Vector3Int newTilePos = PickNewTarget();

            // Update the bug's position
            currentTilePos = newTilePos;

            // Align the bug's position to the center of the new tile
            Vector3 newPosition = tilemap.GetCellCenterWorld(currentTilePos);
            transform.position = newPosition;  // Instantly move to the new tile

            isMoving = false;  // Ready for the next move

            ProcessTile(currentTilePos);
        }
    }

    // Processes the current tile: if it's grass, turn it into sand
    private void ProcessTile(Vector3Int tilePos) {
        // Get the current tile type from the matrix
        int tileType = mapMatrix[tilePos.x, tilePos.y];

        if (tileType == 0)  // If it's grass (1), eat it and turn it into sand
        {
            // Change the matrix to represent the new tile type (0 = sand)
            mapMatrix[tilePos.x, tilePos.y] = 1;

            // Update the Tilemap with the sand tile
            tilemap.SetTile(tilePos, sandTile);
        }
    }

    // Pick a new random target tile (up, down, left, right)
    private Vector3Int PickNewTarget() {
        Vector3Int newTilePos = currentTilePos;

        // Choose a random direction (up, down, left, right)
        int direction = Random.Range(0, 4);

        switch (direction) {
            case 0: newTilePos += new Vector3Int(1, 0, 0); break; // Move right
            case 1: newTilePos += new Vector3Int(-1, 0, 0); break; // Move left
            case 2: newTilePos += new Vector3Int(0, 1, 0); break; // Move up
            case 3: newTilePos += new Vector3Int(0, -1, 0); break; // Move down
        }

        // Make sure the new tile position is within the bounds of the map
        if (newTilePos.x >= 0 && newTilePos.x < mapMatrix.GetLength(0) &&
            newTilePos.y >= 0 && newTilePos.y < mapMatrix.GetLength(1)) {
            return newTilePos;
        } else {
            // If the new tile is out of bounds, try picking a new target again
            return PickNewTarget();
        }
    }
}
