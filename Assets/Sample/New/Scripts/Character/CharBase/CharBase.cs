using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.AI;

public class CharBase : MonoBehaviour {
    void Start () {
        ab = transform.GetChild(transform.childCount - 1).GetComponent<Aibehave>();
        ChangeGameObjectName ();
        TeleportManager.instance.TeleportPlayer (this.gameObject);
        
        SetupCharStatus ();
        InitializePlayerBody ();
        DataGameManager dataGameManager = HiGameManager.dataGameManager;
        SetupPlayerData ( dataGameManager.username, dataGameManager._DataGameEquipment.equipmentResult);
        HiGameManager.cameraMove.target = this.gameObject.transform;
        ConfigureCanvasForCharacter ();
        if (this is PlayerCharBase) {
            this.transform.tag = "Player";
            SetOwner ("Player");
        }

        Invoke("GetCU", 0.1f);
    }
    #region PlayerCharBase
    public virtual void InitializePlayerBody () 
    {
    }

    public virtual void SetupPlayerData (string Username, string[] Result) 
    {
    }

    public virtual void RefreshHUDCurrentHp(int hp)
    {
    }

    public virtual void RefreshHUDMaxHp(int maxHp)
    {
    }
    
    public virtual void ChangeGameObjectName () {
        this.gameObject.name = "Player(Clone)";
    }
    #endregion
   
    #region CharBase
        public string Owner = "Player";
        public NavMeshAgent _NavMeshAgent;
        public Network_Char_Utama _Network_Char_Utama;
        [SerializeField] private Camera Raw_Camera;
        #region DualJoystickPlayerController
        // Char_Technique :
        public GameObject Sphere_Direction;
        // Char_Attack_Move :
        public GameObject Aiming_Direction;
        // Char_Data_Variant_Attack :
        public Aiming_Direction _Aiming_Direction;
        // Aiming_Direction_Object:
        public GameObject Titik_Aim;
        #endregion

        // Char_Data :
        [Space] public GameObject IndividualCharComponent;
        private void GetCU()
        {
            IndividualCharComponent = transform.GetChild(transform.childCount - 1).gameObject;
        }

        void SetOwner (string s)
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
                StartCoroutine(SetOwnerNumerator());
                Debug.Log("Enemy Setting");
            }
        }

        private IEnumerator SetOwnerNumerator()
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<CharacterController>().enabled = true;
        }

        public string Char_Direction_2d = "";
        public void On_Get_Char_Direction_2d(string s)
        {
            Char_Direction_2d = s;
        }

    #endregion
    #region CharStatus
    [HideInInspector]
    public CharStatus charStatus;
    public void SetupCharStatus () {
        charStatus = CharStatusManager.instance.LoadCharStatus ("Hi");
        life = charStatus.life;
        attack = charStatus.attack;
        defense = charStatus.defense;
        speed = charStatus.speed;
        intelligence = charStatus.intelligence;

        Initializelife();
    }
    #endregion
    
    
    
    #region Canvas Management
    [SerializeField] Slider energySlider;
    [SerializeField] TMP_Text usernameText;
    [SerializeField] Image energySliderFill;
    [SerializeField] Canvas upperCanvas;
    [SerializeField] Canvas downCanvas;
    [SerializeField] Color colorBlue;
    [SerializeField] Color colorRed;
    public void SetUsernameText(string username) => usernameText.text = username;
    public void SetCurrentHpBar(int currentHp) {
        energySlider.value = currentHp;
    }
    public void SetMaxHpBar(int maxHp) {
        energySlider.maxValue = maxHp;
    
    }
    public void SetUpperCanvasActive(bool isActive) => upperCanvas.gameObject.SetActive(isActive);
    public void SetDownCanvasActive(bool isActive) => downCanvas.gameObject.SetActive(isActive);
    public void ConfigureCanvasForCharacter()
    {
        if (Owner == "Player")
        {
            SetUpperCanvasActive(true);
            SetDownCanvasActive(true);
            energySliderFill.color = colorBlue;
            energySlider.transform.localScale = Vector3.one;
        }
        else if (Owner == "Enemy")
        {
            SetUpperCanvasActive(false);
            SetDownCanvasActive(true);
            energySliderFill.color = colorRed;
            energySlider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

     public void DeactivateAllCanvases()
        {
            SetUpperCanvasActive(false);
            SetDownCanvasActive(false);
        }
    #endregion
    #region CharAttack
    [SerializeField] Char_Data_Variant_Attack _Char_Data_Variant_Attack;
    // SkillClickPanel
    public void PlaySkill (SkillClickButton skillClickButton, int number) {
      skillClickButton.StartCooldown (charStatus.skillConfigs[number].Base_Duration);
      GameObject Cloning = GameObject.Instantiate (_Char_Data_Variant_Attack.gameObject);
      //Cloning.transform.SetParent (Go_Peluru.transform);
      Cloning.transform.position = this.transform.position;
      Char_Data_Variant_Attack Cloning_Variant = Cloning.GetComponent <Char_Data_Variant_Attack> ();
      Cloning_Variant.On_Set_Clone ();
      Cloning_Variant.On_Set_Char_Data_Variant_Attack (this, charStatus.skillConfigs[number], Cloning, "Shoot"); 
      PlayAnimationWithCallback ("Attack_" + (number +1),Idle);
          
    }
    public Char_Dash _Char_Dash;
    #endregion
    

    #region Char Status
    public string Username;
    public int life, attack, defense, speed, intelligence, ExpDrop;

    [SerializeField] private Spawn_Exp_Particle _Spawn_Exp_Particle;
    public int life_Bonus, attack_Bonus, defense_Bonus, speed_Bonus, intelligence_Bonus;
    private bool isDead;
    private bool isCountingTime;

    // Health Attributes
    public int Cur_Hp, Max_Hp;
    private List<GameObject> activeDamageVfx = new();
    private List<C_Skill_Effect_Setup> skillEffectSetups = new();
    private List<Char_Data_Variant_Attack> charDataVariantAttacks = new();

    public Aibehave ab;


    #region Character Data and Bonus Points
    public int ApplyBonusPoint(string code)
    {
        int result = 0;
        switch (code)
        {
            case "attack":
                attack_Bonus++;
                attack++;
                result = attack_Bonus;
                break;
            case "defense":
                defense_Bonus++;
                defense++;
                result = defense_Bonus;
                break;
            case "life":
                life_Bonus++;
                life++;
                UpdatelifeOnly();
                result = life_Bonus;
                break;
            case "speed":
                speed_Bonus++;
                speed++;
                result = speed_Bonus;
                break;
            case "intelligence":
                intelligence_Bonus++;
                intelligence++;
                result = intelligence_Bonus;
                break;
            default:
                Debug.LogError($"Status Code {code} not found.");
                break;
        }
        return result;
    }
    #endregion

    #region Equipment Management
    public void UpdateEquipmentValue(string action, string code, int value)
    {
        if (action == "Unequip") value *= -1;

        switch (code)
        {
            case "Attack":
                attack += value;
                break;
            case "Defense":
                defense += value;
                break;
            case "Life":
                life += value;
                UpdatelifeOnly();
                break;
            case "Speed":
                speed += value;
                break;
            case "Intelligence":
                intelligence += value;
                break;
            default:
                Debug.LogError($"Status Code {code} not found.");
                break;
        }
    }
    #endregion


    #region Health Management
    
    [SerializeField] GameObject damageVfxPrefab;
    [SerializeField] GameObject healVfxPrefab;
    [SerializeField] GameObject HitVfxSpawnerPrefab;
    public int GetCurrentHp() => Cur_Hp;

    public void Restore()
    {
        Cur_Hp = Max_Hp;
        isDead = false;
        if (Owner == "Player")
        {
            RefreshHUDMaxHp(Max_Hp);
            RefreshHUDCurrentHp(Cur_Hp);
        }

        SetMaxHpBar(Max_Hp);
        SetCurrentHpBar(Cur_Hp);
    }

    private void ModifyHp(int value)
    {
        if (value < 0)
        {
            int atkFrom = Mathf.Abs(value);
            int damage = (value * 100 / defense) * 40 / 100;
            PlayDamageVfx(damage);

            Cur_Hp += damage;
            Debug.Log($"Damage = {damage} from Atk/Def {atkFrom}/{defense}");
        }
        else
        {
            Cur_Hp = Mathf.Min(Cur_Hp + value, Max_Hp);
            PlayHealVfx(value);
        }

        if (Cur_Hp <= 0)
        {
            Cur_Hp = 0;
            if (ab) {
                ab.isalive = false;
            }
            _Spawn_Exp_Particle?.On_Spawn_Exp();
            HandleDeath();
        }
        else if (value < 0)
        {
            PlayAnimationWithCallback("Damage", Idle);
        }

        if (Owner == "Player") SetCurrentHpBar(Cur_Hp);
        RefreshHUDCurrentHp(Cur_Hp);
    }

    public void ApplyDamage(int value, GameObject from, Object_Variant_Config vfxConfig)
    {
        ModifyHp(value);
        GameObject hitVfx = GameObject.Instantiate (HitVfxSpawnerPrefab);
        hitVfx.GetComponent <HitVfxSpawner> ().SetHitVfx (from, this.gameObject, vfxConfig._Config_Char_Hit);
        
       // from?.On_Hit_Vfx();
    }

    public void InstantDamage(GameObject from)
    {
        ModifyHp(-Max_Hp);
       // from?.On_Hit_Vfx();
    }
    #endregion

    #region Death Handling
    private void HandleDeath()
    {
        if (!isDead)
        {
            isDead = true;
            // _Char_Utama._Char_AI.On_Set_AI("Char_AI_Idle");
            DeactivateAllCanvases ();
            PlayAnimationWithCallback("Dead", TriggerDeath);

            if (Owner == "Player")
            {
               // transform.parent.transform.tag = "Untagged";
               Debug.Log ("Game Over x");
                UIManager.instance.StartCoroutine(UIManager.instance.ShowGameOverPanel());
            }
        }
    }
    #endregion

    #region life Calculation and VFX
    public virtual void Initializelife()
    {
        Max_Hp = life * 5;
        Restore();
        SetMaxHpBar (Max_Hp);
        SetCurrentHpBar(Cur_Hp);
        RefreshHUDMaxHp(Max_Hp);
        RefreshHUDCurrentHp(Max_Hp);
        
    }

    private void UpdatelifeOnly()
    {
        Max_Hp = life * 5;
        Cur_Hp = Max_Hp;
        if (Owner == "Player")
        {
            RefreshHUDMaxHp(Max_Hp);
            RefreshHUDCurrentHp(Cur_Hp);
        }

        SetMaxHpBar(Max_Hp);
        SetCurrentHpBar(Cur_Hp);
    }

    private void PlayDamageVfx(int damage)
    {
        var vfx = Instantiate(damageVfxPrefab);
        vfx.GetComponent<Starsky_Vfx_Text>().On_Input_Text(damage.ToString());
        vfx.transform.position = transform.position;
    }

    private void PlayHealVfx(int heal)
    {
        var vfx = Instantiate(healVfxPrefab);
        vfx.GetComponent<Starsky_Vfx_Text>().On_Input_Text(heal.ToString());
        vfx.transform.position = transform.position;
    }
    #endregion

    #region Vfx_Active_When_Got_Damage

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
    #endregion

    #region Skill Effects
    public void ApplySkillEffect(Skill_Effect effect, int value)
    {
        switch (effect)
        {
            case Skill_Effect.Defense:
                defense += value;
                break;
            case Skill_Effect.Heal:
                ModifyHp(value);
                break;
        }
    }

    public void RemoveSkillEffect(Skill_Effect effect, int value)
    {
        switch (effect)
        {
            case Skill_Effect.Defense:
                defense -= value;
                break;
        }
    }

    public void AddSkillEffect(Char_Data_Variant_Attack attack, Skill_Effect_Setup setup, C_Skill_Effect_Setup effectSetup)
    {
        int index = skillEffectSetups.FindIndex(e => e == effectSetup);

        if (index >= 0)
        {
            skillEffectSetups[index].Cur_Time = setup.Effect_Time;
        }
        else
        {
            effectSetup.Cur_Time = setup.Effect_Time;
            charDataVariantAttacks.Add(attack);
            skillEffectSetups.Add(effectSetup);
        }

        if (!isCountingTime)
        {
            isCountingTime = true;
            StartCoroutine(CountSkillEffectDuration());
        }
    }

    private IEnumerator CountSkillEffectDuration()
    {
        yield return new WaitForSeconds(1);
        skillEffectSetups.ForEach(effect => effect.Cur_Time -= 1);

        for (int i = 0; i < skillEffectSetups.Count; i++)
        {
            if (skillEffectSetups[i].Cur_Time <= 0)
            {
                charDataVariantAttacks[i].Off_Give_Effect (skillEffectSetups[i]);
                skillEffectSetups.RemoveAt(i);
                charDataVariantAttacks.RemoveAt(i); 
            }
        }

        isCountingTime = skillEffectSetups.Count > 0;
        if (isCountingTime) StartCoroutine(CountSkillEffectDuration());
    }
    #endregion
    #endregion

    #region CharAnimation

        private string lastAnimationName = "";
        private bool isAnimationComplete = false;
        private float animationClipTime = 0.0f;
        private Coroutine animationCoroutine;
        private Animator animator;
        private UnityAction animationFinishEvent;
        
        #region Animation Control
        public void PlayAnimation(string animationName) 
        {            
            SetAnimationTriggers(animationName);
            lastAnimationName = animationName;
            
        }

        

        public void PlayAnimationWithCallback(string animationName, UnityAction callback) 
        {
            isAnimationComplete = true;
            animationFinishEvent = callback;
            PlayAnimation(animationName);
                coroutineFinishPlayAnimation = StartCoroutine (FinishPlayAnimation ());
            
        }

        Coroutine coroutineFinishPlayAnimation;
        IEnumerator FinishPlayAnimation () {
            float clipTime = 0.0f;
            if (this is PlayerCharBase playerChar) {
                foreach (var s in playerChar.charStatus.animationDatas) {
                    if (s.name == lastAnimationName) {
                        clipTime = s.clip.length;
                    }
                }
            }
            yield return new WaitForSeconds (clipTime);
            animationFinishEvent();
            coroutineFinishPlayAnimation = null;
        }

        private void SetAnimationTriggers(string animationName) 
        {
            if (this is PlayerCharBase playerChar) {
                playerChar.animatorBody.SetTrigger(animationName);
                playerChar.animatorHandLeft.SetTrigger (animationName);
                playerChar.animatorHandRight.SetTrigger (animationName);
            }
        }

        private void ResetAnimator() 
        {
            animator = null;
            animationFinishEvent = null;
            isAnimationComplete = false;
        }

        private void Idle () {
            if (this is PlayerCharBase playerChar) {
                playerChar.animatorBody.SetBool("b_Idle", true);
                playerChar.animatorHandLeft.SetBool("b_Idle", true);
                playerChar.animatorHandRight.SetBool("b_Idle", true);
            }
            PlayAnimation("Idle");
        }
        #endregion
        
        #region Death and Drop Handling
        public void TriggerDeath() 
        {
            PlayAnimationWithCallback("Dead_Kelip", OnDefeated);
        }

        private void OnDefeated() 
        {
            if (Owner == "Enemy") 
            {
                Destroy(gameObject);
                HandleDrops();
            }
        }

        private void HandleDrops() 
        {
            Char_Data.Ins.Your_Char_Utama.GetComponent<Char_Utama>()
                ._Char_Level.On_Inc_Exp(transform.parent.GetChild(2).GetComponent<Char_Status>().ExpDrop);
            
            var lastChild = transform.parent.GetChild(transform.parent.childCount - 1);
            lastChild.GetComponent<EnemyRandomDrop>().dropRandom();

            if (lastChild.TryGetComponent(out BossProperty bossProperty)) 
            {
                BossDungeonManager.instance.BossDefeat();
            }
        }
        #endregion
    #endregion

    #region Char_Attack
     

        // Fungsi ini dipanggil oleh animasi atau collider saat attack mengenai musuh
        public void OnAttackHit(GameObject EnemyCollider, CharBase CharBase, int Skill_Power)
        {
            if (EnemyCollider != null)
            {
                Char_Utama Cu = EnemyCollider.GetComponent<Char_Utama>();
                Char_Status Cs = Cu._Char_Status;
                Debug.Log (EnemyCollider.name);
                int damage = (attack + Skill_Power) * -1;

                 
                // Latest   ApplyDamage (damage, _Char_Technique);
                if (Cu) {
                    Cs.On_Cur_Hp (damage, CharBase.gameObject);
                } else {
                    Debug.Log (CharBase.gameObject.name);
                 //   ApplyDamage(damage, CharBase.gameObject);
                }
            }
        }
    
    #endregion

    #region Char_Direction
    public string Code_Direction_2d = "";
    
    // DualJoystickPlayerController
    public void On_Char_Direction_2d (string v) {
        Code_Direction_2d = v;
        
        if (this is PlayerCharBase playerChar) {
            playerChar.handLeftRotation.On_Get_Char_Direction_2d (Code_Direction_2d);
            playerChar.handRightRotation.On_Get_Char_Direction_2d (Code_Direction_2d);
        }
    }
    #endregion
    
}
