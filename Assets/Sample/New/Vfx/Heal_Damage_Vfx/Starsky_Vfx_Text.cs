using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Starsky_Vfx_Text : MonoBehaviour
{
    [SerializeField]
    string Code_Text = "";
    [SerializeField]
    TMP_Text _Text;

    #region Char_Status
    public void On_Input_Text (string v) {
        this.gameObject.SetActive (true);
        if (Code_Text == "Damage") {
            _Text.text = v;
        } else if (Code_Text == "Heal") {
            _Text.text = "+ " + v;
        }
        StartCoroutine (N_Destroy ());
    }

    IEnumerator N_Destroy () {
        yield return new WaitForSeconds (2.0f);
        Destroy (this.gameObject);
    }
    #endregion
}
