using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager instance;
    void Awake () {
        instance = this;
    }
    public PlayerCharBase player;
    // PlayerCharBase
    public void AddPlayer (PlayerCharBase playerClone) {
       player = playerClone;
       Control.instance.dualJoystickPlayerController.InitializePlayer (player.gameObject);
    }
}
