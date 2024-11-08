using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Status : MonoBehaviour
{
    //
    [SerializeField] private Char_Utama _Char_Utama;

    public Aibehave ab;
    public string Username;
    public int Life, Attack, Defense, Speed, Intelligence, ExpDrop;

    [SerializeField] private Spawn_Exp_Particle _Spawn_Exp_Particle;

    private Char_Pack _Char_Pack;

    private void Start()
    {
        ab = _Char_Utama.transform.GetChild(_Char_Utama.transform.childCount - 1).GetComponent<Aibehave>();
    }

    private void getAibehave()
    {
    }

    #region Char_Data_Bonus_Point

    public int On_Char_Data_Bonus_Point(string code)
    {
        var Result = 0;
        switch (code)
        {
            case "Attack":
                Attack_Bonus += 1;
                Attack += 1;
                Result = Attack_Bonus;
                break;
            case "Defense":
                Defense_Bonus += 1;
                Defense += 1;
                Result = Defense_Bonus;
                break;
            case "Life":
                Life_Bonus += 1;
                Life += 1;
                On_Set_Life_Only();
                Result = Life_Bonus;
                break;
            case "Speed":
                Speed_Bonus += 1;
                Speed += 1;
                Result = Speed_Bonus;
                break;
            case "Intelligence":
                Intelligence_Bonus += 1;
                Intelligence += 1;
                Result = Intelligence_Bonus;
                break;
            default:
                Debug.LogError("Status Code " + code + "tidak ditemui atau error.");
                break;
        }

        return Result;
    }

    #endregion

    #region Char_Equipment

    // Code_Doing_V : "Equip", "Unequip"
    public void On_Char_Equipment_Value(string Code_Doing_V, string code, int y)
    {
        if (Code_Doing_V == "Unequip") y *= -1;
        switch (code)
        {
            case "Attack":
                Attack += y;
                break;
            case "Defense":
                Defense += y;
                break;
            case "Life":
                Life += y;
                On_Set_Life_Only();
                break;
            case "Speed":
                Speed += y;
                break;
            case "Intelligence":
                Intelligence += y;
                break;
            default:
                Debug.LogError("Status Code " + code + "tidak ditemui atau error.");
                break;
        }
    }

    #endregion

    #region Char_Utama

    public void On_Set_Char_Pack(Char_Pack s)
    {
        _Char_Pack = s;
        Life = _Char_Pack.Life;
        Attack = _Char_Pack.Attack;
        Defense = _Char_Pack.Defense;
        Speed = _Char_Pack.Speed;
        Intelligence = _Char_Pack.Intelligence;

        // On_Set_Life ();
    }

    #region Get_Data

    private int Life_Bonus, Attack_Bonus, Defense_Bonus, Speed_Bonus, Intelligence_Bonus;

    public void On_Get_Data_From_Char_Utama(int Life_V, int Attack_V, int Defense_V, int Speed_V, int Intelligence_V)
    {
        Life_Bonus = Life_V;
        Attack_Bonus = Attack_V;
        Defense_Bonus = Defense_V;
        Speed_Bonus = Speed_V;
        Intelligence_Bonus = Intelligence_V;
        Life += Life_V;
        Attack += Attack_V;
        Defense += Defense_V;
        Speed += Speed_V;
        Intelligence += Intelligence_V;
        On_Set_Life();
    }

    public void On_Get_Username_From_Char_Utama(string Username_V)
    {
        Username = Username_V;
        _Char_Utama._Char_Body_Component.On_Set_Username(Username);
    }

    #endregion

    #endregion

    #region Health

    [SerializeField] private int Cur_Hp, Max_Hp;

    // Char_Data_Variant_Attack :
    public int On_Get_Cur_Hp()
    {
        return Cur_Hp;
    }

    // this, Char_Equipment (Ketika load equipment diawal, setelah memberikan efek equipment) :
    public void On_Restore()
    {
        Cur_Hp = Max_Hp;
        b_Dead = false;
        if (_Char_Utama.Owner == "Player")
        {
            _Char_Utama._Char_Utama_Source.On_Refresh_Max_Hp(Max_Hp);
            _Char_Utama._Char_Utama_Source.On_Refresh_Cur_Hp(Cur_Hp);
        }

        _Char_Utama._Char_Body_Component.On_Set_Max_Hp(Max_Hp);
        _Char_Utama._Char_Body_Component.On_Set_Cur_Hp(Cur_Hp);
    }

    private void On_Cur_Hp(int v)
    {
        var Damage = 0;
        var Atk_From = 0;
        if (v < 0)
        {
            Atk_From = v * -1;
            Damage = v * 100 / Defense * 40 / 100;
            On_Play_Vfx_Active_When_Got_Damage();

            Cur_Hp += Damage;
            Debug.Log("Damage = " + Damage + "Atk/Def " + Atk_From + "/" + Defense);
            On_Damage_Vfx(Damage);
        }
        else
        {
            Cur_Hp += v;
            if (Cur_Hp > Max_Hp) Cur_Hp = Max_Hp;
            On_Heal_Vfx(v);
        }


        if (Cur_Hp <= 0)
        {
            Debug.Log("Dead");
            Cur_Hp = 0;
            if (ab) ab.isalive = false;
            if (_Spawn_Exp_Particle) _Spawn_Exp_Particle.On_Spawn_Exp();
            On_Dead();
        }
        else if (v < 0)
        {
            _Char_Utama._Char_Animation.On_Play_Animation_Finish_Event_2("Damage", _Char_Utama._Char_Animation.On_Idle);
        }

        if (_Char_Utama.Owner == "Player") _Char_Utama._Char_Utama_Source.On_Refresh_Cur_Hp(Cur_Hp);
        _Char_Utama._Char_Body_Component.On_Set_Cur_Hp(Cur_Hp);
    }

    [SerializeField] GameObject HitVfxSpawnerPrefab;
    [SerializeField]
    Object_Variant_Config Example_Obj;

    // Char_Attack, Enemy_Test_Canvas :
    public void On_Cur_Hp(int v, GameObject Go_From)
    {
        On_Cur_Hp(v);
        GameObject hitVfx = GameObject.Instantiate (HitVfxSpawnerPrefab);
        hitVfx.GetComponent <HitVfxSpawner> ().SetHitVfx (Go_From, this.gameObject, Example_Obj._Config_Char_Hit);
    }

    // Enemy_Test_Canvas :
    public void On_Instant_Damage(GameObject Go_From)
    {
        On_Cur_Hp(Max_Hp * -1);
        GameObject hitVfx = GameObject.Instantiate (HitVfxSpawnerPrefab);
        hitVfx.GetComponent <HitVfxSpawner> ().SetHitVfx (Go_From, this.gameObject, Example_Obj._Config_Char_Hit);
    }

    #endregion

    #region Dead

    private bool b_Dead;

    private void On_Dead()
    {
        if (!b_Dead)
        {
            Debug.Log("Dead");
            b_Dead = true;
            if (_Char_Utama.Owner == "Enemy")
            {
                //ab.isalive = false;
                //  Char_Data.Ins.Your_Char_Utama.GetComponent <Char_Utama> ()._Char_Level.On_Inc_Exp (20);
            }
            else if (_Char_Utama.Owner == "Player")
            {
                transform.parent.transform.tag = "Untagged";
                if (UIManager.instance) StartCoroutine(UIManager.instance.ShowGameOverPanel());
            }

            _Char_Utama._Char_AI.On_Set_AI("Char_AI_Idle");
            _Char_Utama._Char_Body_Component.Off_Set_All_Canvas();
            _Char_Utama._Char_Animation.On_Play_Animation_Finish_Event("Dead", _Char_Utama._Char_Animation.On_Dead);
        }
    }

    #endregion

    #region Life

    private void On_Set_Life()
    {
        Max_Hp = Life * 5;
        On_Restore();
        _Char_Utama._Char_Body_Component.On_Set_Max_Hp(Max_Hp);
    }

    private void On_Set_Life_Only()
    {
        Max_Hp = Life * 5;
        Cur_Hp = Max_Hp;
        if (_Char_Utama.Owner == "Player")
        {
            _Char_Utama._Char_Utama_Source.On_Refresh_Max_Hp(Max_Hp);
            _Char_Utama._Char_Utama_Source.On_Refresh_Cur_Hp(Cur_Hp); // new
        }

        _Char_Utama._Char_Body_Component.On_Set_Max_Hp(Max_Hp);
        _Char_Utama._Char_Body_Component.On_Set_Cur_Hp(Cur_Hp);
    }

    #endregion

    #region Vfx
    [SerializeField]
    GameObject damagePrefab;
    [SerializeField]
    GameObject healPrefab;    
    private void On_Damage_Vfx(int Damage)
    {
        var Ins = Instantiate(damagePrefab);
        Ins.GetComponent<Starsky_Vfx_Text>().On_Input_Text(Damage.ToString());
        Ins.transform.position = _Char_Utama.transform.position;
    }

    private void On_Heal_Vfx(int Heal)
    {
        var Ins = Instantiate(healPrefab);
        Ins.GetComponent<Starsky_Vfx_Text>().On_Input_Text(Heal.ToString());
        Ins.transform.position = _Char_Utama.transform.position;
    }

    #endregion

    #region Source

    #region Char_Data_Variant_Attack

    [SerializeField] private List<GameObject> L_Vfx_Active_When_Got_Damage = new();

    public void On_Add_Vfx_Active_When_Got_Damage(GameObject s)
    {
        L_Vfx_Active_When_Got_Damage.Add(s);
    }

    public void On_Remove_Vfx_Diactive_When_Got_Damage(GameObject s)
    {
        L_Vfx_Active_When_Got_Damage.Remove(s);
    }

    private void On_Play_Vfx_Active_When_Got_Damage()
    {
        foreach (var Cs in L_Vfx_Active_When_Got_Damage)
            Cs.GetComponent<Char_Data_Variant_Attack>().On_Set_Play_b_Active_When_Damage();
    }

    private void On_Refresh_Vfx_Active_When_Got_Damage()
    {
        L_Vfx_Active_When_Got_Damage = new List<GameObject>();
    }


    public void On_Give_Effect(Skill_Effect _Skill_Effect, int Int_Value)
    {
        switch (_Skill_Effect)
        {
            case Skill_Effect.Defense:
                On_Defense(_Skill_Effect, Int_Value);
                break;
            case Skill_Effect.Heal:
                On_Heal(_Skill_Effect, Int_Value);
                break;
        }
    }

    public void On_Add_C_Skill_Effect_Setup(Char_Data_Variant_Attack Cb, Skill_Effect_Setup _Setup,
        C_Skill_Effect_Setup _C_Skill_Effect_Setup)
    {
        var b_Same_Name = false;
        var Id_Same = 0;
        var x = 0;
        foreach (var Cse in L_C_Skill_Effect_Setup)
        {
            if (Cse == _C_Skill_Effect_Setup)
            {
                b_Same_Name = true;
                Id_Same = x;
                break;
            }

            x++;
        }

        if (b_Same_Name)
        {
            L_C_Skill_Effect_Setup[x].Cur_Time = _Setup.Effect_Time;
        }
        else
        {
            _C_Skill_Effect_Setup.Cur_Time = _Setup.Effect_Time;
            L_Char_Data_Variant_Attack.Add(Cb);
            L_C_Skill_Effect_Setup.Add(_C_Skill_Effect_Setup);
        }

        if (!b_N_Count_Time)
            if (L_C_Skill_Effect_Setup.Count > 0)
            {
                b_N_Count_Time = true;
                StartCoroutine(N_Count_Skill_Effect());
            }
    }

    private int Get_Id_L_C_Skill_Effect_Setup(C_Skill_Effect_Setup c)
    {
        var Id = 0;
        foreach (var cs in L_C_Skill_Effect_Setup)
        {
            if (cs == c) break;

            Id++;
        }

        return Id;
    }

    private bool b_N_Count_Time;

    private IEnumerator N_Count_Skill_Effect()
    {
        yield return new WaitForSeconds(1);
        L_C_Skill_Effect_Setup.ForEach(effect => effect.Cur_Time -= 1f);
        L_C_Skill_Effect_Setup.RemoveAll(effect =>
        {
            if (effect.Cur_Time <= 0)
            {
                for (var i = 0; i < effect.A_Skill_Effect.Length; i++)
                {
                    var Se = effect.A_Skill_Effect[i];
                    On_Give_Effect(Se, effect.A_Int_Value[i] * -1);
                }

                L_Char_Data_Variant_Attack[Get_Id_L_C_Skill_Effect_Setup(effect)].Off_Give_Effect(effect);
                L_Char_Data_Variant_Attack.Remove(L_Char_Data_Variant_Attack[Get_Id_L_C_Skill_Effect_Setup(effect)]);
                return true; // Hapus dari list
            }

            return false; // Tetap di list
        });

        if (L_C_Skill_Effect_Setup.Count > 0)
            StartCoroutine(N_Count_Skill_Effect());
        else
            b_N_Count_Time = false;
    }

    [SerializeField] private List<C_Skill_Effect_Setup> L_C_Skill_Effect_Setup = new();

    private readonly List<Char_Data_Variant_Attack> L_Char_Data_Variant_Attack = new();

    private void On_Defense(Skill_Effect _Skill_Effect, int Int_Value)
    {
        Defense += Int_Value;
    }

    private void On_Heal(Skill_Effect _Skill_Effect, int Int_Value)
    {
        On_Cur_Hp(Int_Value);
    }

    #endregion

    #endregion
}