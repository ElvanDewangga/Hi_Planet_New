using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class Sprite_Umum : MonoBehaviour {
   public Sprite Get_Sprite_By_Name(string Name_V) {
        FieldInfo field = this.GetType().GetField(Name_V, BindingFlags.Public | BindingFlags.Instance);

        if (field != null && field.FieldType == typeof(Sprite)) {
            return (Sprite)field.GetValue(this);
        }

        return null;
    }
}
