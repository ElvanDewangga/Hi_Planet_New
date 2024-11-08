using UnityEngine;

public class Draw_Zone : MonoBehaviour
{
    public float attackRadius = 2f; // Jarak serangan dalam satuan Unity
    public Color gizmoColor = Color.red; // Warna yang ditampilkan di Scene

    // Method ini akan menggambar area serangan di Scene View saat game tidak sedang berjalan
    private void OnDrawGizmos()
    {
        // Mengatur warna Gizmo
        Gizmos.color = gizmoColor;
        // Menggambar lingkaran di sekitar posisi GameObject untuk menandai zona serangan
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    // Method ini bisa dipanggil untuk mendeteksi apakah musuh ada di dalam zona serangan
    public bool IsEnemyInRange(Transform enemy)
    {
        float distance = Vector3.Distance(transform.position, enemy.position);
        return distance <= attackRadius;
    }
}
