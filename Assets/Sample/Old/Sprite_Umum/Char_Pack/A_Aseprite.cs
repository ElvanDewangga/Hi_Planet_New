using UnityEngine;

public class A_Aseprite : MonoBehaviour
{
    #region Char_Accesories

    public Transform[] A_Accesories;

    #endregion

    // Char_Animation :
    /* tidak jadi pakai
    public void On_A_Aseprite (string Name_Animation_V) {
         foreach (Transform Trs in this.transform) {
             if (Trs.name == Name_Animation_V) {
                 Trs.gameObject.SetActive (true);
             } else {
                 Trs.gameObject.SetActive (false);
             }
         }
    }
     */

    public GameObject On_Get_Aseprite(string Name_Animation_V)
    {
        GameObject Res = null;
        foreach (Transform Trs in transform)
            if (Trs.name == Name_Animation_V)
                Res = Trs.gameObject;
            else
                Res = Trs.gameObject;
        return Res;
    }

    #region Char_Animation

    public Animator[] Child_Animator;

    // public Transform [] Part_Object;
    public Part_Aseprite[] A_Part_Aseprite;
    // A_Asesprite, Char_Utama_Source, Char_Data_Variant_Attack, & Char_Animation
    public Object_Rotation_Target_With_Direction[] A_Object_Target_Rotation_With_Direction;

    #endregion

    #region Char_Data_Variant_Attack

    #region Shader

    [HideInInspector] public string Equip_Skin = "Normal";

    [SerializeField] private Char_Skin[] A_Char_Skin;

    [HideInInspector] public Char_Skin Cur_Char_Skin;

    // Char_Utama & Char_Data_Variant_Attack, this
    public void On_Set_Mode(string C_Code_Skin)
    {
        if (A_Char_Skin.Length > 0)
        {
            foreach (var c in A_Char_Skin)
                if (c.Char_Skin_Code == C_Code_Skin)
                {
                    Cur_Char_Skin = c;
                    break;
                }

            foreach (var c in A_Char_Skin)
                if (c == Cur_Char_Skin)
                    c.On_Char_Skin();
                else
                    c.Off_Char_Skin();


            var Tw = 0;
            var A_O = new Object_Rotation_Target_With_Direction[A_Object_Target_Rotation_With_Direction.Length];
            foreach (var T in Cur_Char_Skin.A_Part)
                if (T.GetComponent<Object_Rotation_Target_With_Direction>())
                {
                    A_O[Tw] = T.GetComponent<Object_Rotation_Target_With_Direction>();
                    A_O[Tw].On_Set_Target(A_Object_Target_Rotation_With_Direction[Tw].On_Get_Target());
                    Tw++;
                }

            A_Object_Target_Rotation_With_Direction = A_O;
        }
    }

    //-- Char_Data_Variant:
    public void On_Back_To_Normal()
    {
        On_Set_Mode(Equip_Skin);
    }

    #endregion

    #endregion
}