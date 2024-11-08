using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_AI_Event : MonoBehaviour {
   public bool b_Play = false;
   public virtual void Play () {
      b_Play = true;
   }

   public virtual void Stop () {
       b_Play = false;
   }
}
