using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Starsky {
    // Header_Canvas, GameObject_And_Destroy
    public class Star_GameObject {
        
        #region Example
        /*
        using System;
        
        void On_Example() {
            Star_GameObject St = new Star_GameObject();
            int i = 0;
            for (i = 0; i < Maximum_Slot; i ++) {
                    St.On_Instantiate (Data_Slot_System_Sample,Layout_Data_Slot, true);       
                    St.On_Input_Method ("Data_Slot_System","On_Check_Cur_Slot",new Type[] { typeof(int)}, new object[] {(i + 1)});
                    St.On_Input_Method ("Clone_Sample","On_Set_b_Clone",new Type[] { typeof(bool)}, new object[] {true});
            } 
            Load_Data_Viewport.GetComponent <Scroll_Rect_Position> ().ScrollToTopPosition ();
        }
        */
        
        #endregion
        public GameObject Target_GO;
        public void On_Instantiate (GameObject Sample, Transform Layout, bool Set_Active) {
            Target_GO = GameObject.Instantiate (Sample);
            Target_GO.transform.SetParent (Layout);
            
            Target_GO.gameObject.SetActive (Set_Active);
        }
        
        public void On_Input_Method (string Type_Script, string Method_Name, Type [] Type_V, object [] Isi_V)
        {
            /*
             GetComponentByName Gb = new GetComponentByName ();
            Type s = Gb.On_Get_Name (Type_Script);
            if (s!= null) {
                C_Get_Component Cc = new C_Get_Component ();
                Component component = Cc.On_Get_Component (Target_GO, Type_Script);
                
                Call_Method_Type Cm = new Call_Method_Type ();
                Cm.On_Call_Method (component,s,Method_Name, Type_V, Isi_V);

            }
            */
        }
       
        // Implementasion Example On C# Header_Canvas 
        // Call_Method_Type
    }

    public class C_Get_Component : MonoBehaviour{
        public Component On_Get_Component (GameObject Tar, string Type_Script_V) {
            return Tar.GetComponent(Type_Script_V);
        }
    }
    
}
