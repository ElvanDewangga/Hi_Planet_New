using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Object_Rotation_Target_With_Direction : MonoBehaviour {
        Transform Target;
        [SerializeField] 
        Transform Form;
        float rotationOffset;
       // [SerializeField]
        string Direction_Up = "Up";
        [SerializeField]
        bool b_Direction_Negative = false;
        // Update is called once per frame
        void Update()
        {
            On_Rotation ();
        }

        void On_Rotation () {
            if (Target != null) {
                Vector3 direction = Target.transform.position - Form.transform.position;
                direction.z = 0; // Mengabaikan perubahan pada sumbu Z

                // Menghitung sudut menggunakan Atan2
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (Target.transform.position.y < Form.transform.position.y) {
                    Direction_Up = "Down";
                    if (!b_Direction_Negative) {
                        angle += rotationOffset;
                    } else {
                        angle += rotationOffset * -1;
                    }
                    
                } else {
                    Direction_Up = "Up";
                    
                    if (!b_Direction_Negative) {
                        angle += rotationOffset * -1;
                    } else {
                        angle += rotationOffset;
                    }
                }
                // Tambahkan offset rotasi
                

                // Debugging deltaX dan deltaY untuk memastikan arah yang benar
                // Debug.Log("Delta Y: " + direction.y + ", Delta X: " + direction.x);

                // Set rotasi peluru berdasarkan arah yang sudah dihitung dan offset
                this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        #region Char_Animation
        // PlayerBody
        public void On_Set_Target (Transform Trs) {
            Target = Trs;
            if (_Char_Back) {
                V3_Char_Back_Pos_Real = _Char_Back.transform.localPosition;
            }
            if (_Char_Back) {
                V3_Char_Forward_Pos_Real = _Char_Forward.transform.localPosition;
            } 
        }
        #endregion
        #region A_Aseprite
        public Transform On_Get_Target () {
            return Target;
        }
        #endregion
        #region Source
            #region Char_Direction_2d - Char_Utama_Source
            // Char_Data_Variant_Attack :
            [SerializeField] 
            public float Left_rotationOffset = 0f; // Offset rotasi dalam derajat
            [SerializeField] 
            public float Right_rotationOffset = 0f; // Offset rotasi dalam derajat
            
            public string Char_Direction_2d = ""; // "Left", "Right"
            // CharBase :
            public void On_Get_Char_Direction_2d (string direction) {

                Char_Direction_2d = direction;
                
                if (Char_Direction_2d == "Left") {
                    rotationOffset = Left_rotationOffset;
                    this.transform.localScale = new Vector3 (1,-1,1);
                } else if (Char_Direction_2d == "Right") {
                    rotationOffset = Right_rotationOffset;
                    this.transform.localScale = new Vector3 (1,1,1);
                }
            }

            // Char_Data_Variant_Attack : 
            public GameObject _Char_Back;
            Vector3 V3_Char_Back_Pos_Real;
            public void On_Set_Local_Position_Char_Back (Vector3 V3_Fix_Position_V) {
                _Char_Back.transform.localPosition = V3_Char_Back_Pos_Real + V3_Fix_Position_V;
            }
            public GameObject _Char_Forward;
            Vector3 V3_Char_Forward_Pos_Real;
            public void On_Set_Local_Position_Char_Forward(Vector3 V3_Fix_Position_V) {
                _Char_Forward.transform.localPosition = V3_Char_Forward_Pos_Real + V3_Fix_Position_V;
            }
            #endregion
           
        #endregion
}
