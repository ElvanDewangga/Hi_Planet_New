using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Attack_Collider_Directly : MonoBehaviour {
    [SerializeField]
    Char_Utama _Char_Utama;
    [SerializeField]
    GameObject Go_From;
   private void OnTriggerEnter(Collider other)
    {
        if (!_Char_Utama) {
            if (other.CompareTag("Player")) {
                int damage = (Skill_Power) * -1;
                //  Cs.On_Cur_Hp(damage, attackGo[currentCombo- 1].gameObject);
                other.GetComponent <Char_Utama> ()._Char_Status.On_Cur_Hp(damage, Go_From);
            }
        }else {
            // Check if the other object has a specific tag (optional)
        if (_Char_Utama.gameObject.tag == "Player") {
            if (other.CompareTag("Enemy")) {
                _Char_Utama._Char_Attack.OnAttackHit (other.gameObject, Go_From, Skill_Power);
            }
        } else if (_Char_Utama.gameObject.tag == "Enemy") {
            if (other.CompareTag("Player")) {
                _Char_Utama._Char_Attack.OnAttackHit (other.gameObject, Go_From, Skill_Power);
            }
        }
        }
        
    }

    [SerializeField]
    int Skill_Power;
   
}
