using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Char_Data : MonoBehaviour
{
    public static Char_Data Ins;
    public GameObject Aiming_Direction_Samp;

    [SerializeField] public Char_Utama Char_Utama_Sample;
    public Char_Utama Char_Utama_Enemy_Sample;


    public Char_Data_Level _Char_Data_Level;
    public PlayerBonusPoint _Char_Data_Bonus_Point;
    [FormerlySerializedAs("_Char_Data_Equipment")] public EquipmentManager charDataEquipmentManager;
    public Char_Data_Hit _Char_Data_Hit;
    public Char_Data_Variant_Attack _Char_Data_Variant_Attack;
    public Char_Data_Technique _Char_Data_Technique;
    
    private void Awake()
    {
        Ins = this;
    }

    private Char_Utama On_Get_Char_Utama_Sample(string s)
    {
        if (s == "Enemy")
            return Char_Utama_Enemy_Sample;
        return Char_Utama_Sample;
    }


    #region Char_Pack

    [SerializeField] private Transform A_Char_Pack;

    // test button :
    public void On_Set_Char_Pack(string Char_Pack_Name)
    {
        /*
           Mengubah karakter dan memberi status karakter kamu sesuai dengan Char_Pack

        */
        foreach (Transform t in A_Char_Pack)
        {
            var Cp = t.GetComponent<Char_Pack>();
            if (Cp.name == Char_Pack_Name)
            {
                Your_Char_Utama.GetComponent<Char_Utama>().On_Set_Char_Body_Component(Cp);
                return;
            }
        }
    }

    // Char_Spawn :
    public void On_Set_Char_Pack(Char_Utama _Char_Utama, string Char_Pack_Name)
    {
        /*
           Mengubah karakter dan memberi status karakter kamu sesuai dengan Char_Pack

        */
        foreach (Transform t in A_Char_Pack)
        {
            var Cp = t.GetComponent<Char_Pack>();
            if (Cp.name == Char_Pack_Name)
            {
                _Char_Utama.On_Set_Char_Body_Component(Cp);
                return;
            }
        }
    }

    #endregion

    #region Char_Utama

    // Sample_Scene :


    [SerializeField] private Char_Spawn Player_Char_Spawn_Sample;

    public Char_Spawn On_Get_Player_Char_Spawn()
    {
        return Player_Char_Spawn_Sample;
    }

    #endregion

    #region Char_Spawn

    public void On_Create_Char_Utama_With_Char_Spawn(Char_Spawn _Char_Spawn)
    {
        // Debug.Log ("Char Data");
        var Char_Sample = On_Get_Char_Utama_Sample(_Char_Spawn.Code_Char).gameObject;

        var Ins = Instantiate(Char_Sample);
        Ins.name = "Clone_" + _Char_Spawn.Code_Char;
        Ins.transform.position = _Char_Spawn.gameObject.transform.position;
        Ins.gameObject.SetActive(true);
        Ins.gameObject.SetActive(false);
        Ins.gameObject.SetActive(true);
        var Cu = Ins.GetComponent<Char_Utama>();
        Cu.On_Set_Owner(_Char_Spawn.Code_Char);
        Char_Data.Ins.On_Set_Char_Pack(Cu, _Char_Spawn.Code_Char_Pack);
        Ins.tag = _Char_Spawn.Code_Char;
        Cu._Char_Status.On_Get_Data_From_Char_Utama(0, 0, 0, 0, 0);
        Spawn_System_Parenting(Ins);

        if (_Char_Spawn.Code_Char == "Player")
        {
            Your_Char_Utama_Script = Ins.GetComponent<Char_Utama>();
            DualJoyStickPlayerController_Set_Player(Ins);
            Your_Char_Utama = Ins;
            L_Char_Utama_Network.Add(Ins);
            Ins.gameObject.tag = "Player";
        }

        // Debug.Log ("Enemy Setup 2");

        setEnemyIndiv(_Char_Spawn, Ins.GetComponent<Char_Utama>(), Ins);
        //  Ins.GetComponent <CharacterController> ().enabled = true;
        //  Instantiate(_Char_Spawn.transform.GetChild(0).gameObject,Ins.transform);
    }

    private void setEnemyIndiv(Char_Spawn spawnindiv, Char_Utama Cuindiv, GameObject ins)
    {
        var insindiv = Instantiate(spawnindiv.transform.GetChild(0).gameObject, ins.transform);
        insindiv.transform.localPosition = Vector3.zero;
        var enemynav = ins.GetComponent<NavMeshAgent>();
        if (insindiv.TryGetComponent(out Aibehave ab))
        {
            ab.nav = enemynav;
            enemynav.stoppingDistance = ab.DistanceAttack;
        }

        if (insindiv.TryGetComponent(out MaceBotBossAI mbbai))
        {
            mbbai.nav = enemynav;
            mbbai.GetStartPropety();
            mbbai.maceattack.getCu();
            //enemynav.stoppingDistance = mbbai.DistanceAttack;
        }

        enemynav.enabled = true;
        enemynav.speed = Cuindiv._Char_Status.Speed;
    }

    public void On_Set_Status(Char_Spawn _Char_Spawn, GameObject Go_V)
    {
        Go_V.name = "Clone_" + _Char_Spawn.Code_Char;
        Go_V.transform.position = _Char_Spawn.gameObject.transform.position;
        Go_V.gameObject.SetActive(true);
        Go_V.gameObject.SetActive(false);
        Go_V.gameObject.SetActive(true);
        var Cu = Go_V.GetComponent<Char_Utama>();
        Cu.On_Set_Owner(_Char_Spawn.Code_Char);
        Debug.Log(_Char_Spawn.Code_Char_Pack);
        if (_Char_Spawn.Code_Char_Pack != "") Ins.On_Set_Char_Pack(Cu, _Char_Spawn.Code_Char_Pack);
        Go_V.tag = _Char_Spawn.Code_Char;
        Cu._Char_Status.On_Get_Data_From_Char_Utama(0, 0, 0, 0, 0);
        Spawn_System_Parenting(Go_V);

        if (_Char_Spawn.Code_Char == "Player")
        {
            Your_Char_Utama_Script = Go_V.GetComponent<Char_Utama>();
            DualJoyStickPlayerController_Set_Player(Go_V);
            Your_Char_Utama = Go_V;
            L_Char_Utama_Network.Add(Go_V);
            Go_V.gameObject.tag = "Player";
        }
        else if (_Char_Spawn.Code_Char == "Enemy")
        {
            Debug.Log("ENEMY SETUP");
            setEnemyIndiv(_Char_Spawn, Go_V.GetComponent<Char_Utama>(), Go_V);
        }
    }

    public void On_Spawn_Teleport(Char_Spawn _Char_Spawn, GameObject Go_V)
    {
        Go_V.transform.position = _Char_Spawn.transform.position;
        //  Debug.Log ("Transform Sama");
    }

    #endregion

    #region Network

    // Data_Game_Account :
    public GameObject Your_Char_Utama;
    public Char_Utama Your_Char_Utama_Script;
    [SerializeField] private List<GameObject> L_Char_Utama_Network;

    #endregion

    #region Vfx

    public GameObject Vfx_Text_Damage;
    public GameObject Vfx_Text_Heal;

    public GameObject Exp_Particle_Go;

    // Item_Trigger :
    public GameObject World_Baloon_Samp;

    #endregion

    #region Char_Data_Source
        #region Char_Data_Bonus_Point

        public void Char_Data_Bonus_Point_Save(string Bonus_V, int Value_V)
        {
            var Host_Server_Value = new string [7]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            var Host_Server_Field = new string [7];
            Host_Server_Field[0] = "Id";
            Host_Server_Value[0] = Data_Game_Utama.Ins._Data_Game_Account.Id;
            Host_Server_Field[1] = "table_1";
            Host_Server_Value[1] = "Db_Equipment";
            Host_Server_Field[2] = "title_1";
            Host_Server_Value[2] = Bonus_V;
            Host_Server_Field[3] = "value_1";
            Host_Server_Value[3] = Value_V.ToString();

            Host_Server_Field[4] = "table_2";
            Host_Server_Value[4] = "Db_Equipment";
            Host_Server_Field[5] = "title_2";
            Host_Server_Value[5] = "Bonus_Point";
            Host_Server_Field[6] = "value_2";
            Host_Server_Value[6] = Char_Data.Ins.Your_Char_Utama_Script._Char_Level.Bonus_Point.ToString();

            Data_Game_Utama.Ins._Data_Game_Equipment.StartSave(Host_Server_Field, Host_Server_Value);
        }

        #endregion

        #region Char_Data
        [SerializeField] private Camera Front_Camera_Target;

        [SerializeField] private Transform Target_Par_Front_Camera;
        public virtual void Spawn_System_Front_Camera(GameObject Target)
        {
            Target.transform.SetParent(Target_Par_Front_Camera);
        All_Scene_Go.Ins._Spawn_System.On_Titik_Pemain_Spawn(Target);
        }

        public virtual void DualJoyStickPlayerController_Set_Player(GameObject Target)
        {
            All_Scene_Go.Ins._DualJoystickPlayerController.On_Input_Data_Player(Target);
        All_Scene_Go.Ins._Camera_Move.On_Set_Target(Target.transform);
        }

        public void Spawn_System_Parenting(GameObject Target)
        {
            //   Debug.Log ("Transform");
            Target.transform.SetParent(Sample_Scene.Ins._Search_Active_Object_World.On_Get_A_Object().transform);
        }

        #endregion

        #region Char_Data_Equipment

        public void On_Char_Data_Equipment(GameObject Tabel_V, GameObject BacK_Tabel_Go)
        {
            All_Scene_Go.Ins._Tabel_Popup.On_Set_Tabel_Popup(Tabel_V, "Equipment_Setup", BacK_Tabel_Go.transform);
            All_Scene_Go.Ins._Inventory._Item_Detail.On_Direct_Code_Detail("Equipment_Setup");
            All_Scene_Go.Ins._Inventory.On_Inventory();
        }

        public void Off_Char_Data_Equipment()
        {
            All_Scene_Go.Ins._Inventory.Off_Inventory();
            All_Scene_Go.Ins._Inventory._Item_Detail.Off_Direct_Code_Detail();
        }

        #endregion
    #endregion
}