using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class Visual_Novel_Sprite : MonoBehaviour {
    [SerializeField]
   Sprite [] Pembuka_Police_Office;
    [SerializeField]
   Sprite [] Pembuka_Beach;
   [SerializeField]
   Sprite Unknown_Sprite;
   [SerializeField]
   Sprite [] Bg_Black;
    #region Get_Sprite_From_Name
   // Home_Scene, Sprite_Load :
    public Sprite  On_Get_Sprite_From_Name (string Name, int Code) {
       // Debug.Log("Requested field name: " + Name);
        Sprite Res = Unknown_Sprite;

        // Get the type information of the current class
        Type type = this.GetType();
       // Debug.Log("Type: " + type);

        // Get the FieldInfo object for the field with the specified name
        FieldInfo fieldInfo = type.GetField(Name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

        // Check if the field was found
        if (fieldInfo != null)
        {
         //   Debug.Log("Field found: " + fieldInfo.Name);

            // Get the value of the field
            var fieldValue = fieldInfo.GetValue(this);
         //   Debug.Log("Field value: " + fieldValue);

            // Check if the field value is an array or list of Sprites
            if (fieldValue is Sprite[] spriteArray)
            {
             //   Debug.Log("Field is a Sprite array of length: " + spriteArray.Length);

                // Ensure Code is within the bounds of the array
                if (Code >= 0 && Code < spriteArray.Length)
                {
                    Sprite Target = spriteArray[Code];
                    Res = Target;
                }
                else
                {
                    Debug.LogError("Code is out of bounds of the sprite array.");
                }
            }
            else
            {
                Debug.LogError("The field value is not a Sprite array.");
            }
        }
        else
        {
            Debug.LogError("Field not found: " + Name);
        }

        return Res;
    }
   #endregion
}
