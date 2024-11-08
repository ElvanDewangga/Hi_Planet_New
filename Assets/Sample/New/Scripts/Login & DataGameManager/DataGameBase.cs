using System.Collections;
using UnityEngine;

public abstract class DataGameBase : MonoBehaviour {
    [HideInInspector]
    public DataOperation CodeOption = DataOperation.None; 
    public string CodeDataGame = "";
    public string[] LoadStatusResult;

    public virtual void StartSave() {
        CodeOption = DataOperation.Save;
    }

    public virtual void StartSave(string[] hostServerFields, string[] hostServerValues) {
        CodeOption = DataOperation.Save;
    }

    public virtual void FinishSave() {}

    public virtual void StartLoad(string id) {
        CodeOption = DataOperation.Load;
        HiGameManager.dataGameManager.AddLoading(CodeDataGame);
    }

    public virtual void FinishLoad() {
         HiGameManager.dataGameManager.RemoveLoading(CodeDataGame);
    }

    public virtual void HandleLoadStatus(string[] result) {
        LoadStatusResult = result;
        if (result[0] == nameof(LoadStatus.Failed)) {
            HiGameManager.dataGameManager.AddFailedLoad (this);
            
        } else if (result[0] == nameof(LoadStatus.Success)) {
            if (CodeOption == DataOperation.Load) {
                FinishLoad();
            } else if (CodeOption == DataOperation.Save) {
                FinishSave();
            }
        }
    }

    public enum DataOperation
    {
        None,
        Save,
        Load
    }

    public enum LoadStatus
    {
        Failed,
        Success
    }

    
}
