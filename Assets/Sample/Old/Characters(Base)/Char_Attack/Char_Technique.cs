using System.Collections;
using System.Collections.Generic;
using GameKit.Dependencies.Utilities.Types;
using UnityEngine;

public class Char_Technique : MonoBehaviour
{
    #region Char_Attack / A_Aseprite
    [HideInInspector]
    public Char_Utama _Char_Utama;
    public void On_Set_Char_Utama (Char_Utama Cu) {
        _Char_Utama = Cu;
    }

    #endregion

    public string Technique_Type = "";
    // public float Technique_Time = 0.0f;
    // Slash = Collider tidak ada script Char_Attack_Move
    // Shoot = Collider ada script Char_Attack_Move
    [SerializeField]
    GameObject Col_Damage;
    [SerializeField]
    Vector3 Offset; 
    Vector3 Dicretion_Offset;
    
    string On_Get_Technique_Type () {
        string Res;
        if (Col_Damage.GetComponent<Char_Attack_Move>() != null) {
            Res = "Shoot";
        } else {
            Res = "Slash";
        }
        return Res;
    }
    #region Char_Data_Hit
    [SerializeField]
    string [] A_Char_Data_Hit_Name = new string [0];

    public string [] On_Get_A_Char_Data_Hit_Name () {
        return A_Char_Data_Hit_Name;
    }
    #endregion

    #region Char_Data_Variant_Attack
    [SerializeField]
    string [] A_Char_Data_Variant_Attack = new string [0];

    public string [] On_Get_A_Char_Data_Variant_Attack_Name () {
        return A_Char_Data_Variant_Attack;
    }

    #endregion
    #region Char_Attack
    
    public void On_Slash (Char_Utama Cu) {
        if (Technique_Type == "") {
            Technique_Type = On_Get_Technique_Type ();
        }
        if (_Char_Utama.Char_Direction_2d == "Left") {
            Dicretion_Offset = Offset * -1;
        } else if (_Char_Utama.Char_Direction_2d == "Right") {
            Dicretion_Offset = Offset;
        }

        if (Last_Variant_Attack !=null) {
            Destroy (Last_Variant_Attack);
            Last_Variant_Attack = null;
        }
        if (Technique_Type == "Slash") {
      

            GameObject Bullet = GameObject.Instantiate(Col_Damage);
            Char_Data.Ins.Spawn_System_Parenting (Bullet);
            // Simpan skala lokal Col_Damage sebelum dipisah
            Vector3 worldScale = Col_Damage.transform.lossyScale;

            Bullet.transform.position = Col_Damage.transform.position + Dicretion_Offset;
            Bullet.transform.rotation = Col_Damage.transform.rotation; // Menyamakan rotasi'

            Bullet.transform.localScale = worldScale;
            if(Bullet.GetComponent<Char_Attack_Collider_Sample>().useparent == false)
            {
                Bullet.transform.SetParent(null);
            }else{
                Bullet.transform.SetParent(_Char_Utama.transform);
            }

            Bullet.GetComponent<SelfDestruct>().enabled = true;
            Bullet.SetActive(true);

            if (!Game_Mode.Ins.b_Test_Mode) {
                if (!b_Vfx_Shoot) {
                    Col_Damage.gameObject.SetActive (false);
                    b_Vfx_Shoot = true;
                    Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Bullet, Technique_Type);
                } 
            } else {
                Col_Damage.gameObject.SetActive (false);
                b_Vfx_Shoot = true;
                Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Bullet, Technique_Type);
            }
        } else if (Technique_Type == "Shoot") {
           
            if (_Char_Utama.Owner == "Player") {
                // Instantiate peluru
                GameObject Bullet = GameObject.Instantiate(Col_Damage);
                Char_Data.Ins.Spawn_System_Parenting (Bullet);
                // Simpan skala lokal Col_Damage sebelum dipisah
                Vector3 worldScale = Col_Damage.transform.lossyScale;

                // Set posisi awal Bullet pada posisi karakter utama atau posisi yang sesuai
                Bullet.transform.position = _Char_Utama.transform.position;  // Atur posisi awal peluru

                // Menghitung arah dari posisi peluru saat ini ke Sphere_Direction
                Vector3 direction = _Char_Utama.Sphere_Direction.transform.position - Bullet.transform.position;
                direction.z = 0; // Mengabaikan perubahan pada sumbu Z

                // Menghitung sudut menggunakan Atan2, yang menghitung arah dari 'Bullet' ke 'Sphere_Direction'
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Debugging deltaX dan deltaY untuk memastikan arah yang benar
                float deltaY = _Char_Utama.Sphere_Direction.transform.position.y - Bullet.transform.position.y;
                float deltaX = _Char_Utama.Sphere_Direction.transform.position.x - Bullet.transform.position.x;
                Debug.Log("Delta Y: " + deltaY + ", Delta X: " + deltaX);

                // Set rotasi peluru berdasarkan arah yang sudah dihitung
                Bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

                // Posisikan bullet pada posisi Sphere_Direction dengan offset jika perlu
                Bullet.transform.position = _Char_Utama.transform.position + Dicretion_Offset;

                // Set parent peluru ke null untuk melepaskannya dari hirarki objek
                Bullet.transform.SetParent(null);

                // Kembalikan skala peluru sesuai skala aslinya
                Bullet.transform.localScale = worldScale;

                // Aktifkan komponen SelfDestruct jika ada, untuk mengatur durasi hidup peluru
                
                
                Bullet.SetActive(true);

                if (!Game_Mode.Ins.b_Test_Mode) {
                if (!b_Vfx_Shoot) {
                    Col_Damage.gameObject.SetActive (false);
                    b_Vfx_Shoot = true;
                    Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Bullet, Technique_Type);
                    
                } 
                } else {
                    Col_Damage.gameObject.SetActive (false);
                    b_Vfx_Shoot = true;
                    Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Bullet, Technique_Type);
                }
                // Debug.Log (Destroy_Time);
                Bullet.GetComponent<SelfDestruct>().On_Set_Time (Destroy_Time);
                Bullet.GetComponent<SelfDestruct>().enabled = true;
            // Bullet.GetComponent<Char_Attack_Move> ().On_Set_Data (this); Pindah ke On_Aiming_Direction ()
            } else {
                /*
                if (!b_Vfx_Shoot) {
                    Col_Damage.gameObject.SetActive (false);
                    b_Vfx_Shoot = true;
                    Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Col_Damage, Technique_Type);
                } 
                */

                GameObject Bullet = GameObject.Instantiate(Col_Damage);
                Char_Data.Ins.Spawn_System_Parenting (Bullet);
                // Simpan skala lokal Col_Damage sebelum dipisah
                Vector3 worldScale = Col_Damage.transform.lossyScale;

                Bullet.transform.position = Col_Damage.transform.position + Dicretion_Offset;
                Bullet.transform.rotation = _Char_Utama._Char_Animation._A_Aseprite.gameObject.transform.rotation;
                Bullet.transform.localScale = worldScale;

                if(Bullet.GetComponent<Char_Attack_Collider_Sample>().useparent == false){Bullet.transform.SetParent(null);}
                else{Bullet.transform.SetParent(_Char_Utama.transform);}
                if(Bullet.TryGetComponent(out Char_Attack_Move cam))
                {
                    cam._Char_Technique = this;
                }

                
               // Debug.Log ("Enemy Rotation");
                Bullet.SetActive(true);
                if (!Game_Mode.Ins.b_Test_Mode) {
                if (!b_Vfx_Shoot) {
                    Col_Damage.gameObject.SetActive (false);
                    b_Vfx_Shoot = true;
                    Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Bullet, Technique_Type);
                } 
                } else {
                    Col_Damage.gameObject.SetActive (false);
                    b_Vfx_Shoot = true;
                    Cu._Char_Utama_Source.On_Variant_Attack_Vfx (this.gameObject, Bullet, Technique_Type);
                }
                //Debug.Log (Destroy_Time);
                Bullet.GetComponent<SelfDestruct>().On_Set_Time (Destroy_Time);
                Bullet.GetComponent<SelfDestruct>().enabled = true;
                
            }
        } 
    }

    #endregion
    #region Aiming_Direction
    public void On_Aiming_Direction () {
        Col_Damage.GetComponent<Char_Attack_Move> ().On_Set_Data (this);
    }
    #endregion
    #region Char_AI_Attack
    public string Code_Char_AI_Play = "";
    #endregion
    #region Char_AI_Play_Attack
    public Object_Variant_Config On_Get_Object_Variant_Config () {
      // Latest  return Char_Data.Ins._Char_Data_Variant_Attack.On_Get_Object_Variant_Config (A_Char_Data_Variant_Attack[0]);
        return null;
    }
    #endregion
    #region Cd_Time
    // Char_AI_Play_Attack, Char_Attack
    [HideInInspector]
    public float Cd_Time = 0.0f;
    public float Destroy_Time = 0.0f;
    #endregion
    #region Technique_Type
        #region Shoot
        bool b_Vfx_Shoot = false;
        #endregion
    #endregion
    #region Char_Data_Variant_Attack
    GameObject Last_Variant_Attack;
    public void On_Set_Last_Variant_Attack (GameObject Go) {
        Last_Variant_Attack = Go;
    }

    public void On_Set_Cd_Time (float Cd_V) {
        Cd_Time = Cd_V;
    }

    public void On_Destroy_Time (float Destroy_Time_V) {
        Destroy_Time = Destroy_Time_V;
        Col_Damage.GetComponent <SelfDestruct> ().On_Set_Time (Destroy_Time);
    }

    #endregion
    
    
   
}
