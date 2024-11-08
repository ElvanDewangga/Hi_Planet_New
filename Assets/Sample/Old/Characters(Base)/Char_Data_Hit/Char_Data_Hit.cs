using System.Collections.Generic;
using UnityEngine;

public class Char_Data_Hit : MonoBehaviour
{
    [SerializeField] private Logic_Light_Setup _Logic_Light_Setup;

    private void Start()
    {
        On_Load_Dicitionary();
    }

    private void On_Set_b_Light_Setup(Config_Char_Hit Object_Config, GameObject GO_To, GameObject Vfx_Go)
    {
        if (!Vfx_Go) Debug.LogError("Vfx_Go");
        if (!GO_To) Debug.LogError("GO_To");
        _Logic_Light_Setup.ApplySetup(GO_To.transform, Vfx_Go.transform, Object_Config._Config_Light_Setup);
    }

    #region Char_Status - Char_Source

    private Char_Technique Cur_Char_Technique;

    public void On_Set_Char_Data_Hit(GameObject GO_From, GameObject GO_To)
    {
//      Debug.Log (GO_From);
        Cur_Char_Technique = GO_From.GetComponent<Char_Technique>();
        var A_Name_Vfx = Cur_Char_Technique.On_Get_A_Char_Data_Hit_Name();

        if (A_Name_Vfx.Length > 0)
        {
            var x = Random.Range(0, A_Name_Vfx.Length);

            var Cd = Dict_Char_Data_Hit[A_Name_Vfx[x]];
            var Ins = Instantiate(Cd.gameObject);
            var Cd_Ins = Ins.GetComponent<Char_Data_Hit_Settings>();
            Config_Char_Hit Object_Config = null;
            if (Cd_Ins._Config_Char_Hit)
            {
                Object_Config = Cd_Ins._Config_Char_Hit;
                Cd_Ins.transform.localScale = Object_Config.V3_Scale;
                Ins.transform.position = GO_To.gameObject.transform.position + Object_Config.V3_Position;
                Ins.GetComponent<SelfDestruct>().On_Set_Time(Object_Config.Destroy_Duration);
            }
            else
            {
                Debug.LogError("Tidak ada Config " + A_Name_Vfx[0]);
                Ins.transform.position = GO_To.gameObject.transform.position;
            }


            foreach (Transform Ts in Cd_Ins.transform)
                if (Object_Config)
                    Ts.transform.localScale = Object_Config.V3_Scale_Child;
                else
                    Ts.transform.localScale = Cd.V3_Scale;

            if (Object_Config) On_Set_Object_Variant_Config_Handler(Object_Config, Cur_Char_Technique, GO_To, Ins);
            Ins.SetActive(true);
        }
    }

    private void On_Set_Object_Variant_Config_Handler(Config_Char_Hit Object_Config, Char_Technique _Char_Technique,
        GameObject GO_To, GameObject Vfx_Go)
    {
        if (Object_Config.b_Light_Setup) On_Set_b_Light_Setup(Object_Config, GO_To, Vfx_Go);
    }

    #endregion

    #region Dictionary

    [SerializeField] private Transform A_Hit;

    private Dictionary<string, Char_Data_Hit_Settings> Dict_Char_Data_Hit = new();

    private void On_Load_Dicitionary()
    {
        Dict_Char_Data_Hit = new Dictionary<string, Char_Data_Hit_Settings>();
        foreach (Transform Ts in A_Hit) Ts.GetComponent<Char_Data_Hit_Settings>().On_Add_Asset();
    }

    public void On_Add_Dict_Char_Data_Hit(string s, Char_Data_Hit_Settings cd)
    {
        // Debug.Log (s);
        Dict_Char_Data_Hit.Add(s, cd);
    }

    #endregion
}