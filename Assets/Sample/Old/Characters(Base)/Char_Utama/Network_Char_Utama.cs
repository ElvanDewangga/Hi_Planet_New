using System.Collections;
using UnityEngine;

public class Network_Char_Utama : MonoBehaviour
{
    [SerializeField] private Char_Utama _Char_Utama;

    private void Start()
    {
        On_Get_Spawn_Object();
    }

    #region Data_Game_Source

    private string Code_Character = ""; // Hi,V, dll

    public void On_Get_Data_From_Data_Game_Source(string Code_Character_V)
    {
        Code_Character = Code_Character_V;
        // Debug.Log (Code_Character);
        _Char_Spawn.On_Set_Code_Char_Pack("Player", Code_Character);
        _Char_Spawn.On_Get_Spawn_Object(gameObject);
    }

    #endregion

    #region NetworkManger (GameObject) - PlayerSpawner

    private Char_Spawn _Char_Spawn;

    private void On_Get_Spawn_Object()
    {
        StartCoroutine(N_On_Get_Spawn_Object());
    }

    private IEnumerator N_On_Get_Spawn_Object()
    {
        var Psp = _Char_Utama.gameObject.GetComponent<Player_Sync_Position>();
        if (_Char_Spawn == null) _Char_Spawn = Char_Data.Ins.On_Get_Player_Char_Spawn();
        yield return new WaitForSeconds(1);
        if (Psp.b_Owner)
        {
            Debug.Log("Player");
            _Char_Spawn.On_Set_Code_Char_Pack("Player", "");
        }
        else
        {
            Debug.Log("Client");
            _Char_Spawn.On_Set_Code_Char_Pack("Client", "Hi");
        }

        _Char_Spawn.On_Get_Spawn_Object(gameObject);
    }

    #endregion
}