using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {
   public SkinComponent [] skinComponents;
   
   [System.Serializable]
   public class SkinComponent {
    public string skinName;
    public GameObject skin;
    public GameObject body;
    public GameObject handLeft;
    public GameObject handRight;
    public GameObject animatorBody;
    public GameObject animatorHandLeft;
    public GameObject animatorHandRight;
    public GameObject A_Accesories;
   }

   // PlayerCharBase :
   public SkinComponent ChangeSkinComponent (string name) {
        SkinComponent result = null;
        foreach (SkinComponent skinComponent in skinComponents) {
            if (name == skinComponent.skinName) {
                result = skinComponent;
                skinComponent.body.SetActive (true);
                skinComponent.handLeft.SetActive (true);
                skinComponent.handRight.SetActive (true);
            } else {
                skinComponent.body.SetActive (false);
                skinComponent.handLeft.SetActive (false);
                skinComponent.handRight.SetActive (false);
            }
        }
        
        PlayerCharBase playerCharBase = this.gameObject.transform.parent.GetComponent <PlayerCharBase> ();
        playerCharBase.handLeftRotation = result.handLeft.GetComponent <Object_Rotation_Target_With_Direction> ();
        playerCharBase.handRightRotation = result.handRight.GetComponent <Object_Rotation_Target_With_Direction> ();
        result.handLeft.GetComponent <Object_Rotation_Target_With_Direction> ().On_Set_Target (playerCharBase.sphereDirection.transform);
        result.handRight.GetComponent <Object_Rotation_Target_With_Direction> ().On_Set_Target (playerCharBase.sphereDirection.transform);
        return result;
   }
}
