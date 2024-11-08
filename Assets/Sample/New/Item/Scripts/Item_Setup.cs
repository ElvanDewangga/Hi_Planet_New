using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item_Ability;

[CreateAssetMenu(fileName = "Item_Setup", menuName = "StarSky/Create Item Setup", order = 1)]
public class Item_Setup : ScriptableObject {
     #region Data
   public string Id;
   public int Quantity = 0;
   public Item_Input _Item_Input;
   #endregion
}
