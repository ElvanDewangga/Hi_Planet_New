using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class World_Place_Name : MonoBehaviour {
    [SerializeField]
    Animator _Animator;
    [SerializeField]
    TMP_Text _World_Place_Name_Tx;
    void Example_On_World_Place_Home () {
        // val = Nama tempat yang dikunjung atau tulisan yang ingin ditampilkan.
        On_World_Place_Home (new object [] {"Ecopolis"});
    }
    
    // Test_Script :
    public void On_World_Place_Home (object [] s) {
        _World_Place_Name_Tx.text = s[0].ToString ();
        _Animator.gameObject.SetActive (true);
        StartCoroutine (N_On_World_Place_Home ());
    }

    IEnumerator N_On_World_Place_Home () {
        _Animator.SetTrigger ("On");
        yield return new WaitForSeconds (3.5f);
        _Animator.SetTrigger ("Off");
        yield return new WaitForSeconds (1);
        _Animator.gameObject.SetActive (false);
    }
}
