using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))] // Tambahkan MeshCollider secara otomatis
public class TilemapToMesh : MonoBehaviour
{
    public Tilemap tilemap;
    public float tileDepth = 1f; // Ketebalan tile dalam mesh 3D
    private MeshFilter meshFilter;
    private MeshCollider meshCollider; // Tambahkan referensi ke MeshCollider

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>(); // Ambil referensi MeshCollider

        if (tilemap == null)
        {
            Debug.LogError("Tilemap reference is missing. Please assign a Tilemap GameObject.");
            return;
        }

        GenerateMeshFromTilemap();
    }

    void GenerateMeshFromTilemap()
    {
        // Dapatkan batas tilemap (area yang diisi tile)
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int[] positions = new Vector3Int[bounds.size.x * bounds.size.y];
        TileBase[] allTiles = new TileBase[bounds.size.x * bounds.size.y];

        int index = 0;
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(position);

                if (tile != null)
                {
                    positions[index] = position;
                    allTiles[index] = tile;
                    index++;
                }
            }
        }

        // Buat daftar untuk vertex dan triangles
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();

        // Loop melalui tile yang ditemukan dan buat mesh berdasarkan posisi tile
        foreach (Vector3Int pos in positions)
        {
            if (tilemap.GetTile(pos) != null)
            {
                AddTileMesh(pos, vertices, triangles, uv);
            }
        }

        // Buat mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Set mesh ke MeshFilter
        meshFilter.mesh = mesh;

        // Set mesh ke MeshCollider untuk membuatnya solid
        meshCollider.sharedMesh = mesh;
    }

    void AddTileMesh(Vector3Int position, List<Vector3> vertices, List<int> triangles, List<Vector2> uv)
    {
        Vector3 basePosition = tilemap.GetCellCenterLocal(position); // Menggunakan GetCellCenterWorld untuk posisi yang tepat
        Vector3 tileSize = tilemap.cellSize; // Mendapatkan ukuran tile dari Tilemap

        // Buat vertex untuk tile
        int vertexIndex = vertices.Count;
        vertices.Add(basePosition + new Vector3(0, 0, 0)); 
        vertices.Add(basePosition + new Vector3(tileSize.x, 0, 0)); 
        vertices.Add(basePosition + new Vector3(tileSize.x, tileSize.y, 0)); 
        vertices.Add(basePosition + new Vector3(0, tileSize.y, 0)); 

        // Tambahkan depth (ketebalan) untuk membuat tile 3D
        vertices.Add(basePosition + new Vector3(0, 0, -tileDepth)); 
        vertices.Add(basePosition + new Vector3(tileSize.x, 0, -tileDepth)); 
        vertices.Add(basePosition + new Vector3(tileSize.x, tileSize.y, -tileDepth)); 
        vertices.Add(basePosition + new Vector3(0, tileSize.y, -tileDepth)); 

        // Buat triangles (persegi depan dan belakang)
        triangles.AddRange(new int[] {
            vertexIndex + 0, vertexIndex + 2, vertexIndex + 1,
            vertexIndex + 0, vertexIndex + 3, vertexIndex + 2,
            vertexIndex + 4, vertexIndex + 5, vertexIndex + 6,
            vertexIndex + 4, vertexIndex + 6, vertexIndex + 7
        });

        // Buat UV mapping (jika ingin menggunakan material)
        uv.AddRange(new Vector2[] {
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
        });
    }
}
