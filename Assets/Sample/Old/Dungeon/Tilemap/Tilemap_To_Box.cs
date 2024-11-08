using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapToBoxCollider : MonoBehaviour
{
    public Tilemap tilemap;
    public Material colliderMaterial;  // Material untuk Mesh Renderer
    public Vector3 boxObjectScale = new Vector3(1f, 1f, 1f);  // Custom scale for the GameObject containing BoxCollider
    public Vector2 boxcolsize;
    void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }

        GenerateColliders();
    }

    void GenerateColliders()
    {
        // Dapatkan batas tilemap (area yang diisi tile)
        BoundsInt bounds = tilemap.cellBounds;

        // Loop melalui semua tile yang ada dalam tilemap
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                // Jika ada tile di posisi ini, tambahkan BoxCollider dan Mesh
                if (tile != null)
                {
                    AddBoxCollider(tilePosition);
                }
            }
        }
    }

    void AddBoxCollider(Vector3Int tilePosition)
    {
        // Buat GameObject kosong sebagai collider untuk tile ini
        GameObject tileColliderObject = new GameObject("TileCollider");
        tileColliderObject.transform.parent = this.transform;  // Set parent ke tilemap
        tileColliderObject.transform.position = tilemap.CellToWorld(tilePosition);  // Set posisi ke tile

        // Set custom scale untuk GameObject
        tileColliderObject.transform.localScale = boxObjectScale;  // Atur scale dari GameObject

        // Tambahkan BoxCollider
        BoxCollider boxCollider = tileColliderObject.AddComponent<BoxCollider>();
        // Set ukuran BoxCollider sesuai dengan ukuran tilemap
        Vector3 tileSize = tilemap.cellSize;
        boxCollider.size = new Vector3(boxcolsize.x, boxcolsize.y, 1f);  // Ukuran asli collider

        // Atur posisi BoxCollider agar sesuai dengan posisi tile
        boxCollider.center = new Vector3(tileSize.x / 2f, tileSize.y / 2f, -0.5f); // Offset ke pusat tile

        // Tambahkan Mesh Filter dan Mesh Renderer
        MeshFilter meshFilter = tileColliderObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = tileColliderObject.AddComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        // Buat mesh untuk kotak
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(tileSize.x, 0, 0),
            new Vector3(tileSize.x, tileSize.y, 0),
            new Vector3(0, tileSize.y, 0),
            new Vector3(0, 0, -1),
            new Vector3(tileSize.x, 0, -1),
            new Vector3(tileSize.x, tileSize.y, -1),
            new Vector3(0, tileSize.y, -1),
        };

        mesh.triangles = new int[]
        {
            // Front face
            0, 2, 1,
            0, 3, 2,
            // Back face
            4, 5, 6,
            4, 6, 7,
            // Left face
            0, 4, 7,
            0, 7, 3,
            // Right face
            1, 2, 6,
            1, 6, 5,
            // Top face
            2, 3, 7,
            2, 7, 6,
            // Bottom face
            0, 1, 5,
            0, 5, 4
        };

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        // Tambahkan material pada MeshRenderer
        if (colliderMaterial != null)
        {
            meshRenderer.material = colliderMaterial;
        }
        else
        {
            // Gunakan material default jika tidak ada material yang disediakan
            meshRenderer.material = new Material(Shader.Find("Standard"));
        }
    }
}
