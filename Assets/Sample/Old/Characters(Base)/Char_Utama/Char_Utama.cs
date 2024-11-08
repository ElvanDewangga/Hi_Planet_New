using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Char_Utama : MonoBehaviour
{
    public Char_Body_Component _Char_Body_Component;
    public Char_Status _Char_Status;
    public Char_Level _Char_Level;
    public Char_Utama_Source _Char_Utama_Source;
    public Char_Equipment _Char_Equipment;
    public Char_Attack _Char_Attack;
    public Char_Animation _Char_Animation;
    public Char_AI _Char_AI;
    public Char_Direction_2d _Char_Direction_2d;
    public Char_Dash _Char_Dash;
    public NavMeshAgent _NavMeshAgent;
    public Network_Char_Utama _Network_Char_Utama;

    [SerializeField] private Camera Raw_Camera;

    #region Char_Data_Technique

    public void On_Get_Game_Object_Char_Technique(GameObject Go, string Code_Go)
    {
        // Debug.Log ("Attack Transfer 1");

        if (Owner == "Player" || Owner == "Client")
            // Debug.Log ("Attack Transfer 2");
            _Char_Attack.On_Get_Game_Object_Char_Technique(Go, Code_Go);
        else if (Owner == "Enemy") _Char_Animation.On_Get_Game_Object_Char_Technique(Go, Code_Go);
    }

    #endregion

    #region DualJoystickPlayerController

    // Char_Technique :
    public GameObject Sphere_Direction;

    // Char_Attack_Move :
    public GameObject Aiming_Direction;

    // Char_Data_Variant_Attack :
    public Aiming_Direction _Aiming_Direction;

    // Aiming_Direction_Object:
    public GameObject Titik_Aim;
    public Char_Pack _Char_Pack;

    #endregion

    // Char_Data :

    #region Char_Pack

    [Space] public GameObject IndividualCharComponent;

    private void Start()
    {
        Invoke("GetCU", 0.1f);
    }

    private void GetCU()
    {
        IndividualCharComponent = transform.GetChild(transform.childCount - 1).gameObject;
    }

    public void On_Set_Char_Body_Component(Char_Pack P)
    {
        _Char_Pack = P;
        if (P.A_Aseprite_Sample == null)
        {
            _Char_Body_Component.On_Set_Char_Body_Component("Body", P.Sprite_Umum);
            _Char_Body_Component.On_Set_Char_Body_Component("Hand_Left", P.Sprite_Hand_Left);
            _Char_Body_Component.On_Set_Char_Body_Component("Hand_Right", P.Sprite_Hand_Right);
            _Char_Attack.On_Char_Technique_Max(P._Char_Pack_Technique.On_Get_A_Char_Technique_Settings().Length);
            if (P._Char_Pack_Technique_Skill)
            {
                _Char_Attack.On_Char_Technique_Skill_Max(P._Char_Pack_Technique_Skill.On_Get_A_Char_Technique_Settings()
                    .Length);
                Debug.Log(P._Char_Pack_Technique_Skill.On_Get_A_Char_Technique_Settings().Length);
            }

            _Char_Attack.On_Set_Char_Utama(this);
        }
        else
        {
            _Char_Body_Component.On_Diactive_All_Component();
            _Char_Animation.On_Set_Char_Pack(P);
            _Char_Animation.On_Char_Technique_Max(P._Char_Pack_Technique.On_Get_A_Char_Technique_Settings().Length);
            if (P._Char_Pack_Technique_Skill)
            {
                if (Owner == "Player" || Owner == "Client")
                {
                    _Char_Animation.On_Char_Technique_Skill_Max(P._Char_Pack_Technique_Skill
                        .On_Get_A_Char_Technique_Settings().Length);
                    _Char_Attack.On_Char_Technique_Skill_Max(P._Char_Pack_Technique_Skill
                        .On_Get_A_Char_Technique_Settings().Length);
                    // Latest _Aiming_Direction.On_Input_Char_Utama(this);
                }
                else
                {
                    _Char_Animation.On_Char_Technique_Skill_Max(P._Char_Pack_Technique_Skill
                        .On_Get_A_Char_Technique_Settings().Length);
                }


                Debug.Log(P._Char_Pack_Technique_Skill.On_Get_A_Char_Technique_Settings().Length);
            }

            _Char_Attack.On_Set_Char_Utama(this);
        }

        _Char_Body_Component.On_Set_Canvas();
        _Char_Status.On_Set_Char_Pack(P);


       // Latest Char_Data.Ins._Char_Data_Technique.On_Load_Char_Technique(this, P);
    }

    #endregion

    #region Data_Game_Source

    public void On_Get_Data_Game_Source(string[] Result)
    {
        int.TryParse(Result[2], out var Level_V);
        int.TryParse(Result[3], out var Bonus_Point_V);
        int.TryParse(Result[9], out var Cur_Exp_V);
        _Char_Level.On_Get_Data_From_Char_Utama(Level_V, Bonus_Point_V, Cur_Exp_V);

        int.TryParse(Result[4], out var Life_V);
        int.TryParse(Result[5], out var Attack_V);
        int.TryParse(Result[6], out var Defense_V);
        int.TryParse(Result[7], out var Speed_V);
        int.TryParse(Result[8], out var Intelligence_V);
        _Char_Status.On_Get_Data_From_Char_Utama(Life_V, Attack_V, Defense_V, Speed_V, Intelligence_V);

        int.TryParse(Result[10], out var Helmet_V);
        int.TryParse(Result[11], out var Armor_V);
        int.TryParse(Result[12], out var Drone_V);
        int.TryParse(Result[13], out var Wing_V);
        int.TryParse(Result[14], out var Intelligence_Cube_V);
        int.TryParse(Result[15], out var Fire_V);
        int.TryParse(Result[16], out var Water_V);
        int.TryParse(Result[17], out var Wood_V);
        int.TryParse(Result[18], out var Metal_V);
        int.TryParse(Result[19], out var Stone_V);

        _Char_Equipment.On_Get_Equipment_Id(Helmet_V, Armor_V, Drone_V, Wing_V, Intelligence_Cube_V, Fire_V, Water_V,
            Wood_V, Metal_V, Stone_V);
    }

    public void On_Get_Data_Account_Game_Source(string[] Result)
    {
        _Char_Status.On_Get_Username_From_Char_Utama(Result[1]);
    }

    #endregion

    #region Char_Data

    public string Owner = "";

    public void On_Set_Owner(string s)
    {
        Owner = s;
        if (Owner == "Player" || Owner == "Client")
        {
            _NavMeshAgent.enabled = false;
            if (Owner == "Player") Raw_Camera.gameObject.SetActive(true);
        }
        else if (Owner == "Enemy")
        {
            _NavMeshAgent.enabled = true;
            _NavMeshAgent.updateRotation = false;
            Raw_Camera.gameObject.SetActive(false);
            StartCoroutine(N_Set_Owner());
            Debug.Log("Enemy Setting");
        }
    }

    private IEnumerator N_Set_Owner()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<CharacterController>().enabled = true;
    }

    #endregion

    #region Char_Utama_Source

        #region Char_Direction_2d

        public string Char_Direction_2d = "";

        public void On_Get_Char_Direction_2d(string s)
        {
            Char_Direction_2d = s;
        }

        #endregion
        
        

    #endregion

    #region DualJoystickSource

    #endregion
}