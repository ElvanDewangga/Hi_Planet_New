using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Look_At : MonoBehaviour
{
    public Transform target; // Target yang ingin dilihat
    public float rotationOffset = 0f; // Offset tambahan untuk rotasi

    void Update()
    {
        if (target != null)
        {
            // Hitung arah dari objek ini ke target pada sumbu X dan Y saja
            Vector3 direction = target.position - transform.position;
            direction.z = 0; // Mengabaikan perubahan pada sumbu Z

            // Jika arah tidak nol, hitung rotasi untuk sumbu Z
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                // Tambahkan offset ke rotasi
                angle += rotationOffset;

                // Set rotasi pada sumbu Z saja, dengan offset tambahan
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
