using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class World_Baloon : MonoBehaviour {
    #region Baloon_1
    [SerializeField]
    TMP_Text Baloon_1_Title_Tx;
    [SerializeField]
    TMP_Text Baloon_1_Subtitle_Tx;
    #endregion
    #region Display
        void On_Display (string Title_V, string Subtitle_V) {
            Baloon_1_Title_Tx.text = Title_V;
            Baloon_1_Subtitle_Tx.text = Subtitle_V;
            this.gameObject.SetActive (true);
        }

        

        #region Item_Trigger
        public GameObject On_Create (GameObject From, string Title_V, string Subtitle_V) {
            GameObject Ins = GameObject.Instantiate (this.gameObject);
            Ins.transform.position = From.transform.position;
            Ins.GetComponent <World_Baloon> (). On_Display (Title_V, Subtitle_V);
            Ins.transform.localScale = new Vector3 (1,1,1);
            return Ins;
        }

        bool b_Once_Off = false;
        // Item_Trigger
        public void Off_Display () {
            if (!b_Once_Off) {b_Once_Off = true; StartCoroutine (N_Off_Display ());}
        }

        IEnumerator N_Off_Display () {
            yield return new WaitForSeconds (0);
            Destroy (this.gameObject);
        }
        #endregion
    #endregion
}
