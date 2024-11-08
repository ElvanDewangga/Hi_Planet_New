using TMPro;
using UnityEngine;

public class PlayerBonusPoint : MonoBehaviour
{
    [SerializeField] private Canvas Canvas_Char_Data_Bonus_Point;

    // Button (test) :
    public void On_Display()
    {
        On_Refresh_Display();
        Canvas_Char_Data_Bonus_Point.gameObject.SetActive(true);
    }

    public void Off_Display()
    {
        Canvas_Char_Data_Bonus_Point.gameObject.SetActive(false);
    }

    private void On_Refresh_Display()
    {
        A_Bonus_Point_Text.text = "Bonus Point : " + AccountManager.instance.player.bonusPoint;
        A_Tmp_Text_Value[0].text = AccountManager.instance.player.life.ToString();
        A_Tmp_Text_Value[1].text = AccountManager.instance.player.attack.ToString();
        A_Tmp_Text_Value[2].text = AccountManager.instance.player.defense.ToString();
        A_Tmp_Text_Value[3].text = AccountManager.instance.player.speed.ToString();
        A_Tmp_Text_Value[4].text = AccountManager.instance.player.intelligence.ToString();
    }

    [SerializeField] private TMP_Text A_Bonus_Point_Text;
    [SerializeField] private TMP_Text[] A_Tmp_Text_Value;

    // Button :
    public void On_Use_Bonus_Point(string s)
    {
        if (AccountManager.instance.player.bonusPoint > 0)
        {
            var value = AccountManager.instance.player.On_Char_Data_Bonus_Point(s);
            AccountManager.instance.player.RemoveBonusPoints(1);
            On_Refresh_Display();
            On_Save(s, value);
        }
    }

    #region Save

    private void On_Save(string s, int value)
    {
        var Host_Server_Value = new string [7]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            var Host_Server_Field = new string [7];
            Host_Server_Field[0] = "Id";
            Host_Server_Value[0] = HiGameManager.instance._dataGameManager.id;
            Host_Server_Field[1] = "table_1";
            Host_Server_Value[1] = "Db_Equipment";
            Host_Server_Field[2] = "title_1";
            Host_Server_Value[2] = s;
            Host_Server_Field[3] = "value_1";
            Host_Server_Value[3] = value.ToString();

            Host_Server_Field[4] = "table_2";
            Host_Server_Value[4] = "Db_Equipment";
            Host_Server_Field[5] = "title_2";
            Host_Server_Value[5] = "Bonus_Point";
            Host_Server_Field[6] = "value_2";
            Host_Server_Value[6] = AccountManager.instance.player.bonusPoint.ToString();

            HiGameManager.instance._dataGameManager._DataGameEquipment.StartSave(Host_Server_Field, Host_Server_Value);
    }

    #endregion
}