using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Data : MonoBehaviour {
   public string Item_Name = "";
   [HideInInspector]
    public Data_Item_Input _Data_Item_Input;
    [SerializeField]
    SpriteRenderer _Sprite_Renderer;
   void Start () {
        _Data_Item_Input = ItemInputManager.instance.On_Get_Data_Item_Input_From_Name (Item_Name);
        _Sprite_Renderer.sprite = _Data_Item_Input._Item_Input.Item_Sprite;
        Char_Data.Ins.Spawn_System_Parenting (this.gameObject);    
   }
}
