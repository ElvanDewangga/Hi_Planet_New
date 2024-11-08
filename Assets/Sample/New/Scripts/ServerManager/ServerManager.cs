using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ServerManager : MonoBehaviour {
    public string phpLink = "http://liwebgames.com/Php_Hi_Planet";
    [SerializeField]
    private GameObject serverProcessSample;

    public void TestConnection() {
        string[] hostServerField, hostServerValue;
        InitializeServerData(out hostServerField, out hostServerValue, 
                             HiGameManager.dataGameManager.id, "Db_Player", "Tutorial", "1");
                             
        SendToHostServer("SaveStatus", "WriteAllTableValueFix", hostServerField, hostServerValue, null);
    }

    private void InitializeServerData(out string[] fieldArray, out string[] valueArray, 
                                      string id, string table, string title, string value) {
        fieldArray = new string[4] { "Id", "table_1", "title_1", "value_1" };
        valueArray = new string[4] { id, table, title, value };
    }

    public virtual void SendToHostServer(string targetRow, string phpAction, string[] fields, string[] values, UnityAction<string[]> resultAction ) {
        for (int i = 0; i < values.Length; i++) {
            values[i] = ReplaceSpace(values[i]);
        }

        GameObject instance = Instantiate(serverProcessSample);
        instance.SetActive(true);
        instance.GetComponent<ServerProcess>().ReadDataRows(targetRow, phpAction, fields, values, resultAction);
    }

    public string ReplaceSpace (string V) {
        string Result = "";
        if (V != "") {

            char[] A_Text = V.ToCharArray();

            int i = 0;
            for (i=0; i < A_Text.Length; i++) {
                if (A_Text[i] == ' ') {A_Text[i] = '_';}
            }

            // Tar.text = "";
            int j = 0;
            for (j=0; j < A_Text.Length; j++) {
                Result = Result + A_Text[j];
            }
        }
        return Result;
    }
}
