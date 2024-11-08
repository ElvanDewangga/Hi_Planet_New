
using UnityEngine;

public class Spawn_Clone_Object : MonoBehaviour
{
    void Start () { // Scene (SampleScene, DungeonPCG)
         Sample_Scene.Ins._Search_Active_Object_World.On_Set_Spawn_Clone_Object (this);
    }

    #region Network_Go
    public void On_Set_Spawn_Clone_Object () {
        Sample_Scene.Ins._Search_Active_Object_World.On_Set_Spawn_Clone_Object (this);
    }
    #endregion
}
