using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

public class Regex_Utama : MonoBehaviour
{
   public static Regex_Utama Ins;
   public Regex_Format_1_Event _Regex_Format_1_Event;
   void Awake () {
    Ins = this;
   } 

   #region Regex_Format_1
    /* Example :
    [SerializeField]
    Regex_Format_1 Rgx_1 = new Regex_Format_1 ();
    void Start () {
        Rgx_1.On_Get_List_From_Format_1 (":Add_Char(Child,Normal):Add_Char(Sistine,Smile)");
        Rgx_1.On_Doing_Event ();
    }
    */

    [System.Serializable]
    public class Regex_Format_1 {
        public List<string> L_1 = new List<string>();
        public List<string> L_2 = new List<string>();
        public List<string> L_3 = new List<string>();

        public void On_Get_List_From_Format_1 (string value) {
            /*
            // Example Format : :Add_Char(Child,Normal):Add_Char(Sistine,Smile)
            string Kalimat = All_Scene_Go_Script.Ins._Pengubah_String._Replace_String.On_Remove_Space (value);
          // Kalimat = ":Add_Char(Child,Normal):Add_Char(Sistine,Smile)";

            // Pattern regex untuk menangkap bagian yang diinginkan
            string pattern = @":(\w+)\((\w+),(\w+)\)";
            Regex regex = new Regex(pattern);

            // List untuk menyimpan hasil
            

            // Mencari kecocokan menggunakan regex
            MatchCollection matches = regex.Matches(Kalimat);

            foreach (Match match in matches)
            {
                L_1.Add(match.Groups[1].Value); // Menambahkan ke List L_1
                L_2.Add(match.Groups[2].Value); // Menambahkan ke List L_2
                L_3.Add(match.Groups[3].Value); // Menambahkan ke List L_3
            }

            // Menampilkan hasil
            */
        }

        // Visual_Novel_System :
        public void On_Doing_Event () {
            Regex_Format_1_Event Rx = Regex_Utama.Ins._Regex_Format_1_Event;
            Rx.On_Doing_Event (L_1,L_2,L_3);
        }
    }
    
   
    #endregion   
}
