using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Visual_Novel_World : MonoBehaviour {
   #region Head
    [SerializeField]
    Canvas Canvas_3d_World;
    [SerializeField]
    GameObject A_Asset;

    // Visual_Novel_System :
   public void On_Start_Visual_Novel_Read () {
      Canvas_3d_World.gameObject.SetActive (true);
      A_Asset.gameObject.SetActive (true);
   }

      // Visual_Novel_System :
      #region Close_Visual_Novel
      public void On_Close_Visual_Novel () {
         Canvas_3d_World.gameObject.SetActive (false);
         A_Asset.gameObject.SetActive (false);
      }
      #endregion
   #endregion
   #region Background
   
    [SerializeField]
    Image Background;

    // Visual_Novel_Event :
    public void On_Set_Background (string Name, string Code) {
      int.TryParse (Code, out int Code_Bg);
      Background.sprite = Visual_Novel_System.Ins._Visual_Novel_Sprite.On_Get_Sprite_From_Name (Name, Code_Bg);
    }
    #endregion
   #region Visual_Novel_Char
    [SerializeField]
    Transform A_Visual_Novel_Char;
    [SerializeField]
    GameObject Visual_Novel_Char_Sample;
     [SerializeField]
     Transform [] A_Transform_Char_Pos; 
   public List <C_Char_2d> L_C_Char_2d = new List<C_Char_2d> ();
   public class C_Char_2d {
      public GameObject Char_2d_GO;
      public string Name;
      public string Style;
   }

   bool b_On_Load_Dict= false;
   Dictionary <int, string> Dict_Char_2d_Fade_In = new Dictionary<int, string> ();
   void On_Load_Dict () {
      Dict_Char_2d_Fade_In = new Dictionary<int, string> ();
      Dict_Char_2d_Fade_In.Add (0, "Fade_In_Left");
      Dict_Char_2d_Fade_In.Add (1, "Fade_In_Right");
   } 
   // Visual_Novel_Event
   public void On_Add_Char_2d (string Name,string Style) {
      if (!b_On_Load_Dict) {
         b_On_Load_Dict = true;
         On_Load_Dict ();
      }

      if (!On_Check_Same_Char (Name)) {
         
         C_Char_2d c = new C_Char_2d ();
         c.Name = Name; c.Style = Style;

         GameObject Ins = GameObject.Instantiate (Visual_Novel_Char_Sample);
         Ins.transform.SetParent (A_Visual_Novel_Char);
         Ins.transform.localPosition = A_Transform_Char_Pos[L_C_Char_2d.Count].localPosition;
         // Ins.gameObject.SetActive (true);
         Ins.gameObject.SetActive (true);
         /*
         Visual_Novel_Char Vnc = Ins.GetComponent <Visual_Novel_Char> ();
         Vnc.On_Set_Sprite (Home_Scene.Ins._A_Character_Asset.On_Get_Character_Asset (Name).On_Get_C_Expression(Style).Sprite);
         Vnc.On_Set_Animation (Dict_Char_2d_Fade_In[L_C_Char_2d.Count]);
         */
         c.Char_2d_GO = Ins;
         L_C_Char_2d.Add (c);
         
      }
   }

    // Visual_Novel_Event
   public void On_Remove_Char_2d (string Name,string Type_Remove) {
      if (On_Check_Same_Char (Name)) {
         C_Char_2d Target_Remove = new C_Char_2d ();
         foreach (C_Char_2d s in L_C_Char_2d) {
            if (s.Name == Name) {
               Target_Remove = s;
               break;
            }
         }

         Visual_Novel_Char Vnc = Target_Remove.Char_2d_GO.GetComponent <Visual_Novel_Char> ();
         Vnc.On_Set_Animation (Type_Remove);
         Vnc.On_Destroy (1.0f);
         L_C_Char_2d.Remove (Target_Remove);
        // Destroy (Target_Remove.Char_2d_GO);
      }
   }

   // Visual_Novel_Event
   public void On_Set_Char_2d (string Name, string Style) {
       if (On_Check_Same_Char (Name)) {
         /*
         C_Char_2d Target_Set = new C_Char_2d ();
         foreach (C_Char_2d s in L_C_Char_2d) {
            if (s.Name == Name) {
               Target_Set = s;
               break;
            }
         }
         Target_Set.Char_2d_GO.GetComponent <Visual_Novel_Char> ().On_Set_Sprite (Home_Scene.Ins._A_Character_Asset.On_Get_Character_Asset (Name).On_Get_C_Expression(Style).Sprite);
         */
      }
   }

      #region On_Check_Same_Char
      bool On_Check_Same_Char (string Name) {
         bool Res = false;
         foreach (C_Char_2d s in L_C_Char_2d) {
            if (s.Name == Name) {
               Res = true;
               break;
            }
         }

         return Res;
      }
      #endregion
   #endregion
   
}
