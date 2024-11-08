using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Bcpg;
using UnityEngine;

public class Search_Active_Object : MonoBehaviour
{
    Spawn_Clone_Object _Spawn_Clone_Object;
    public void On_Set_Spawn_Clone_Object (Spawn_Clone_Object S) {
        _Spawn_Clone_Object = S;
     //   Debug.LogError ("Set Object");
    }
    // Char_Spawn - Char_Data - Char_Data_Source :
    public GameObject On_Get_A_Object () {  
        if (_Spawn_Clone_Object) {
            return _Spawn_Clone_Object.gameObject;
        }

        Debug.LogError ("Tidak ada objek yang aktif");
        return null;
    } 

    
}
