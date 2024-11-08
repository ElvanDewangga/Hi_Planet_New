using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array_Time_Event : MonoBehaviour {
    #region Option
    [SerializeField]
    [Tooltip("waktu static delay : jika tidak pakai maka cukup tulis 0")]
    float Delay_Static_Time;
    #endregion

    GameObject [] A_GameObject;
    float [] A_Delay;
    void Awake () {
        On_Start ();
    }

    void OnEnable () {
        On_Doing ();
    }

    void OnDisable () {
        foreach (GameObject s in A_GameObject) {
            s.gameObject.SetActive (false);
        }
    }

    void On_Start () {
        A_GameObject = new GameObject[this.gameObject.transform.childCount];
        A_Delay = new float [this.gameObject.transform.childCount];
        int x = -1;
        foreach (Transform obj in this.gameObject.transform) {
            x++;
            A_GameObject[x] = obj.gameObject;
            if (Delay_Static_Time > 0) {
                A_Delay[x] = Delay_Static_Time;
            } else {
                A_Delay[x] = 0;
            }
        }
    }

    int Cur_Target = 0;
    void On_Doing () {
        Cur_Target = 0;
        StartCoroutine (N_On_Doing ());
    }

    IEnumerator N_On_Doing () {
        yield return new WaitForSeconds (A_Delay[Cur_Target]);
        // Fungsi_Umum Fs = new Fungsi_Umum ();
       //  Fs.On_Set_Active (A_GameObject[Cur_Target], true);
        Cur_Target ++;
        if (Cur_Target < A_GameObject.Length) {
            StartCoroutine (N_On_Doing ());
        } 
    }
    
    #region Animator
    // Visual_Novel_System :
    public void On_Set_All_Animator (string val) {
        foreach (GameObject s in A_GameObject) {
            s.GetComponent<Animator> ().SetTrigger (val);
        }
    }
    #endregion
}
