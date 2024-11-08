using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_AI_Idle : Char_AI_Event {
    [SerializeField]
   Char_AI _Char_AI;
   public override void Play () {
    base.Play ();
    On_Play ();
   }

   public override void Stop () {
    base.Stop ();
   }

   void On_Play () {
        _Char_AI._Char_Utama._Char_Animation.On_Play_Animation ("Idle");
   }
}
