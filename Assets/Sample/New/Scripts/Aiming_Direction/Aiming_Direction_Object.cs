using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Direction_Object : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        // Menghitung arah dari posisi peluru saat ini ke Sphere_Direction
            Vector3 direction = this.transform.position - Titik_Tengah.transform.position;
            direction.z = 0; // Mengabaikan perubahan pada sumbu Z

            // Menghitung sudut menggunakan Atan2
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Debugging deltaX dan deltaY untuk memastikan arah yang benar
           // Debug.Log("Delta Y: " + direction.y + ", Delta X: " + direction.x);

            // Set rotasi peluru berdasarkan arah yang sudah dihitung
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    #region Aiming_Direction
    // Aiming_Direction
    public void SetTitikTengah (GameObject titikTengah) {
        Titik_Tengah = titikTengah;
      //   this.gameObject.transform.SetParent (Titik_Tengah.Sphere_Direction.transform);
    }
    #endregion
    #region DualJoystickSource
    float Aiming_Distance = 0.0f;
    float rotationSpeed = 10;
    GameObject Titik_Tengah;
   public void On_Aiming_Direction_Object_Rotate_Around(Vector3 Distance_V)
{
    if (Titik_Tengah != null)
    {
        // Normalisasi arah agar konsisten
        Distance_V.Normalize();

        // Dapatkan radius dari Titik_Tengah (karakter)
        float charRadius = 0.0f;
        Collider charCollider = Titik_Tengah.GetComponent<Collider>();
        if (charCollider != null)
        {
            // Ambil nilai extents yang lebih besar antara x dan y untuk menjaga keseragaman radius
            charRadius = Mathf.Max(charCollider.bounds.extents.x, charCollider.bounds.extents.y);
        }

        // Dapatkan ukuran (radius) dari Aiming_Direction_Object
        float aimingObjectRadius = 0.0f;
        Collider aimingCollider = this.gameObject.GetComponent<Collider>();
        if (aimingCollider != null)
        {
            // Gunakan extents yang lebih besar untuk menjaga konsistensi jarak
            aimingObjectRadius = Mathf.Max(aimingCollider.bounds.extents.x, aimingCollider.bounds.extents.y);
        }

        // Tetapkan jarak final dari karakter ke posisi aiming
        float finalDistance = charRadius + Aiming_Distance + aimingObjectRadius;

        // Hitung posisi baru tanpa mempengaruhi skala karakter
        Vector3 newPosition = Titik_Tengah.transform.position + Distance_V * finalDistance;
        newPosition = new Vector3(newPosition.x, newPosition.y, Titik_Tengah.transform.position.z); //61.5f // Pastikan posisi Z tetap sama (untuk 2D)

        // Perbarui posisi Sphere_Direction
        this.gameObject.transform.position = newPosition;

        // Lakukan rotasi dengan Time.deltaTime untuk rotasi yang halus
        this.gameObject.transform.RotateAround(Titik_Tengah.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        // Set posisi target spawn di ujung objek Aiming_Direction_Object
        Vector3 spawnPosition = newPosition + (Distance_V * aimingObjectRadius);

        // Debugging untuk arah dan posisi baru
       // Debug.Log("Aiming Object Position: " + newPosition + " Final Distance: " + finalDistance);
    }
}



    #endregion
}
