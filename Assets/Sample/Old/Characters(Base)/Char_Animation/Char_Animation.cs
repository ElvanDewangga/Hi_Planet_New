using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Char_Animation : MonoBehaviour, I_Char_Technique { // Char_Data_Technique
    [SerializeField]
    Char_Utama _Char_Utama;
    [HideInInspector]
    public A_Aseprite _A_Aseprite;
    #region Char_Utama
    public void On_Set_Char_Pack (Char_Pack P) {
        if (P.A_Aseprite_Sample.gameObject != null) {
            GameObject Ins = GameObject.Instantiate (P.A_Aseprite_Sample.gameObject);
            _A_Aseprite = Ins.GetComponent <A_Aseprite> ();
            Ins.transform.SetParent (this.transform);
            Ins.transform.localPosition = new Vector3 (0,0,0);
            Ins.transform.localScale = new Vector3 (P.A_Aseprite_Sample.transform.localScale.x,P.A_Aseprite_Sample.transform.localScale.y,P.A_Aseprite_Sample.transform.localScale.z);
            On_Play_Animation ("Idle");
            
            // Hand_Left, Hand_Right :
            if (_A_Aseprite.A_Object_Target_Rotation_With_Direction.Length >0) {
            _A_Aseprite.A_Object_Target_Rotation_With_Direction[0].On_Set_Target (_Char_Utama.Sphere_Direction.gameObject.transform);
            _A_Aseprite.A_Object_Target_Rotation_With_Direction[1].On_Set_Target (_Char_Utama.Sphere_Direction.gameObject.transform);
            }

        }
    }
    #endregion

    #region Animation
    string Last_Anim_Name = "";
    // Animation :
    public void On_Play_Animation (string Anim_Name) {
        b_Animation_Complete = false;
        Animation_Finish_Event = null;
        
        animator = null;

        if (_A_Aseprite !=null) {
            
            if (_A_Aseprite.Cur_Char_Skin != null) {
                animator = _A_Aseprite.Cur_Char_Skin.A_Part[0].GetComponent <Animator> ();
            } else if (_A_Aseprite.Cur_Char_Skin == null) { // Ver.1
                animator = _A_Aseprite.GetComponent<Animator> ();
            }

            if (Anim_Name == "Idle") {
            animator.SetBool ("b_Idle", true);

            foreach (Animator Anm in _A_Aseprite.Child_Animator) {
                Anm.SetBool ("b_Idle", true);
            }

        }

            animator.SetTrigger (Anim_Name);

            foreach (Animator Anm in _A_Aseprite.Child_Animator) {
                Anm.SetTrigger (Anim_Name);
            }
        }
        Last_Anim_Name = Anim_Name;
    }

    UnityAction Animation_Finish_Event;
    public void On_Play_Animation_Finish_Event (string Anim_Name, UnityAction Ua) { // Ada bug Waktu animation Finish tidak sesuai karena target yang sekarang dimainkan
        b_Animation_Complete = true;
        if (Anim_Name== Last_Anim_Name) {
            OnAnimationComplete();
        }
        Debug.Log (Anim_Name);
       // Debug.Log ("Anim_Name " + Anim_Name);
        Animation_Finish_Event = Ua;
        animator = null;
        if (_A_Aseprite !=null) {
            animator = _A_Aseprite.GetComponent<Animator> ();

            animator.SetTrigger (Anim_Name);

            foreach (Animator Anm in _A_Aseprite.Child_Animator) {
                Anm.SetTrigger (Anim_Name);
            }
        }
        Last_Anim_Name = Anim_Name;
    }
    
    bool b_Animation_Complete = false;
    float Clip_Time = 0.0f;
    public void On_Play_Animation_Finish_Event_2 (string Anim_Name, UnityAction Ua) {
        Clip_Time = 0.0f;
        b_Animation_Complete = false;
        Debug.Log (Anim_Name);
       
        Last_Anim_Name = Anim_Name;
        if (C_N_On_Play_Animation_Finish_Event_2 == null) {
            // Debug.Log ("Anim_Name " + Anim_Name);
        Animation_Finish_Event = Ua;
        animator = null;
        
        if (_A_Aseprite !=null) {
            animator = _A_Aseprite.GetComponent<Animator> ();
            
            if (Anim_Name == "Attack_1") {
                animator.SetBool ("b_Idle", false);

                foreach (Animator Anm in _A_Aseprite.Child_Animator) {
                    Anm.SetBool ("b_Idle", false);
                }

            }
            foreach (var s in _Char_Utama._Char_Pack.A_C_Animation) {
                if (s.Name == Anim_Name) {
                    Clip_Time = s._AnimationClip.length;
                }
            }
             
            /*
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;

            // Loop melalui setiap clip yang ada di Animator
            
            foreach (AnimationClip clip in ac.animationClips)
            {
                // Cek apakah nama clip sesuai dengan nama state yang diinginkan
                if (clip.name == "Hi_Attack_1_Body")
                {
                    // Print atau gunakan durasi (length) animasi
                    Debug.Log("Durasi animasi '" + Anim_Name + "' adalah: " + clip.length + " detik.");
                    Clip_Time = clip.length;
                }
            }
            */


                animator.SetTrigger (Anim_Name);

                foreach (Animator Anm in _A_Aseprite.Child_Animator) {
                    Anm.SetTrigger (Anim_Name);
                }
            }

            C_N_On_Play_Animation_Finish_Event_2 = StartCoroutine (N_On_Play_Animation_Finish_Event_2 (Clip_Time));
        } else {
            StopCoroutine (C_N_On_Play_Animation_Finish_Event_2);
            C_N_On_Play_Animation_Finish_Event_2 = null;
            On_Idle ();
           StartCoroutine (N_Jeda_Idle (Anim_Name, Ua));
        }
    }
    IEnumerator N_Jeda_Idle (string Anim_Name, UnityAction Ua) {
        yield return new WaitForSeconds (0.1f);
         On_Play_Animation_Finish_Event_2 (Anim_Name, Ua);
    }
    Coroutine C_N_On_Play_Animation_Finish_Event_2;
    IEnumerator N_On_Play_Animation_Finish_Event_2 (float Time) {
        Debug.Log ("Time" + Time);
        yield return new WaitForSeconds (Time);
        OnAnimationComplete();
        C_N_On_Play_Animation_Finish_Event_2 = null;
    }
    #endregion
    #region Animation_Finish
    public Animator animator;
    [SerializeField]
    bool hasAnimationEnded = false;
    [SerializeField]
    float animationTime;
    [SerializeField]
    float Max_Time;
    
    void Update()
    {
        if (b_Animation_Complete) {
            if (animator != null) {
                // Mendapatkan informasi tentang animasi yang sedang berjalan
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                Max_Time =animator.GetCurrentAnimatorStateInfo(0).length;
                // Mengecek apakah animasi telah selesai (stateInfo.normalizedTime >= 1) 
                // dan apakah animasi yang sama belum pernah selesai sebelumnya
                
                if (stateInfo.normalizedTime >= 1 && !hasAnimationEnded)
                {
                    hasAnimationEnded = true;
                    OnAnimationComplete();
                    
                }

                // Reset flag jika animasi berubah atau diulang
                if (stateInfo.normalizedTime < 1)
                {
                    hasAnimationEnded = false;
                }
            }
        }
    }

    void OnAnimationComplete() {
        Debug.Log ("Complete");
        if (Animation_Finish_Event != null) {
            Animation_Finish_Event ();
            Debug.Log ("Complete 2");
        }
    }
    #endregion
    #region Animation_Fungsi_Umum
    public void On_Idle () {
       On_Play_Animation ("Idle");
    }

   // Sprite Last_Dead_Sprite;
    public void On_Dead () { // Char_Status
         Debug.Log ("Dead");
         //Last_Dead_Sprite = _A_Aseprite.gameObject.GetComponent<SpriteRenderer> ().sprite;
       // _A_Aseprite.gameObject.GetComponent<SpriteRenderer> ().sprite = Last_Dead_Sprite;    
         On_Play_Animation_Finish_Event ("Dead_Kelip", On_Defeated_Event);
    }

    void On_Defeated_Event () {
       if (_Char_Utama.Owner == "Enemy") {
        Destroy (_Char_Utama.gameObject);
        DropAndOther();
       }
    }
    void DropAndOther()
    {
        /* Latest Exp Drop
        Char_Data.Ins.Your_Char_Utama.GetComponent <Char_Utama> ()._Char_Level.On_Inc_Exp (transform.parent.GetChild(2).GetComponent<Char_Status>().ExpDrop);
        transform.parent.GetChild(transform.parent.childCount-1).GetComponent<EnemyRandomDrop>().dropRandom();
        */
        
        if(transform.parent.GetChild(transform.parent.childCount-1).TryGetComponent(out BossProperty bp))
        {
            BossDungeonManager.instance.BossDefeat();
        }
    }
    #endregion
    
    #region I_Char_Technique
        [SerializeField]
        Char_Technique [] attackGo;
        // Char_AI_Attack: (Not Use)
        public Char_Technique [] On_Get_Attack_Go () {
            return attackGo;
        }
       #region Char_Utama
        public void On_Char_Technique_Max (int s) {
            
            Char_Technique_Empty_Slot = 0;
            attackGo = new Char_Technique [s];
        }

        public void On_Char_Technique_Skill_Max (int s) {
            
            Char_Technique_Skill_Empty_Slot = 0;
            skillGo = new Char_Technique [s];
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
    #region Skill
        [SerializeField]
        Char_Technique [] skillGo;
        // Char_AI_Attack:
        public Char_Technique [] On_Get_Skill_Go () {
            return skillGo;
        }
        void On_Play_Skill () {
            skillGo[Number_Button].gameObject.SetActive(true);
            skillGo[Number_Button].On_Slash (_Char_Utama);
        }

        #endregion
        #region Skill_Panel_Source
        [SerializeField]
        string Code_Attack = "";
        int Number_Button = 0;
        
        public void On_Set_Skill_Panel_Source (string Code, int Number_Button_V) { // Code : "Basic_Attack", "Skill_Attack"
            Code_Attack = Code; Number_Button = Number_Button_V;
            if (Code == "Basic_Attack") {

            } else if (Code == "Skill_Attack") {

            }
        }
        #endregion
  #endregion
}
