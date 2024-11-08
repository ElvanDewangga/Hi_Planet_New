using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharBase : MonoBehaviour {
   /* Note
   Ini hanya script sample, untuk menghindari Conflict, kalian bisa menggunakan cara kalian sendiri atau menyesuaikan script yang sudah di refactor.
   */
   #region CharAttack
   
    [SerializeField] Char_Data_Variant_Attack _Char_Data_Variant_Attack;
    public Object_Variant_Config skillConfig;
    // EnemyAttack
    public void AIPlaySkill () {
      GameObject Cloning = GameObject.Instantiate (_Char_Data_Variant_Attack.gameObject);
      //Cloning.transform.SetParent (Go_Peluru.transform);
      Cloning.transform.position = this.transform.position;
      Char_Data_Variant_Attack Cloning_Variant = Cloning.GetComponent <Char_Data_Variant_Attack> ();
      Cloning_Variant.On_Set_Clone ();
      Cloning_Variant.On_Set_Char_Data_Variant_Attack (null, skillConfig, Cloning, "Enemy"); 
      Cloning.gameObject.SetActive (true); 
      Cloning.name = "EnemyAttack (Test Only)";   
    }

    #endregion
}
