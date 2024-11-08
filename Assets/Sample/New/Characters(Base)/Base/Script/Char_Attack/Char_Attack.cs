using UnityEngine;

public class Char_Attack : MonoBehaviour, I_Char_Technique
{ // Char_Data_Technique
   // [SerializeField]
    Char_Utama _Char_Utama;
    public int maxCombo = 3; // Maksimum combo
    private int currentCombo = 0;
    private float comboTimer = 0f;
    public float comboResetTime = 1f; // Waktu untuk reset combo jika tidak ada input
    public float attackDelay = 0.5f; // Jeda waktu antara serangan
    private bool canAttack = true; // Menentukan apakah bisa menyerang atau tidak
   // public Animator animator; // Animator untuk mengontrol animasi serangan

    public Char_Technique [] attackGo; // Array untuk VFX serangan
    private GameObject previousAttack; // Untuk menyimpan attackGo sebelumnya
    
    void Update()
    {
        // Reset combo jika timer mencapai batas
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            { // attack combo reset
                currentCombo = 0;
                if (previousAttack != null)
                {
                    previousAttack.SetActive(false);
                }
                // animator.SetInteger("Combo", currentCombo);
            }
        }
        /*
        // Memeriksa input serangan
        if (Input.GetButtonDown("Fire1") && canAttack) // Atau sesuaikan dengan tombol serangan
        {
            PerformAttack();
        }
        */
    }

    // DualJoystickPlayerController
    public void PerformAttack()
    {
        if (Code_Attack == "Basic_Attack") {
            if (currentCombo < maxCombo)
            {
                currentCombo++;
            }
            else
            {
                currentCombo = 1; // Kembali ke combo pertama jika mencapai maxCombo
            }

            // Set timer combo reset
            comboTimer = comboResetTime;

            // Mengirimkan nilai combo ke animator
            // animator.SetInteger("Combo", currentCombo);

            // Memutar animasi serangan sesuai combo
            // animator.SetTrigger("Attack");

            // Mengeluarkan efek VFX sesuai combo
            PlayAttackVFX(currentCombo - 1);

            // Disable attack for a certain delay
            canAttack = false;
            Invoke(nameof(ResetAttack), attackDelay);
        } else if (Code_Attack == "Skill_Attack") {
            On_Play_Skill ();
        }
    }

    void PlayAttackVFX(int comboIndex)
    {
            // Nonaktifkan VFX sebelumnya
            if (previousAttack != null)
            {
                previousAttack.SetActive(false);
            }

            if (comboIndex >= 0 && comboIndex < attackGo.Length)
            {
                // Memastikan VFX ada dan aktifkan VFX
                if (attackGo != null)
                {
                    _Char_Utama._Char_Animation.On_Play_Animation_Finish_Event_2 ("Attack_1", _Char_Utama._Char_Animation.On_Idle);
                    attackGo[comboIndex].gameObject.SetActive(true);
                    attackGo[comboIndex].On_Slash (_Char_Utama);
                    previousAttack = attackGo[comboIndex].gameObject; // Simpan attackGo yang aktif
                }
            }
        
    }

    void ResetAttack()
    {
        canAttack = true; // Mengizinkan serangan berikutnya
    }

    // Fungsi ini dipanggil oleh animasi atau collider saat attack mengenai musuh
    public void OnAttackHit(GameObject EnemyCollider, Char_Technique _Char_Technique, int Skill_Power)
    {
        if (EnemyCollider != null)
        {
            Char_Utama Cu = EnemyCollider.GetComponent<Char_Utama>();
            Char_Status Cs = Cu._Char_Status;
            int damage = (_Char_Utama._Char_Status.Attack + Skill_Power) * -1;
          //  Cs.On_Cur_Hp(damage, attackGo[currentCombo- 1].gameObject);
            Cs.On_Cur_Hp(damage, _Char_Technique.gameObject);
        }
    }

     public void OnAttackHit(GameObject EnemyCollider, GameObject Go_From, int Skill_Power)
    {
        if (EnemyCollider != null)
        {
            Char_Utama Cu = EnemyCollider.GetComponent<Char_Utama>();
            Char_Status Cs = Cu._Char_Status;
            int damage = (_Char_Utama._Char_Status.Attack + Skill_Power) * -1;
          //  Cs.On_Cur_Hp(damage, attackGo[currentCombo- 1].gameObject);
            Cs.On_Cur_Hp(damage, Go_From);
        }
    }
    #region I_Char_Technique 
        #region Char_Utama
        public void On_Char_Technique_Max (int s) {
            
            Char_Technique_Empty_Slot = 0;
            attackGo = new Char_Technique [s];
        }

        public void On_Char_Technique_Skill_Max (int s) {
            
            Char_Technique_Skill_Empty_Slot = 0;
            skillGo = new Char_Technique [s];
        //    Debug.Log (s);
        }

        public void On_Set_Char_Utama (Char_Utama Cu) {
            _Char_Utama = Cu;
        }

        
        #endregion
        #region Char_Data_Technique - Char_Utama
            int Char_Technique_Empty_Slot = 0;
            int Char_Technique_Skill_Empty_Slot = 0;
        public void On_Get_Game_Object_Char_Technique (GameObject Go, string Code_Go) {
            Go.GetComponent <Char_Technique> ().On_Set_Char_Utama (_Char_Utama);
            Go.transform.SetParent (this.transform);
            Go.transform.localPosition = Vector3.zero;
            Go.transform.localScale = new Vector3 (1,1,1);
            if (Code_Go == "Basic_Attack") {
                attackGo[Char_Technique_Empty_Slot] = Go.GetComponent <Char_Technique> ();
                Char_Technique_Empty_Slot ++;
            } else if (Code_Go == "Skill_Attack") {
                skillGo[Char_Technique_Skill_Empty_Slot] = Go.GetComponent <Char_Technique> ();
                Char_Technique_Skill_Empty_Slot ++;
            }
            
        }
        #endregion
        #region Aiming_Direction
        public Char_Technique On_Get_Char_Technique_Basic_Attack () {
            return attackGo[currentCombo];
        }
        #endregion
        #region Skill
        [SerializeField]
        Char_Technique [] skillGo;
        
        void On_Play_Skill () {
            _Char_Utama._Char_Animation.On_Play_Animation_Finish_Event_2 ("Attack_1", _Char_Utama._Char_Animation.On_Idle);
            skillGo[Number_Button].gameObject.SetActive(true);
            skillGo[Number_Button].On_Slash (_Char_Utama);
        }

        // Char_AI_Attack:
        public Char_Technique [] On_Get_Skill_Go () {
            return skillGo;
        }

        #endregion
        #region Skill_Panel_Source
        [SerializeField]
        string Code_Attack = "";
        int Number_Button = 0;
        
        public void On_Set_Skill_Panel_Source (string Code, int Number_Button_V) { // Code : "Basic_Attack", "Skill_Attack"
            Code_Attack = Code; Number_Button = Number_Button_V;
            Debug.Log (Code + Number_Button);
            if (Code == "Basic_Attack") {
                PlayAttackVFX (Number_Button);
                _Char_Utama._Char_Utama_Source.On_Countdown_Skill (attackGo[Number_Button].Cd_Time);
            } else if (Code == "Skill_Attack") {
                On_Play_Skill ();
                _Char_Utama._Char_Utama_Source.On_Countdown_Skill (skillGo[Number_Button].Cd_Time);
            }
        }
        #endregion
  #endregion
}
