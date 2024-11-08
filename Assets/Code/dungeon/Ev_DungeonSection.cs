using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ev_DungeonSection : MonoBehaviour {
    GameObject Transition;
   public void On_Back_First_Spawn () {
    Transition = UIManager.instance._transitionPanel;
    Transition.gameObject.SetActive (false);
    Network_Go.Ins.On_Finish_Dungeon ();
   }
}
