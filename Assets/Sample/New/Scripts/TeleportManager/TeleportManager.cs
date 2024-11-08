using UnityEngine;

public class TeleportManager : MonoBehaviour {
    public static TeleportManager instance;
    void Awake () {
        instance = this;
    }
    public Transform playerSpawnPoint;
    // PlayerCharBase
    public void TeleportPlayer (GameObject playerClone) {
        playerClone.transform.position =  playerSpawnPoint.position;
    }
    // DungeonManager
    public void TeleportPlayerAtPoint (GameObject playerClone, Transform spawnPoint) {
        playerClone.transform.position = spawnPoint.position;
    }
}