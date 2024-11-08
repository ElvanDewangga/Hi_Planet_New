using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Mirror : MonoBehaviour {
   [SerializeField]
   GameObject Target;
   [SerializeField]
   Vector3 [] A_V3_Position; 
   int Last_Position = 0;
   int Cur_Position = 0;
   Vector3 Last_V3_Position;
   void FixedUpdate () {
   //  Debug.Log (Target.transform.eulerAngles);
        if (Last_V3_Position != Target.transform.eulerAngles) {
            Last_V3_Position = Target.transform.eulerAngles;
            for (int i=0; i < A_V3_Position.Length; i++) {
                if (Last_V3_Position == A_V3_Position[i]) {
                    Cur_Position = i;
                }
            }
            // Debug.Log ("Last Position");
        }
        if (Last_Position != Cur_Position) {
            Last_Position = Cur_Position;
            On_Doing_Rotate ();
        }
   }

   void On_Doing_Rotate () {
        if (Last_Position ==0) {
            this.transform.localEulerAngles = new Vector3 (0,0,0);
          //  Debug.Log ("Rotate 0");
        } else if (Last_Position == 1) {
            this.transform.localEulerAngles = new Vector3 (0,180,0);
           // Debug.Log ("Rotate 1");
        }
   }
}
