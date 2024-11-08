using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataGameManager : MonoBehaviour {
    
    public string id = "";
    public string username = "";
    public string password = "";

    // public Data_Game_Player _DataGamePlayer;
    public Data_Game_Inventory _DataGameInventory;
    public Data_Game_Equipment _DataGameEquipment;
    public Data_Game_Storage _DataGameStorage;

    // Login:
    public void LoadPlayerDataAsync(string id_v, string username_v, string password_v) {
        id = id_v; username = username_v; password = password_v;
        LoadingManager.instance.StartInvisibleLoading("Loading_2", LoadPlayerEquipment, 0);
        StartCoroutine(LoadPlayerDataCoroutine());
    }

    private IEnumerator LoadPlayerDataCoroutine() {
        yield return new WaitForSeconds(1.0f);

        _DataGameInventory.StartLoad(id);
        _DataGameStorage.StartLoad(id);
    }
    // called when loading finish on LoadPlayerDataAsync
    private void LoadPlayerEquipment() {
        _DataGameEquipment.StartLoad(id);
        LoadingManager.instance.StartInvisibleLoading("Loading_2", LoadLobbyScene, 0);
    }

    // called when loading finish on LoadPlayerEquipment
    private void LoadLobbyScene() {
        StartCoroutine (LoadLobbySceneNumerator ());
    }

    IEnumerator LoadLobbySceneNumerator () {
        LoadingManager.instance.ShowLoading ("Loading_1");
        AsyncOperation async = SceneManager.LoadSceneAsync ("LobbyScene", LoadSceneMode.Additive);
        yield return new WaitUntil (() => async.isDone);
        yield return new WaitForSeconds (2);
        LoadingManager.instance.HideLoading ("Loading_1");
    }

    #region Source
        private List<DataGameBase> failedDataGamesBase = new List<DataGameBase>();
        

        #region Methods

        // Handle failed load for Data_Game
        public void AddFailedLoad(DataGameBase dataGameBase) 
        {
            failedDataGamesBase.Add(dataGameBase);
        }

        #region Player Data Handling
        // Data_Game_Storage
        public void SendDataToPlayer(string[] result) 
        {
            Data_Game_Utama.Ins._Data_Game_Account.On_Get_Data_Game_Source(result);
            Char_Data.Ins.Your_Char_Utama.GetComponent<Char_Utama>().On_Get_Data_Account_Game_Source(result);

            All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Username(Data_Game_Utama.Ins._Data_Game_Account.Username);
            Char_Data.Ins.Your_Char_Utama_Script._Network_Char_Utama.On_Get_Data_From_Data_Game_Source(result[5]);
        }
        #endregion

        

        #region Loading Management

        public void AddLoading(string code) 
        {
            LoadingManager.instance.AddInvisibleLoading(code);
        }

        public void RemoveLoading(string code) 
        {
            LoadingManager.instance.RemoveInvisibleLoading(code);
        }
        #endregion

        #endregion

        #region Utility Methods

        // Converts specified indexes of result array to a tuple of strings
        public (string, string) ConvertResultToStrings(string[] result, int index1, int index2) 
        {
            var part1 = ConvertStringToArray(result[index1])[0]; // Gets the first element after splitting
            var part2 = ConvertStringToArray(result[index2])[0]; // Gets the first element after splitting
            return (part1, part2);
        }

        public (string[], string[]) ConvertResultToStringArrays(string[] result, int index1, int index2) 
        {
            var part1Array = ConvertStringToArray(result[index1]); // Returns a string[]
            var part2Array = ConvertStringToArray(result[index2]); // Returns a string[]
            return (part1Array, part2Array);
        }
        
       public string[] ConvertStringToArray(string input) 
        {
            return input.Split(':');
        }
        #endregion
    #endregion

}
