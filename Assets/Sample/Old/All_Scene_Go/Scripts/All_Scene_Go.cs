using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Scene_Go : MonoBehaviour {
    void Awake () {
        Ins = this;
    }

    public static All_Scene_Go Ins;
    public Notification_Script _Notification_Script;
    public Spawn_System _Spawn_System;
    public DualJoystickPlayerController _DualJoystickPlayerController;
    public CameraMove _Camera_Move;
    public Hud_Canvas _Hud_Canvas;
    public Inventory _Inventory;
    public Tabel_Popup _Tabel_Popup;
    public Storage _Storage;
    public Sprite_Umum _Sprite_Umum;
}
