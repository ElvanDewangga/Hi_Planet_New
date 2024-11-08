using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill_Keterangan { // Skill_Offline (Data from CSV).
   public string Skill_Name;
   public string Description;
   public string Skill_Info;
   public string Skill_Category;
   public int Gold_Learn;
   public int Phase;

   public Skill_Keterangan (Skill_Keterangan d) {
        Skill_Name = d.Skill_Name;
        Description = d.Description;
        Skill_Info = d.Skill_Info;
        Skill_Category = d.Skill_Category;
        Gold_Learn = d.Gold_Learn;
        Phase = d.Phase;
   }

   #region Database_Online
   // Hall_Of_Masters :
   public int Got_Skill = 0; // 0 = Tidak ada / 1 = ada.
   #endregion`
}
