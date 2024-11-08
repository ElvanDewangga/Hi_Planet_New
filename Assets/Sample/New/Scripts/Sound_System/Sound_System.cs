using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_System : MonoBehaviour {
    public static Sound_System Ins;
    void Awake () {
        Ins = this;
    }
   #region Music
   [SerializeField]
   AudioSource _AudioSource;
   [SerializeField]
   AudioClip [] A_Audioclip;

   Dictionary <string, AudioClip> Dic_Audio_Clip;
    void Example_On_Set_Music () {
        On_Set_Music ("Potato"); 
    }
    public void On_Set_Music (string Code_V) {
        _AudioSource.clip = Dic_Audio_Clip[Code_V];
        _AudioSource.Play ();
    }

    public void On_Add_Music () {
        Dic_Audio_Clip = new Dictionary<string, AudioClip> ();
        foreach (AudioClip a in A_Audioclip) {
            Dic_Audio_Clip.Add (a.name, a);
        }
    }

    void Start () {
        On_Add_Music ();
    }
   #endregion
}
