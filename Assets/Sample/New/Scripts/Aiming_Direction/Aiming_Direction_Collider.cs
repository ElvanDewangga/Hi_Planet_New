using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Direction_Collider : MonoBehaviour {
   [SerializeField]
   List <GameObject> L_Target_GO = new List<GameObject> ();
   
   void OnTriggerEnter (Collider Co) {
        if (Co.gameObject.tag == "Enemy") {
            if (!L_Target_GO.Contains (Co.gameObject)) {
                L_Target_GO.Add (Co.gameObject);
            }
        }
   }

   void OnTriggerExit (Collider Co) {
        if (Co.gameObject.tag == "Enemy") {
            if (L_Target_GO.Contains (Co.gameObject)) {
                L_Target_GO.Remove (Co.gameObject);
            }
        }
   }

   #region Char_Data_Variant_Attack
   public List<GameObject> On_Get_L_Target_Go () {
        return L_Target_GO;
   }
   
   #endregion
}
