using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test_Scene : MonoBehaviour
{

    [SerializeField]
    GameObject Enemy_Test_World;
   public void On_Masuk_Scene () {
    
    _Enemy_Test_Canvas.On_Display ();
    Char_Data.Ins.Your_Char_Utama.transform.SetParent (Enemy_Test_World.transform);
    Enemy_Test_World.gameObject.SetActive (true);
    Sample_Scene.Ins.On_Keluar_Scene ();
   }

   public Char_Spawn _Char_Spawn;
    [SerializeField]
   public  Enemy_Test_Canvas _Enemy_Test_Canvas;
   public static Enemy_Test_Scene Ins;
   void Awake () {
    Ins = this;
   }
   
}
