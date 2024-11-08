using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Target_Rotation : MonoBehaviour
{
        // [SerializeField] 
        Transform Target;
        [SerializeField] 
        Transform Form;
        [SerializeField] 
        float rotationOffset = 0f; // Offset rotasi dalam derajat
        
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

                
                // Tambahkan offset rotasi
                angle += rotationOffset;

                // Debugging deltaX dan deltaY untuk memastikan arah yang benar
                // Debug.Log("Delta Y: " + direction.y + ", Delta X: " + direction.x);

                // Set rotasi peluru berdasarkan arah yang sudah dihitung dan offset
                this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        #region Char_Animation
        public void On_Set_Target (Transform Trs) {
            Target = Trs;
        }
        #endregion
}
