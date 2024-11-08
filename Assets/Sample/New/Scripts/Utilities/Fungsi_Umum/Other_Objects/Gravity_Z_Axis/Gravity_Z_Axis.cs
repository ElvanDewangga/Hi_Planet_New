using UnityEngine;

public class Gravity_Z_Axis : MonoBehaviour
{
    public float gravityStrength = -9.81f; // Kekuatan gravitasi (negatif untuk menarik ke bawah)
    private Vector3 velocity;

    void Update()
    {
        // Jika objek berada di atas tanah (misalnya Z > 0), terapkan gravitasi
        if (transform.position.z > 0)
        {
            // Tambahkan gravitasi pada kecepatan objek di sumbu Z
            velocity.z += gravityStrength * Time.deltaTime;
        }
        else
        {
            // Set kecepatan menjadi nol saat mencapai permukaan tanah (Z <= 0)
            velocity.z = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        // Terapkan kecepatan ke posisi objek
        transform.position += velocity * Time.deltaTime;
    }
}