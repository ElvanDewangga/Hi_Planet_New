using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Char_Data_Variant_Attack : MonoBehaviour
{
    public bool b_Clone;


    #region Char_Technique - Char_Utama_Source

    private int Vfx_Max_Loading;
    private int Vfx_Cur_Loading;
    private bool b_On_Loading;

    private void On_Inc_Cur_Loading()
    {
        Vfx_Cur_Loading++;
        if (b_On_Loading)
            if (Vfx_Cur_Loading >= Vfx_Max_Loading)
            {
                Vfx_Cur_Loading = 0;
                Vfx_Max_Loading = 0;
                b_On_Loading = false;
            }
    }

    // Char_Utama_Source :
    public void On_Set_Clone()
    {
        b_Clone = true;
    }

    // This (Clone Only) :
    public Char_Data_Variant_Attack_Settings On_Get_Char_Data_Variant_Attack_Settings(string Nm_Vfx)
    {
        return Dict_Char_Data_Variant_Attack[Nm_Vfx];
    }

    private GameObject Cur_GO_From, Cur_Cur_GO_From;
    private Object_Variant_Config Cur_Object_Variant_Config;
    private Object_Variant_Config.Object_Delay Cur_Object_Delay;
    CharBase currentCharBase;
    PlayerCharBase currentPlayerCharBase;
    string typeVfx;
    // PlayerCharBase
    public void On_Set_Char_Data_Variant_Attack(CharBase charBase, Object_Variant_Config objectVariantConfig, GameObject GO_From, string type)
    {
        currentCharBase = charBase; typeVfx = type;
        currentPlayerCharBase = charBase as PlayerCharBase;
        // Dapatkan nama VFX dari GO_From
        if (!b_On_Loading)
        {
            b_On_Loading = true;
            Cur_GO_From = GO_From;
            var currentCharBase = GO_From.GetComponent<Char_Technique>();
           
                /*
                currentCharBase.On_Set_Cd_Time(Cs.config.Base_Duration);
                currentCharBase.On_Destroy_Time(Cs.config.Destroy_Duration);
                */
                // Ambil Object_Delay dari Object_Variant_Config sesuai dengan index x
                Cur_Object_Variant_Config = objectVariantConfig;
                /*
                if (Cur_Object_Variant_Config._Animation_Part == Object_Variant_Config.Animation_Part.Static)
                {
                    if (currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite.Length > 2)
                    {
                        currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite[1]
                            .On_Change_Part("Static");
                        currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite[2]
                            .On_Change_Part("Static");
                    }
                }
                else if (Cur_Object_Variant_Config._Animation_Part == Object_Variant_Config.Animation_Part.Dynamic)
                {
                    if (currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite.Length > 2)
                    {
                        currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite[1]
                            .On_Change_Part("Dynamic");
                        currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite[2]
                            .On_Change_Part("Dynamic");
                    }
                }
                */

                b_Base_Config_Once = false;
                foreach (Object_Variant_Config.Object_Delay Cd in Cur_Object_Variant_Config.objectsWithDelay)
                {
                    Vfx_Max_Loading++;
                    // Jalankan coroutine untuk menambahkan delayTime
                    if (!Cd.b_Hide_Vfx) {
                        Cur_Object_Delay = Cd;
                    }
                        StartCoroutine(InstantiateWithDelay(Cd));
                }

                if (C_N_Base_Config == null)
                    C_N_Base_Config = StartCoroutine(N_Base_Config(Cur_Object_Variant_Config, currentCharBase));
            
        }
    }

    private Coroutine C_N_Base_Config;

    private IEnumerator N_Base_Config(Object_Variant_Config Base_Config, Char_Technique currentCharBase)
    {
        yield return new WaitForSeconds(Base_Config.Destroy_Duration);
        if (Base_Config._Animation_Part == Object_Variant_Config.Animation_Part.Static)
        {
            /*
            if (currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite.Length > 2)
            {
               
            }
            */
        }
        else if (Base_Config._Animation_Part == Object_Variant_Config.Animation_Part.Dynamic)
        {
            /*
            if (currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite.Length > 2)
            {
                currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite[1].On_Change_Part("Static");
                currentCharBase._Char_Utama._Char_Animation._A_Aseprite.A_Part_Aseprite[2].On_Change_Part("Static");
            }
            */
        }

        C_N_Base_Config = null;
        Destroy (this.gameObject);
    }

    private bool b_Base_Config_Once;

    public Vector3 CalculateNewPosition(Vector3 originalPosition, Vector3 Offset, Quaternion rotation, float distance)
    {
        //  Debug.Log (rotation);
        Vector3 direction;
        // Menggunakan rotasi untuk menghitung arah baru
        if (rotation.z < 0.7f && rotation.z > -0.7f)
        {
            //  Debug.Log ("Right");
            direction = rotation *
                        Vector3.right; // Gunakan Vector3.right untuk arah horizontal (2D), atau Vector3.forward untuk arah depan (3D)
        }
        else
        {
            //  Debug.Log ("left");
            direction = rotation *
                        Vector3.right; // Gunakan Vector3.right untuk arah horizontal (2D), atau Vector3.forward untuk arah depan (3D)
            Offset *= -1;
        }

        // Menghitung posisi baru berdasarkan arah dan jarak (distance)
        var newPosition = originalPosition + Offset + direction * distance;

        return newPosition;
    }

    private IEnumerator InstantiateWithDelay(Object_Variant_Config.Object_Delay objectDelay)
    {
        // Tunggu berdasarkan delayTime sebelum instansiasi objek
        yield return new WaitForSeconds(objectDelay.delayTime);
        // Instantiate gameObject dari config setelah delay
        GameObject Ins = null;
        if (typeVfx == "Active_When_Damage")
        {
            if (!objectDelay.b_Active_When_Damage) yield break;

            Ins = Instantiate(objectDelay.gameObject);
        }
        else if (typeVfx == "Slash" || typeVfx == "Shoot" || typeVfx == "Enemy")
        {
            if (!objectDelay.b_Active_When_Damage && !objectDelay._Config_Vfx_Down_Target)
            {
                // Instantiate Normal
                Ins = Instantiate(objectDelay.gameObject);
            }
            else
            {
                // -- Untuk menjalankan Perintah :
                Ins = Instantiate(objectDelay.gameObject);
                On_Set_Object_Variant_Config_Handler(objectDelay, Ins);
                Ins.gameObject.SetActive(false);

                yield break;
            }
        }
        else if (typeVfx == "Config_Vfx_Down_Target")
        {
            for (var i = 0; i < objectDelay._Config_Vfx_Down_Target.A_Position.Length; i++)
            {
                Ins = Instantiate(objectDelay.gameObject);
                yield return new WaitForSeconds(objectDelay._Config_Vfx_Down_Target.A_Delay_Time[i]);
                On_Start_Logic_Vfx_Down_Target(objectDelay,Ins,i);
            }

            yield break;
        }

        // Pengaturan Mechanism :
        if (typeVfx == "Config_Vfx_Down_Target")
        {
            // On_Start_Logic_Vfx_Down_Target (objectDelay,currentCharBase,Cur_GO_From,Ins);
        }
        else
        {
            On_Set_Object_Variant_Config_Handler(objectDelay, Ins);
        }


        // Terapkan skala ke setiap anak dari Ins
        Ins.transform.localScale = objectDelay.V3_Scale;
        foreach (Transform Ts in Ins.transform) Ts.transform.localScale = objectDelay.V3_Child_Scale;

        // Set posisi objek sesuai dengan Cur_GO_From atau GO_From
        if (Cur_GO_From != null)
        {
           
            // Ins.transform.localRotation *= Quaternion.Euler(objectDelay.V3_Rotation);
            if (objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Default ||
                objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Bullet)
                Ins.transform.position = Cur_GO_From.transform.position + objectDelay.V3_Offset_Vfx;
                
            else if (objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Character)
                Ins.transform.position = currentCharBase.transform.position + objectDelay.V3_Offset_Vfx;
               // currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ()._Char_Forward
            if (!objectDelay.b_Fix_Rotation && !objectDelay.b_Rotate_Around && !objectDelay.b_Follow_Char)
                Ins.transform.SetParent(Cur_GO_From.transform);
        }
        else
        {
            if (!objectDelay.b_Rotate_Around) Ins.transform.position = Cur_GO_From.gameObject.transform.position + objectDelay.V3_Offset_Vfx;
        }

        // Terapkan V3_Fix_Position jika ada
        if (objectDelay.V3_Fix_Position != Vector3.zero)
        {
            var Cln_Fix_Position = objectDelay.V3_Fix_Position;
            if (objectDelay.V3_Fix_Position.y != 0)
                if (Get_b_Fix_Position_Reverse(objectDelay, currentPlayerCharBase,
                        currentPlayerCharBase.currentSkinComponent.handRight.transform))
                    Cln_Fix_Position = new Vector3(objectDelay.V3_Fix_Position.x, objectDelay.V3_Fix_Position.y * -1,
                        objectDelay.V3_Fix_Position.z);
            if (!objectDelay.b_Rotate_Around) Ins.transform.localPosition = Cln_Fix_Position;
        }

        // Logika tambahan untuk tipe "Slash" atau "Shoot"
        if (typeVfx == "Slash" || typeVfx == "Active_When_Damage")
        {
            // Tambahkan logika untuk "Slash" jika diperlukan
        }
        else if (typeVfx == "Shoot")
        {
            //  if (!objectDelay.b_Fix_Rotation) {
            if (!objectDelay.b_Follow_Char && !objectDelay.b_Fix_Rotation)
            {
                var Direct_Rotation = DetectDirection(currentPlayerCharBase);
                if (Direct_Rotation == "Up_Right" || Direct_Rotation == "Down_Right") {
                    Ins.transform.rotation = currentPlayerCharBase.currentSkinComponent.handRight.transform.rotation * Quaternion.Euler (objectDelay.V3_Rotation); // Set rotasi untuk tipe Shoot
                }
                else {
                    Ins.transform.rotation = currentPlayerCharBase.currentSkinComponent.handRight.transform.rotation * Quaternion.Euler (objectDelay.V3_Rotation *-1); 
                }

                if (objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Hand_Right_Front) {
                    Debug.Log ("Hand Right Front");
                    currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ().On_Set_Local_Position_Char_Forward (objectDelay.V3_Fix_Position);
                    Ins.transform.position = currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ()._Char_Forward.transform.position;
                }
            }
            else
            {
                var Direct_Rotation = DetectDirection(currentPlayerCharBase);


                if (objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Hand_Right_Back)
                {
                    if (Direct_Rotation == "Up_Right" || Direct_Rotation == "Down_Right")
                        Ins.transform.rotation =
                            currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ().transform.rotation *
                            Quaternion.Euler(objectDelay.V3_Rotation);
                    else
                        Ins.transform.rotation =
                            currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ().transform.rotation *
                            Quaternion.Euler(objectDelay.V3_Rotation * -1);
                        currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ().On_Set_Local_Position_Char_Back(objectDelay.V3_Fix_Position);
                    Ins.transform.position = currentPlayerCharBase.currentSkinComponent.handRight.GetComponent <Object_Rotation_Target_With_Direction> ()._Char_Back.transform.position;
                } 
                else if (objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Character ||
                         objectDelay._Vfx_Spawn_Target == Object_Variant_Config.Vfx_Spawn_Target.Default)
                {
                    if (Direct_Rotation == "Up_Right" || Direct_Rotation == "Down_Right")
                        Ins.transform.rotation =
                            currentPlayerCharBase.playerBody.transform.rotation *
                            Quaternion.Euler(objectDelay.V3_Rotation);
                    else
                        Ins.transform.rotation =
                            currentPlayerCharBase.playerBody.transform.rotation *
                            Quaternion.Euler(objectDelay.V3_Rotation * -1);
                    Ins.transform.position =
                        currentPlayerCharBase.playerBody.transform.position + objectDelay.V3_Fix_Position;
                }
            }
        } 

        On_Set_Object_Variant_Config_Handler_2(objectDelay, Ins);
        // Aktifkan instansiasi objek setelah semua pengaturan diterapkan
        if (!objectDelay.b_Down_Target)
            Ins.SetActive(true);
        else
            Ins.SetActive(false); // Truenya dibagian On_Set_Object_Variant_Config_Handler - N_On_Set_Config_b_Down_Target

        if (!b_Base_Config_Once)
        {
            b_Base_Config_Once = true;
            // Debug.Log ("Dash");
            if (Cur_Object_Variant_Config.b_Vfx_Dash)
            {
                var direction = CalculateNewPosition(currentCharBase.transform.position,
                    Cur_Object_Variant_Config.V3_Vfx_Dash_Target, Ins.transform.rotation,
                    Cur_Object_Variant_Config.V3_Vfx_Dash_Target.x); // Gunakan Vector3.right untuk arah horizontal (2D)
                // Debug.Log (direction);
                currentCharBase._Char_Dash.On_Start_Dash(direction);
            }

            if (Cur_Object_Variant_Config.b_Skill_Effect) On_Set_b_Skill_Effect(Cur_GO_From);
        }

        yield return new WaitForSeconds(objectDelay.Vfx_Time);
        if (objectDelay.b_Hide_Object)
        {
            if (objectDelay.Hide_Object_Hand_Left)
                currentPlayerCharBase.currentSkinComponent.body.GetComponent <Part_Aseprite> ().On_Part();
            else if (objectDelay.Hide_Object_Hand_Right)
                currentPlayerCharBase.currentSkinComponent.body.GetComponent <Part_Aseprite> ().Off_Part();
        }

        On_Inc_Cur_Loading();
        if (Ins != null) Ins.SetActive(false);
    }

    #endregion

    #region Dictionary

    [SerializeField] private Transform A_Variant_Attack;

    private Dictionary<string, Char_Data_Variant_Attack_Settings> Dict_Char_Data_Variant_Attack = new();
    /*
    public void On_Load_Dicitionary()
    {
        Dict_Char_Data_Variant_Attack = new Dictionary<string, Char_Data_Variant_Attack_Settings>();
        foreach (Transform Ts in A_Variant_Attack) Ts.GetComponent<Char_Data_Variant_Attack_Settings>().On_Add_Asset();
    }
    */

    public void On_Add_Dict_Char_Data_Variant_Attack(string s, Char_Data_Variant_Attack_Settings cd)
    {
        Dict_Char_Data_Variant_Attack.Add(s, cd);
    }

    #endregion

    #region Object_Variant_Config_Handler (Vfx Handler)

    private void On_Set_Object_Variant_Config_Handler(Object_Variant_Config.Object_Delay objectDelay, GameObject Vfx_Go)
    {
        if (objectDelay._Object_Type == Object_Variant_Config.Object_Type.Default ||
            objectDelay._Object_Type == Object_Variant_Config.Object_Type.Vfx)
        {
            if (objectDelay.b_Down_Target)
            {
                if (objectDelay._Config_Vfx_Down_Target)
                    On_Config_Vfx_Down_Target();
                else
                    On_Set_Config_b_Down_Target(objectDelay, Vfx_Go);
            }

            if (objectDelay.b_Rotate_Around)
                On_Set_Config_b_Rotate_Around(objectDelay, Vfx_Go);

            if (objectDelay.b_Follow_Char)
                On_Set_Config_b_Follow_Char(objectDelay, Vfx_Go);

            if (objectDelay.b_Hide_Object) On_Set_b_Hide_Object(objectDelay);

            if (objectDelay.b_Vfx_Damage) On_Set_b_Vfx_Damage(objectDelay, Vfx_Go);

            if (objectDelay.b_Active_When_Damage) On_Set_b_Active_When_Damage();
        }
        else if (objectDelay._Object_Type == Object_Variant_Config.Object_Type.Shader)
        {
            On_Set_Object_Type_Shader(objectDelay);
        }
    }

    private void On_Set_Object_Variant_Config_Handler_2(Object_Variant_Config.Object_Delay objectDelay, GameObject Vfx_Go)
    {
        if (objectDelay.b_Light_Setup) On_Set_b_Light_Setup(objectDelay, currentPlayerCharBase, Vfx_Go);
    }

    #region Object_Type (Shader)

    public void On_Set_Object_Type_Shader(Object_Variant_Config.Object_Delay objectDelay)
    {
        currentPlayerCharBase.ChangeSkinComponent(objectDelay.C_Code_Skin);
    }

    private void Off_Set_Object_Type_Shader(Object_Variant_Config.Object_Delay objectDelay)
    {
        currentPlayerCharBase.ChangeLatestSkinComponent();
    }
  

    private IEnumerator b_build(Component component, Char_Technique currentCharBase)
    {
        yield return new WaitForSeconds(3);
        component.CopyComponent(currentCharBase._Char_Utama._Char_Animation._A_Aseprite.gameObject);
    }

    #endregion

    #region b_Light_Setup

    [SerializeField] private Logic_Light_Setup _Logic_Light_Setup;

    private void On_Set_b_Light_Setup(Object_Variant_Config.Object_Delay object_Delay, PlayerCharBase playerCharBase_v,
        GameObject Vfx_Go)
    {
        _Logic_Light_Setup.ApplySetup(playerCharBase_v.transform, Vfx_Go.transform, object_Delay._Config_Light_Setup);
    }

    #endregion

    private void On_Set_Config_b_Down_Target(Object_Variant_Config.Object_Delay objectDelay, GameObject Vfx_Go)
    {
        var Aiming = currentCharBase._Aiming_Direction.On_Get_Aiming_Direction(Cur_GO_From.transform, 5.0f, 3.0f, objectDelay.Down_Target_Col_Scale);
        var Aiming_Col = Aiming.GetComponent<Aiming_Direction_Collider>();
        var L_GO = Aiming_Col.On_Get_L_Target_Go();
        StartCoroutine(N_On_Set_Config_b_Down_Target(objectDelay, Vfx_Go, Aiming, L_GO));
    }

    private IEnumerator N_On_Set_Config_b_Down_Target(Object_Variant_Config.Object_Delay objectDelay, GameObject Vfx_Go, GameObject Aiming, List<GameObject> L_GO)
    {
        yield return new WaitForSeconds(0.1f);
        if (L_GO.Count > 0)
        {
            var nearestObject = L_GO[0];
            var closestDistanceSqr = Mathf.Infinity;
            var charPosition = currentCharBase.transform.position;

            foreach (var obj in L_GO)
            {
                var directionToObj = obj.transform.position - charPosition;
                var distanceSqr = directionToObj.sqrMagnitude; // Menggunakan kuadrat jarak

                if (distanceSqr < closestDistanceSqr)
                    if (obj.GetComponent<Char_Utama>()._Char_Status.On_Get_Cur_Hp() > 0)
                    {
                        closestDistanceSqr = distanceSqr;
                        nearestObject = obj;
                    }
            }

            if (nearestObject != null)
            {
                Vfx_Go.transform.position = nearestObject.transform.position + objectDelay.Down_Target_Offset;
                Vfx_Go.gameObject.SetActive(true);
                currentCharBase.OnAttackHit(nearestObject, currentCharBase, 0);
            }
        }

        Destroy(Aiming);
    }

    [SerializeField] private GameObject Titik_Tengah_Sample;

    private void On_Set_Config_b_Rotate_Around(Object_Variant_Config.Object_Delay objectDelay,  GameObject Vfx_Go)
    {
        Cur_GO_From.transform.position = currentCharBase.transform.position;
        // Titik Tengah :

        var Titik_Tengah = Instantiate(Titik_Tengah_Sample);
        Titik_Tengah.transform.SetParent(Cur_GO_From.transform);
        Titik_Tengah.transform.localPosition = objectDelay.Titik_Tengah_Start;

        Vfx_Go.transform.SetParent(Cur_GO_From.transform);
        Vfx_Go.transform.localPosition = objectDelay.V3_Offset_Vfx;
        Vfx_Go.AddComponent<Rotate_Variant_Attack>();
        var Rv = Vfx_Go.gameObject.GetComponent<Rotate_Variant_Attack>();
        Rv.On_Setup(Titik_Tengah.transform, objectDelay.Rotate_Around_Speed);

        // currentCharBase._Char_Utama._Char_Animation.
    }

    private void On_Set_Config_b_Follow_Char(Object_Variant_Config.Object_Delay objectDelay,  GameObject Vfx_Go)
    {
        // Cur_GO_From.transform.SetParent (currentCharBase._Char_Utama._Char_Animation._A_Aseprite.transform);

        Vfx_Go.transform.SetParent(currentPlayerCharBase.playerBody.transform);
        Vfx_Go.AddComponent<SelfDestruct>();
        Vfx_Go.GetComponent<SelfDestruct>().On_Set_Time(objectDelay.Vfx_Time);
    }

    private void On_Set_b_Hide_Object(Object_Variant_Config.Object_Delay objectDelay)
    {
        if (objectDelay.Hide_Object_Hand_Left)
            currentPlayerCharBase.currentSkinComponent.body.GetComponent <Part_Aseprite> ().Off_Part();
        else if (objectDelay.Hide_Object_Hand_Right)
           currentPlayerCharBase.currentSkinComponent.body.GetComponent <Part_Aseprite> ().Off_Part();
    }

    private void On_Set_b_Vfx_Damage(Object_Variant_Config.Object_Delay objectDelay, GameObject Vfx_Go)
    {
        // Pastikan Vfx_Go tidak null
        if (Vfx_Go != null)
            // Cek apakah b_Vfx_Damage aktif
            if (objectDelay.b_Vfx_Damage)
            {
                // Tambahkan BoxCollider ke Vfx_Go jika belum ada
                var boxCollider = Vfx_Go.GetComponent<BoxCollider>();
                if (boxCollider == null) boxCollider = Vfx_Go.AddComponent<BoxCollider>();

                // Atur ukuran BoxCollider sesuai dengan V3_Scale_Damage
                boxCollider.size = objectDelay.V3_Scale_Damage;
                boxCollider.isTrigger = true;
                // Atur offset BoxCollider sesuai dengan V3_Offset_Damage
                boxCollider.center = objectDelay.V3_Offset_Damage;
                if (typeVfx == "Enemy") {Vfx_Go.tag = "Enemy";}
                if (typeVfx == "Shoot") {Vfx_Go.tag = "Player";}
                // Tambahkan komponen Char_Attack_Collider_Sample jika belum ada
                var attackCollider = Vfx_Go.GetComponent<Char_Attack_Collider_Sample>();
                if (attackCollider == null) attackCollider = Vfx_Go.AddComponent<Char_Attack_Collider_Sample>();
                // Set the Char_Technique reference
                attackCollider.On_Input_Data(currentPlayerCharBase, objectDelay.Vfx_Damage_Power, Cur_Object_Variant_Config);
            }
    }

    #region b_Active_When_Damage

    private bool b_Once_Active_When_Damage;

    private void On_Set_b_Active_When_Damage()
    {
        if (!b_Once_Active_When_Damage)
        {
            b_Once_Active_When_Damage = true;
            currentCharBase.On_Add_Vfx_Active_When_Got_Damage(gameObject);
        }
    }

    private void Off_Set_b_Active_When_Damage(Char_Technique currentCharBase)
    {
        // if (b_Once_Active_When_Damage) {
        b_Once_Active_When_Damage = false;
        currentCharBase._Char_Utama._Char_Status.On_Remove_Vfx_Diactive_When_Got_Damage(gameObject);
        //}
    }

    // Char_Status : Ketika pemain terdeteksi kena Damage
    public void On_Set_Play_b_Active_When_Damage()
    {
        foreach (var Cd in Cur_Object_Variant_Config.objectsWithDelay)
        {
            Vfx_Max_Loading++;
            // Jalankan coroutine untuk menambahkan delayTime
            /*
            StartCoroutine(InstantiateWithDelay(Cd, currentCharBase, Cur_GO_From, Cur_Cur_GO_From,
                "Active_When_Damage"));
                */
            On_Set_Char_Data_Variant_Attack (currentCharBase,Cur_Object_Variant_Config,Cur_GO_From,"Active_When_Damage");    
        }
    }



    #endregion

    #region b_Fix_Position_Reverse

    // b_Up_Left, b_Up_Right, b_Down_Left, b_Down_Right

    private bool Get_b_Fix_Position_Reverse(Object_Variant_Config.Object_Delay Cur_Object_Variant_Config,
        PlayerCharBase currentCharBase, Transform Object)
    {
        
        var Code_Detect_Direction = DetectDirection(currentCharBase);
        if (Code_Detect_Direction == "Down_Left")
            // Debug.Log("DL");
            return Cur_Object_Variant_Config.b_Down_Left;
        if (Code_Detect_Direction == "Down_Right")
            //   Debug.Log("DR");
            return Cur_Object_Variant_Config.b_Down_Right;
        if (Code_Detect_Direction == "Up_Left")
            //  Debug.Log("UL");
            return Cur_Object_Variant_Config.b_Up_Left;
        if (Code_Detect_Direction == "Up_Right")
            //  Debug.Log("UR");
            return Cur_Object_Variant_Config.b_Up_Right;
        return false;
    }

    private string DetectDirection(PlayerCharBase currentCharBase)
    {
        if (currentCharBase) {
            return currentCharBase._Aiming_Direction.On_Get_4_Direction();
        } else {
            return "Down_Right";
        }
    }

    #endregion

    #endregion

    #region b_Skill_Effect

    [SerializeField] private GameObject Skill_Effect_Go_Sample;

    private List<CharBase> listCharBase = new();

    private void On_Set_b_Skill_Effect(GameObject Cur_GO_From)
    {
        // Off_Effect_Vfx ();
        listCharBase = new List<CharBase>();

        // Atur ukuran BoxCollider sesuai dengan V3_Scale_Damage
        foreach (var Cs in Cur_Object_Variant_Config._Skill_Effect_Setup.A_C_Skill_Effect_Setup)
        {
            if (Cs._World_Target == World_Target.Self)
            {
                listCharBase.Add(currentCharBase);
            }
            else if (Cs._World_Target == World_Target.Zone_All)
            {
                var Ins = Instantiate(Skill_Effect_Go_Sample);
                Ins.transform.SetParent(Cur_GO_From.transform);
                // Tambahkan BoxCollider ke Vfx_Go jika belum ada
                var boxCollider = Ins.GetComponent<BoxCollider>();
                if (boxCollider == null) boxCollider = Ins.AddComponent<BoxCollider>();
            }


            foreach (var Cu in listCharBase)
                Cu.AddSkillEffect(this, Cur_Object_Variant_Config._Skill_Effect_Setup, Cs);

            var x = 0;
            foreach (var Se in Cs.A_Skill_Effect)
            {
                On_Give_Effect(listCharBase, Se, Cs.A_Int_Value[x]);
                x++;
            }
        }

    }

    private void On_Give_Effect(List<CharBase> listCharBase_V, Skill_Effect _Skill_Effect, int Int_Value)
    {
        foreach (var Cu in listCharBase_V)
        {
            Cu.ApplySkillEffect(_Skill_Effect, Int_Value);
        }
    }

    // CharBase, Char_Status
    public void Off_Give_Effect(C_Skill_Effect_Setup _C_Skill_Effect_Setup)
    {
        /*
        if (b_Once_Active_When_Damage) Off_Set_b_Active_When_Damage(currentCharBase);
        */
        foreach (var Cu in listCharBase)
        {
            Cu.RemoveSkillEffect(_C_Skill_Effect_Setup.A_Skill_Effect[0], _C_Skill_Effect_Setup.A_Int_Value[0]);
        }

        foreach (var Cd in Cur_Object_Variant_Config.objectsWithDelay)
            // Jalankan coroutine untuk menambahkan delayTime
            if (Cd.b_Disable_Equals_Effect_Duration)
            {
                // Debug.Log("Off 1");
                if (Cd._Object_Type == Object_Variant_Config.Object_Type.Shader)
                {
                   // Debug.Log("Off 2");
                    Off_Set_Object_Type_Shader(Cd);
                }
            }
    }
   

    #endregion

    #region _Config_Vfx_Down_Target

    private void On_Config_Vfx_Down_Target()
    {
       // Latest StartCoroutine(InstantiateWithDelay(Cur_Object_Variant_Config, currentCharBase, Cur_GO_From,"Config_Vfx_Down_Target"));
       On_Set_Char_Data_Variant_Attack (currentCharBase,Cur_Object_Variant_Config,Cur_GO_From,"Config_Vfx_Down_Target"); 
    }

    private void On_Start_Logic_Vfx_Down_Target(Object_Variant_Config.Object_Delay objectDelay, GameObject Vfx_Go,int Urutan)
    {
        Vfx_Go.AddComponent<Target_Move>();
        var Tm = Vfx_Go.GetComponent<Target_Move>();
        var Start_Pos = currentCharBase.transform.position + objectDelay._Config_Vfx_Down_Target.A_Position[Urutan];
        var Target_Pos = currentCharBase.transform.position +
                         objectDelay._Config_Vfx_Down_Target.Target_Position[Urutan];
        var Speed = objectDelay._Config_Vfx_Down_Target.A_Speed_Down[Urutan];

        Tm.On_Set_Data(this, objectDelay, Urutan, Cur_GO_From, Start_Pos, Target_Pos, Speed);
        Vfx_Go.transform.localScale = objectDelay.V3_Scale;
        foreach (Transform S in Vfx_Go.transform) S.transform.localScale = objectDelay.V3_Child_Scale;
        Vfx_Go.transform.localRotation = Quaternion.Euler(objectDelay._Config_Vfx_Down_Target.A_Rotation[Urutan]);
        Vfx_Go.transform.SetParent(Cur_GO_From.transform);
        Vfx_Go.SetActive(true);
    }

    // Target_Move :
    public void On_Finish_Point_Vfx_Down_Target(Target_Move Tm)
    {
        var Obj = Tm.Object_Config;
        var Ledakan_Settings =
            Cur_Object_Variant_Config.objectsWithDelay[Obj._Config_Vfx_Down_Target.A_Finish_Vfx_Code[Tm.Urutan]];
        var Ledakan = Instantiate(Cur_Object_Variant_Config
            .objectsWithDelay[Obj._Config_Vfx_Down_Target.A_Finish_Vfx_Code[Tm.Urutan]].gameObject);
        Ledakan.transform.position = Tm.transform.position + Ledakan_Settings.V3_Offset_Vfx;
        Ledakan.transform.localScale = Ledakan_Settings.V3_Scale;
        foreach (Transform S in Ledakan.transform) S.transform.localScale = Ledakan_Settings.V3_Child_Scale;
        Ledakan.transform.localRotation = Quaternion.Euler(Ledakan_Settings.V3_Rotation);
        Ledakan.SetActive(true);
        Ledakan.transform.SetParent(Tm.GO_Peluru.transform);
        var Cn = new C_Delay_Time();
        StartCoroutine(Cn.N_On_Start(Ledakan_Settings, Ledakan));
        On_Set_Object_Variant_Config_Handler(Ledakan_Settings, Ledakan);

        if (Obj._Config_Vfx_Down_Target.A_Hilang_Vfx_Code[Tm.Urutan] > -1)
        {
            var Efek_Hilang_Settings =
                Cur_Object_Variant_Config.objectsWithDelay[Obj._Config_Vfx_Down_Target.A_Hilang_Vfx_Code[Tm.Urutan]];
            var Efek_Hilang = Instantiate(Cur_Object_Variant_Config
                .objectsWithDelay[Obj._Config_Vfx_Down_Target.A_Hilang_Vfx_Code[Tm.Urutan]].gameObject);
            Efek_Hilang.transform.position = Tm.transform.position + Efek_Hilang_Settings.V3_Offset_Vfx;
            Efek_Hilang.transform.localScale = Efek_Hilang_Settings.V3_Scale;
            foreach (Transform S in Efek_Hilang.transform) S.transform.localScale = Efek_Hilang_Settings.V3_Child_Scale;
            Debug.Log(Efek_Hilang.transform.localScale);
            Efek_Hilang.transform.localRotation = Quaternion.Euler(Efek_Hilang_Settings.V3_Rotation);
            Efek_Hilang.SetActive(true);
            Efek_Hilang.transform.SetParent(Tm.GO_Peluru.transform);
            var Cn2 = new C_Delay_Time();
            StartCoroutine(Cn2.N_On_Start(Efek_Hilang_Settings, Efek_Hilang));
        }

        StartCoroutine(N_Time(Tm));
    }

    private IEnumerator N_Time(Target_Move Tm)
    {
        yield return new WaitForSeconds(0.2f);
        Tm.gameObject.SetActive(false);
    }

    #endregion
} // END Char_Data_Variant_Attack

public class C_Delay_Time
{
    public IEnumerator N_On_Start(Object_Variant_Config.Object_Delay Cur_Object_Variant_Config, GameObject Vfx_Go)
    {
        yield return new WaitForSeconds(Cur_Object_Variant_Config.Vfx_Time);
        Vfx_Go.gameObject.SetActive(false);
    }
}

public static class ComponentExtensions // Ensure this class is static
{
    // Make sure the method is static
    public static T CopyComponent<T>(this T original, GameObject destination) where T : Component
    {
        if (original == null) return null; // Return if no component found on the source object

        var type = original.GetType();
        var copy = destination.AddComponent(type);

        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        foreach (var field in fields) field.SetValue(copy, field.GetValue(original));

        return copy as T;
    }
}