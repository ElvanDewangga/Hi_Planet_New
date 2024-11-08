using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CharStatusManager : MonoBehaviour
{
    [SerializeField] private List<CharStatus> allCharStatuses = new List<CharStatus>();
    public static CharStatusManager instance;
    void Awake () {
        instance = this;
    }

    public CharStatus LoadCharStatus (string name) {
        CharStatus result = null;
        foreach (CharStatus charStatus in allCharStatuses) {
            if (charStatus.name == name) {
                result = charStatus;
            }
        }
        return result;
    }
}
