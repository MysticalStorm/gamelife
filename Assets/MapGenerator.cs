using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour {
    public Tilemap tilemap;  // Reference to the Tilemap component
    public TileBase[] tiles; // Array to store the loaded tiles dynamically

    // 4x4 Matrix: 0 = Grass, 1 = Water, 2 = Dirt (or any other types)
    public int[,] mapMatrix = new int[,]
    {
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 }
    };

    void Update() {
        // Load all TileBase assets from the "Resources/Sprites/Base" folder
        //tiles = Resources.LoadAll<TileBase>("Sprites/Base");

        // Check if tiles are loaded
        if (tiles.Length == 0) {
            Debug.LogError("No tiles found in the specified folder.");
            return;
        }

        // Generate the map based on the matrix
        GenerateMap();
    }

    // Function to generate the map based on the matrix
    void GenerateMap() {
        int width = mapMatrix.GetLength(0);   // Matrix width
        int height = mapMatrix.GetLength(1);  // Matrix height

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int tileType = mapMatrix[x, y];  // Get the tile type from the matrix

                // Ensure that the tileType corresponds to a valid tile in the tiles array
                if (tileType >= 0 && tileType < tiles.Length) {
                    // Place the tile at position (x, y) in the Tilemap
                    tilemap.SetTile(new Vector3Int(x, y, 0), tiles[tileType]);
                }
            }
        }
    }
}
