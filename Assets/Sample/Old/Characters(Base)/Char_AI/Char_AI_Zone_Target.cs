using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_AI_Zone_Target : MonoBehaviour {
   [SerializeField]
   List <GameObject> L_Target_GO = new List<GameObject> ();
   
   void OnTriggerEnter (Collider Co) {
        if (Co.gameObject.tag == "Player") {
            if (!L_Target_GO.Contains (Co.gameObject)) {
                L_Target_GO.Add (Co.gameObject);
            }
        }
   }

   void OnTriggerExit (Collider Co) {
        if (Co.gameObject.tag == "Player") {
            if (L_Target_GO.Contains (Co.gameObject)) {
                L_Target_GO.Remove (Co.gameObject);
            }
        }
   }

   #region Char_AI_Event
   // Char_AI_Follow, Char_AI_Play_Attack (Char_AI_Play_Attack_Shoot)
    public GameObject On_Get_Target_Object () {
        if (L_Target_GO.Count >0) {
            return L_Target_GO[Random.Range (0,L_Target_GO.Count)];
        } else {
            return null;
        }
    }
   #endregion
}
