using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item_Ability;

public class DataItemInput : MonoBehaviour  {
   #region Data
   public string Id;
   public int Quantity = 0;
   #endregion
   #region Input
   public Item_Input _Item_Input;
   #endregion
   #region Data_Game_Source - Inventory
   public void SetData (string Id_V, int Quantity_V) {
      Id = Id_V;
      Quantity = Quantity_V;
      _Item_Input = ItemInputManager.instance.On_Get_Item_Input (Id_V);
   }
   #endregion
   #region Slot_Panel
   [SerializeField]
   public int Slot_Panel = 0;
   public void SetSlotPanel (int s) {
      Slot_Panel = s;
   }
   #endregion
   #region Data_Game_Source - Inventory (Equipment)
   public List <Item_Effect> L_Data_Item_Effect = new List<Item_Effect> ();
   public void SetEquipmentStatus (string Id_V, string Kalimat_Status_V) {
      Id = Id_V;
      _Item_Input = ItemInputManager.instance.On_Get_Item_Input (Id_V);

      L_Data_Item_Effect = new List<Item_Effect> ();
      Convert_Item_Effect Cs = new Convert_Item_Effect ();
      L_Data_Item_Effect = Cs.String_To_Item_Effect (Kalimat_Status_V);
      // Debug.Log (Kalimat_Status_V);
   }
   // Char_Equipment :
   public void On_Transfer_Data_Item_Input (DataItemInput From, DataItemInput To) {
      To.Id = From.Id;
        To.Quantity = From.Quantity;
        
        // Deep copy untuk _Item_Input
        To._Item_Input = new Item_Input {
            // Salin setiap properti dari From._Item_Input
            Name = From._Item_Input.Name,
         Type = From._Item_Input.Type,
         Item_Sprite = From._Item_Input.Item_Sprite,
         Item_Detail = From._Item_Input.Item_Detail,
         Max_Quantity = From._Item_Input.Max_Quantity,
         L_Item_Effect = From._Item_Input.L_Item_Effect,
            // Lakukan hal yang sama untuk properti lainnya...
        };
        
        // Deep copy untuk L_Data_Item_Effect
        To.L_Data_Item_Effect = new List<Item_Effect>();
        foreach (var effect in From.L_Data_Item_Effect) {
            var clonedEffect = new Item_Effect {
                Code_Effect = (string[])effect.Code_Effect.Clone(),
                Code_Value = (int[])effect.Code_Value.Clone()
            };
            L_Data_Item_Effect.Add(clonedEffect);
        }
      /*
      // Deep copy untuk _Item_Input
      To._Item_Input = new Item_Input {
         // Salin setiap properti dari From._Item_Input ke To._Item_Input
         // Contoh:
         Name = From._Item_Input.Name,
         Type = From._Item_Input.Type,
         Item_Sprite = From._Item_Input.Item_Sprite,
         Item_Detail = From._Item_Input.Item_Detail,
         Max_Quantity = From._Item_Input.Max_Quantity,
         L_Item_Effect = From._Item_Input.L_Item_Effect,
         // Lakukan hal yang sama untuk properti lainnya...
      };
      
      // Deep copy untuk L_Data_Item_Effect
      To.L_Data_Item_Effect = new List<Item_Effect>();
      foreach (var effect in From.L_Data_Item_Effect) {
         var clonedEffect = new Item_Effect {
               Code_Effect = (string[])effect.Code_Effect.Clone(),
               Code_Value = (int[])effect.Code_Value.Clone()
         };
         To.L_Data_Item_Effect.Add(clonedEffect);
      }
      */
   }
   #endregion
   // Char_Utama_Source :
   public DataItemInput (DataItemInput original) {
        Id = original.Id;
        Quantity = original.Quantity;
        
        // Deep copy untuk _Item_Input
        _Item_Input = new Item_Input {
            // Salin setiap properti dari original._Item_Input
            Name = original._Item_Input.Name,
         Type = original._Item_Input.Type,
         Item_Sprite = original._Item_Input.Item_Sprite,
         Item_Detail = original._Item_Input.Item_Detail,
         Max_Quantity = original._Item_Input.Max_Quantity,
         L_Item_Effect = original._Item_Input.L_Item_Effect,
            // Lakukan hal yang sama untuk properti lainnya...
        };
        
        // Deep copy untuk L_Data_Item_Effect
        L_Data_Item_Effect = new List<Item_Effect>();
        foreach (var effect in original.L_Data_Item_Effect) {
            var clonedEffect = new Item_Effect {
                Code_Effect = (string[])effect.Code_Effect.Clone(),
                Code_Value = (int[])effect.Code_Value.Clone()
            };
            L_Data_Item_Effect.Add(clonedEffect);
        }
    }

    #region Item_Setup
      [SerializeField]
      Item_Setup _Item_Setup;

      public void On_Set_Item_Setup (Item_Setup Is) {
         _Item_Setup = Is;
         Id = _Item_Setup.Id;
         Quantity = _Item_Setup.Quantity;
         _Item_Input = _Item_Setup._Item_Input;
      }
    #endregion
}
