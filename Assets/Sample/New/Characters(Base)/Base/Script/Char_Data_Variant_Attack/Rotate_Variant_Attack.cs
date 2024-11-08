using UnityEngine;

public class Rotate_Variant_Attack : MonoBehaviour
{
    // [SerializeField]
    private bool b_Setup;

    private Quaternion Real_Rotation;

    //  Vector3 rotateSpeed = new Vector3(1, 0, 0); // Rotasi di sumbu X dan Z saja
    private float speed = 50.0f;

    // [SerializeField]
    private Transform Titik_Tengah;

    // Update is called once per frame
    private void Update()
    {
        if (b_Setup)
        {
            // Ambil posisi world dari Titik_Tengah
            var worldPosition = Titik_Tengah.position;

            // Hanya putar pada sumbu X dan Z
            var rotateAxis = new Vector3(1, 1, 0); // Sumbu X dan Z

            transform.RotateAround(worldPosition, Vector3.forward, speed * 10 * Time.deltaTime);
            //  transform.RotateAround(centerPoint.position, Vector3.1up, rotateSpeed.y * speed * Time.deltaTime);

            // Kunci rotasi objek agar tetap di rotasi asli (orientasi tidak berubah)
            transform.rotation = Real_Rotation;
        }
    }

    // Optional: Menampilkan posisi Titik_Tengah di Scene View menggunakan Gizmos
    private void OnDrawGizmos()
    {
        if (Titik_Tengah != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Titik_Tengah.position, 0.2f); // Menampilkan lingkaran kecil di posisi Titik_Tengah
        }
    }

    // Char_Data_Variant_Attack
    public void On_Setup(Transform Titik_Tengah_V, float Speed_V)
    {
        Titik_Tengah = Titik_Tengah_V;
        //  rotateSpeed = rotateSpeed_V;
        speed = Speed_V;
        Real_Rotation = transform.rotation; // Simpan rotasi asli objek
        b_Setup = true;

        // Debug untuk cek posisi titik tengah
        Debug.Log("Titik Tengah Position: " + Titik_Tengah.position);
    }
}