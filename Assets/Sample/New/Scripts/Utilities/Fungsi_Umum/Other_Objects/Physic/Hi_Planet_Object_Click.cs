using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hi_Planet_Object_Click : Object_Click {
    
   public override void On_Click_Object () {
    if (Code_Event == "Character_Object") {
        if (this.gameObject.name == "Clone_Enemy") {
           // Debug.Log ("Enemy Click");
            Enemy_Test_Scene.Ins._Enemy_Test_Canvas.On_Char_Utama_Select (this.gameObject);
            Enemy_Test_Scene.Ins._Enemy_Test_Canvas.On_Open_Option_Panel ();
        }
    }
   }
}
