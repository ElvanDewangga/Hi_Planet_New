using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Aiming_Direction : MonoBehaviour {
    [SerializeField]
   GameObject Aiming_Direction_Object_Prefab;
   [HideInInspector]
   public Aiming_Direction_Object distanceVisualizer; // Instance dari visualisasi jarak
    GameObject Go_distanceVisualizer;
    [SerializeField] GameObject TitikTengah;
    // Char_Technique (not Use), DualJoystickSource
   public void On_Aiming_Direction (Transform From, float moveSpeed, float Cd_Time) {
         // Buat objek visualisasi jarak (misalnya, cube)
        Go_distanceVisualizer = Instantiate(Aiming_Direction_Object_Prefab.gameObject, From.transform.position, Quaternion.identity);
        distanceVisualizer = Go_distanceVisualizer.GetComponent <Aiming_Direction_Object> ();
        // Hitung jarak yang akan ditempuh objek sebelum dihancurkan
        float totalDistance = moveSpeed * Cd_Time;

        // Dapatkan ukuran peluru (misal panjang peluru dari prefab) di bagi 2 karena vfx yang ada di MeshRenderer berada di tengah
       // float bulletLength = From.gameObject.GetComponent<MeshRenderer>().bounds.size.x/2; // Asumsi peluru bergerak di sumbu x (right)

        // Perpanjang totalDistance dengan ukuran peluru
      //  totalDistance += bulletLength;

        // Set ukuran visualisasi jarak (misalnya, scale dari cube untuk menunjukkan jarak)
        distanceVisualizer.transform.localScale = new Vector3(totalDistance, 1.5f, 1.5f); // Misal panjang saja yang berubah
     //   distanceVisualizer.transform.position = From.transform.position + From.transform.right * (totalDistance / 2); // Tempatkan di tengah jarak
        distanceVisualizer.transform.position = From.transform.position + From.transform.right * (1 / 2);
        distanceVisualizer.GetComponent <Aiming_Direction_Object> ().SetTitikTengah(TitikTengah);
       // distanceVisualizer.transform.position = new Vector3 (distanceVisualizer.transform.position.x, distanceVisualizer.transform.position.y, From.transform.position.z);
        distanceVisualizer.gameObject.SetActive (true);
          Go_distanceVisualizer.gameObject.SetActive (true);
   }

    //  Char_Data_Variant_Attack (Vfx Aiming) : FUNCTION SAMA DGN DIATAS HANYA BEDA RETURN
   public GameObject On_Get_Aiming_Direction (Transform From, float moveSpeed, float Cd_Time, Vector3 Collider_Scale) {
         // Buat objek visualisasi jarak (misalnya, cube)
        Go_distanceVisualizer = Instantiate(Aiming_Direction_Object_Prefab.gameObject, From.transform.position, Quaternion.identity);
        Go_distanceVisualizer.GetComponent <BoxCollider> ().size = Collider_Scale;
        distanceVisualizer = Go_distanceVisualizer.GetComponent <Aiming_Direction_Object> ();
        // Hitung jarak yang akan ditempuh objek sebelum dihancurkan
        float totalDistance = moveSpeed * Cd_Time;

        // Dapatkan ukuran peluru (misal panjang peluru dari prefab) di bagi 2 karena vfx yang ada di MeshRenderer berada di tengah
        // float bulletLength = From.gameObject.GetComponent<MeshRenderer>().bounds.size.x/2; // Asumsi peluru bergerak di sumbu x (right)

        // Perpanjang totalDistance dengan ukuran peluru
        // totalDistance += bulletLength;

        // Set ukuran visualisasi jarak (misalnya, scale dari cube untuk menunjukkan jarak)
        distanceVisualizer.transform.localScale = new Vector3(totalDistance, 1.5f, 1.5f); // Misal panjang saja yang berubah
        distanceVisualizer.transform.position = From.transform.position + From.transform.right * (totalDistance / 2); // Tempatkan di tengah jarak
        distanceVisualizer.GetComponent <Aiming_Direction_Object> ().SetTitikTengah(TitikTengah);
       // distanceVisualizer.transform.position = new Vector3 (distanceVisualizer.transform.position.x, distanceVisualizer.transform.position.y, From.transform.position.z);
        distanceVisualizer.gameObject.SetActive (true);
          Go_distanceVisualizer.gameObject.SetActive (true);

          return Go_distanceVisualizer;
   }

   #region DualJoystickSource

    public void On_Aiming () {
        /*
        Char_Attack Ca = Char_Data.Ins.Your_Char_Utama_Script._Char_Attack ;
        Ca.On_Get_Char_Technique_Basic_Attack ().On_Aiming_Direction ();
        */
        On_Aiming_Direction (this.gameObject.transform, 1, 1);
    }

    public void Off_Aiming () {
      
        if (distanceVisualizer != null) {
            Cur_Aim_Position = Go_distanceVisualizer.transform.position;
            Destroy (distanceVisualizer.gameObject);
            distanceVisualizer = null;
        }
    }
   #endregion
   
   #region Char_Data_Variant_Attack
    
   public Vector3 Cur_Aim_Position;
    public string On_Get_4_Direction () {
        string Res = "";
     //   Debug.Log (Cur_Aim_Position);
      //  Debug.Log (_Char_Utama.transform.position);
        if (Cur_Aim_Position.x > this.transform.parent.position.x ) {
            if (Cur_Aim_Position.y > this.transform.parent.transform.position.y) {
                Res = "Up_Right";
            } else {
                Res = "Down_Right";
            }
        } else if (Cur_Aim_Position.x < this.transform.parent.transform.position.x ) {
            if (Cur_Aim_Position.y > this.transform.parent.transform.position.y) {
                Res = "Up_Left";
            } else {
                Res = "Down_Left";
            }
        } else {
            
        }
      
        return Res;
    }
   #endregion
}
