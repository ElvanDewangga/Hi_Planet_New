using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiNetworkManager : MonoBehaviour {
   public static HiNetworkManager instance;
   public static NetworkWorld networkWorld;
   void Awake () {
    instance = this;
    networkWorld = GetComponentInChildren <NetworkWorld> ();
   }

   void Start () {
   // networkWorld.NetworkStart ();
   // Player akan langsung Spawn dari Fishnet. dan akan langsung dimulai dari ClientConnectionHandler
   }


}
