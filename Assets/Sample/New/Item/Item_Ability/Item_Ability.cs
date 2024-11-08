using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Item_Ability
{
    [System.Serializable]
    public class Item_Input {
        public string Name = "";
        public string Type = ""; // "Material", "Helmet", "Drone"
        public Sprite Item_Sprite;
        public string Item_Detail = "";
        public int Max_Quantity = 1;
        public List <Item_Effect> L_Item_Effect = new List <Item_Effect> ();
        public Item_Display _Item_Display;
    }
    [System.Serializable]
    public class Item_Display {
        public Vector3 V3_Local_Position;
        public Vector3 V3_localRotation;
        public Vector3 V3_Local_Scale;
    }

    public class Item_Display_Logic {
        // Char_Accesories :
        public void On_Set_Item_Display (GameObject Go, Item_Input _Item_Input, Vector3 Offset) {
            Item_Display _Item_Display = _Item_Input._Item_Display;
            Go.GetComponent<SpriteRenderer> ().sprite = _Item_Input.Item_Sprite;
            Go.transform.localPosition = _Item_Display.V3_Local_Position + Offset;
            Go.transform.localRotation = Quaternion.Euler (_Item_Display.V3_localRotation);
            Go.transform.localScale = _Item_Display.V3_Local_Scale;
        }
    }

    [System.Serializable]
    public class Item_Effect {
        public string [] Code_Effect = new string [0];
        public int [] Code_Value = new int [0];
    }

    // Data_Item_Input :
    public class Convert_Item_Effect {
        public string Convert_Item_Effect_Code (string s) {
            string Res = "";
            if (s == "L") {Res = "Life";}
            else if (s == "A") {Res = "Attack";}
            else if (s == "D") {Res = "Defense";}
            else if (s == "S") {Res = "Speed";}
            else if (s == "I") {Res = "Intelligence";}
            return Res;
        }

        public List <Item_Effect> String_To_Item_Effect (string s) {
            // (A,5),(B,6) => Code_Effect[0] = A Code_Value[0] = 5, Code_Effect[1] = B Code_Value[1] = 6
                List<Item_Effect> Res = new List<Item_Effect>();

                // Menghapus tanda kurung dan spasi
                s = s.Replace("(", "").Replace(")", "").Trim();

                // Split string berdasarkan koma yang memisahkan elemen
                string[] pairs = s.Split(' ');

                foreach (string pair in pairs) {
                    // Memisahkan effect dan value berdasarkan koma di dalam setiap pasangan
                    string[] effectAndValue = pair.Split(',');

                    if (effectAndValue.Length == 2) {
                        // Membuat instance baru Item_Effect
                        Item_Effect itemEffect = new Item_Effect {
                            Code_Effect = new string[] { Convert_Item_Effect_Code (effectAndValue[0]) },
                            Code_Value = new int[] { int.Parse(effectAndValue[1]) }
                        };

                        Res.Add(itemEffect);
                    }
                }

                return Res;
            }

            public string List_Item_Effect_To_String (List <Item_Effect> L_V) {
                StringBuilder Res = new StringBuilder();

                foreach (var item in L_V) {
                    // Membentuk string dalam format "(effect,value)"
                    for (int i = 0; i < item.Code_Effect.Length; i++) {
                        Res.Append($"({item.Code_Effect[i]},{item.Code_Value[i]}) ");
                    }
                }

                // Menghapus spasi terakhir yang berlebih
                return Res.ToString().Trim();
            }

            public string [] Combine_A_Int_Own_And_A_String_Status (int [] A_Int_Own, string [] A_Str_Status) {
                string [] Res = new string [A_Str_Status.Length];
                int x = 0;
                for (x =0; x < A_Str_Status.Length; x++) {
                    if (A_Str_Status[x].Contains ("(")) {
                        Res[x] = A_Str_Status[x];
                    } else {
                        Res[x] = A_Int_Own[x].ToString ();
                    }
                }
                return Res;
            }

            // Char_Utama_Source, Char_Accesories :
            public void On_Transfer_Data_Item_Input (Data_Item_Input From, Data_Item_Input To) {
                Debug.Log (From.Id);
                Debug.Log (To.Id);
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
                    _Item_Display = From._Item_Input._Item_Display
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

            }
            // Char_Utama_Source :
            public void On_Refresh_Data_Item_Input (Data_Item_Input To) {
                    To.Id = "";
                    To.Quantity = 0;
                    
                    // Deep copy untuk _Item_Input
                    To._Item_Input = new Item_Input {
                        // Salin setiap properti dari From._Item_Input
                        Name = "",
                    Type = "",
                    Item_Sprite = null,
                    Item_Detail = "",
                    Max_Quantity = 0,
                    L_Item_Effect = new List<Item_Effect> (),
                    _Item_Display = new Item_Display ()
                        // Lakukan hal yang sama untuk properti lainnya...
                    };
                    
                    // Deep copy untuk L_Data_Item_Effect
                    To.L_Data_Item_Effect = new List<Item_Effect>();
            }
    }

    public class ConvertItemEffect
    {
        public string ConvertItemEffectCode(string code)
        {
            return code switch
            {
                "L" => "Life",
                "A" => "Attack",
                "D" => "Defense",
                "S" => "Speed",
                "I" => "Intelligence",
                _ => string.Empty
            };
        }

        public List<Item_Effect> StringToItemEffect(string input)
        {
            var result = new List<Item_Effect>();
            input = input.Replace("(", "").Replace(")", "").Trim();
            var pairs = input.Split(' ');

            foreach (var pair in pairs)
            {
                var effectAndValue = pair.Split(',');

                if (effectAndValue.Length == 2 && int.TryParse(effectAndValue[1], out int value))
                {
                    var itemEffect = new Item_Effect
                    {
                        Code_Effect = new[] { ConvertItemEffectCode(effectAndValue[0]) },
                        Code_Value = new[] { value }
                    };

                    result.Add(itemEffect);
                }
            }

            return result;
        }

        public string ItemEffectListToString(List<Item_Effect> itemEffects)
        {
            var result = new StringBuilder();

            foreach (var item in itemEffects)
            {
                for (int i = 0; i < item.Code_Effect.Length; i++)
                {
                    result.AppendFormat("({0},{1}) ", item.Code_Effect[i], item.Code_Value[i]);
                }
            }

            return result.ToString().Trim();
        }

        public string[] CombineIntArrayWithStatusString(int[] intArray, string[] statusArray)
        {
            string[] result = new string[statusArray.Length];

            for (int i = 0; i < statusArray.Length; i++)
            {
                result[i] = statusArray[i].Contains("(") ? statusArray[i] : intArray[i].ToString();
            }

            return result;
        }

        public void TransferDataItemInput(DataItemInput source, DataItemInput target)
        {
           // Debug.Log(source.Id);
          //  Debug.Log(target.Id);

            target.Id = source.Id;
            target.Quantity = source.Quantity;
            target._Item_Input = CloneItemInput(source._Item_Input);
            target.L_Data_Item_Effect = CloneItemEffectList(source.L_Data_Item_Effect);
        }

        public void RefreshDataItemInput(DataItemInput target)
        {
            target.Id = string.Empty;
            target.Quantity = 0;
            target._Item_Input = new Item_Input
            {
                Name = string.Empty,
                Type = string.Empty,
                Item_Sprite = null,
                Item_Detail = string.Empty,
                Max_Quantity = 0,
                L_Item_Effect = new List<Item_Effect>(),
                _Item_Display = new Item_Display()
            };
            target.L_Data_Item_Effect = new List<Item_Effect>();
        }

        private Item_Input CloneItemInput(Item_Input itemInput)
        {
            return new Item_Input
            {
                Name = itemInput.Name,
                Type = itemInput.Type,
                Item_Sprite = itemInput.Item_Sprite,
                Item_Detail = itemInput.Item_Detail,
                Max_Quantity = itemInput.Max_Quantity,
                L_Item_Effect = new List<Item_Effect>(itemInput.L_Item_Effect),
                _Item_Display = itemInput._Item_Display
            };
        }

        private List<Item_Effect> CloneItemEffectList(List<Item_Effect> effects)
        {
            var clonedList = new List<Item_Effect>();

            foreach (var effect in effects)
            {
                clonedList.Add(new Item_Effect
                {
                    Code_Effect = (string[])effect.Code_Effect.Clone(),
                    Code_Value = (int[])effect.Code_Value.Clone()
                });
            }

            return clonedList;
        }
    }
}
