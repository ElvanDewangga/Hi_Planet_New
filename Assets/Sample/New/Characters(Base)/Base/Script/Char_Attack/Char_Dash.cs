using System.Collections;
using UnityEngine;

public class Char_Dash : MonoBehaviour
{
    Vector3 V3_Target_Move;
    bool b_Dash = false;
   void Update () {
        if (b_Dash) {
            
           // transform.parent.LeanMove(_Char_Utama.transform.position + (-transform.right * 0.5f), 0.1f).setEase(LeanTweenType.easeOutSine);
            transform.parent.LeanMove(this.transform.parent.position + (-transform.right * 0.1f), 0.05f).setEase(LeanTweenType.easeOutSine);
            StartCoroutine(makeamove());
        }
   }

   IEnumerator makeamove()
    {
        yield return new WaitForSeconds(0.02f);
        transform.parent.LeanMove(new Vector3(V3_Target_Move.x, V3_Target_Move.y, this.transform.parent.transform.position.z), 0.15f);
       // Invoke("EnableNav", 0.5f);
       b_Dash = false;
        yield return null;
    }

    #region Char_Data_Variant_Attack
    public void On_Start_Dash (Vector3 Dash_Position) {
        V3_Target_Move = Dash_Position;
        b_Dash = true;
    }
    #endregion
}
