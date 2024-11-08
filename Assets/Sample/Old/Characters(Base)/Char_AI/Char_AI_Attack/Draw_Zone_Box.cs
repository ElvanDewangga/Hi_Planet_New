using UnityEngine;

public class Draw_Zone_Box : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(2f, 2f, 2f); // Ukuran kotak (X, Y, Z)
    public Color gizmoColor = Color.green; // Warna yang ditampilkan di Scene
    public Vector3 attackCenterOffset = Vector3.zero; // Offset untuk pusat area serangan

    // Method ini akan menggambar area serangan berbentuk kotak di Scene View saat game tidak sedang berjalan
    private void OnDrawGizmos()
    {
        // Mengatur warna Gizmo
        Gizmos.color = gizmoColor;

        // Menggambar kotak di sekitar posisi GameObject dengan offset
        Gizmos.DrawWireCube(transform.position + attackCenterOffset, boxSize);
    }

    // Method ini bisa dipanggil untuk mendeteksi apakah musuh ada di dalam zona serangan
    // Menambahkan parameter 'position' untuk menentukan pusat serangan secara dinamis
    public bool IsEnemyInRange(Transform enemy, Vector3 attackCenter)
    {
        // Menghitung jarak absolut antara posisi enemy dan pusat area serangan
        Vector3 difference = enemy.position - attackCenter;
        return Mathf.Abs(difference.x) <= boxSize.x / 2 &&
            Mathf.Abs(difference.y) <= boxSize.y / 2 &&
            Mathf.Abs(difference.z) <= boxSize.z / 2;
    }

}