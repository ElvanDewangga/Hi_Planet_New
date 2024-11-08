using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DM_ColliderSample))]
public class Char_Attack_Collider_Sample : MonoBehaviour {
   
    public bool useparent = false;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has a specific tag (optional)
        if (this.gameObject.tag == "Player") {
            if (other.CompareTag("Enemy")) {
                playerCharBase.OnAttackHit (other.gameObject, playerCharBase, Skill_Power);
            }
        } else if (this.gameObject.tag == "Enemy") {
            if (other.CompareTag("Player")) {
               other.GetComponent <PlayerCharBase> ().ApplyDamage (-15, this.gameObject,configVariant );
            }
        }
    }

    #region Char_Data_Variant_Attack
    int Skill_Power;
    PlayerCharBase playerCharBase;
    Object_Variant_Config configVariant;
    public void On_Input_Data (PlayerCharBase CharBase, int Skill_Power_V, Object_Variant_Config configVariantV) {
        playerCharBase = CharBase;
        Skill_Power = Skill_Power_V;
        configVariant = configVariantV;

    }
    #endregion
}
