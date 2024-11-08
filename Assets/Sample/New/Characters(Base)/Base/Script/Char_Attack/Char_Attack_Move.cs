using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Esf;
using Unity.VisualScripting;
using UnityEngine;

public class Char_Attack_Move : MonoBehaviour {
    public float moveSpeed = 5f; // Kecepatan gerak maju objek
    // public float Cd_Time = 3f;   // Waktu objek akan hancur

    [Header("Collide_Type")]
    public bool ismovethrough = true;
    [Tooltip("false = right, true = forwardAim")]public bool useaimtoplayer;

    void Start() 
    {
        StartCoroutine(Moveduration(GetComponent<SelfDestruct>().selfdestruct_in - 0.3f));
        StartCoroutine(getrotate());
    }

    IEnumerator getrotate()
    {
        yield return new WaitForSeconds(0.05f);
        if(useaimtoplayer == true)
        {
            Vector3 totarget = _Char_Technique._Char_Utama.IndividualCharComponent.GetComponent<instantproperty>().target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(totarget); // Menyamakan rotasi'
            transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update() {
        // Menggerakkan objek ke arah rotasinya (forward direction) dengan kecepatan yang ditentukan
        if(useaimtoplayer == true)
        {
            transform.position += transform.forward * (moveSpeed * Time.deltaTime);
        }else{
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    public IEnumerator Moveduration(float dur)
    {
        yield return new WaitForSeconds(dur);
        if(ismovethrough == false)
        {
            moveSpeed = 0;
            if(transform.GetChild(1).TryGetComponent(out Animator anim))
            {
                if(AnimParamChecker.ContainsParam(anim, "vfxanim"))
                {
                    transform.GetChild(1).GetComponent<Animator>().SetInteger("vfxanim", 1);
                    StartCoroutine(WaitVFXAnimationFinish(transform.GetChild(1).GetComponent<Animator>()));
                }
            }

            if(TryGetComponent(out BoxCollider bc))
            {
                bc.enabled = false;
            } 
            if(TryGetComponent(out SphereCollider sc))
            {
                sc.enabled = false;
            }   
        }
        yield return null;
    }
    IEnumerator WaitVFXAnimationFinish(Animator anim)
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        Destroy(gameObject);
        yield return null;
    }

    void On_Refresh_Data () {
       // Latest _Char_Technique._Char_Utama._Aiming_Direction.On_Aiming_Direction (_Char_Technique, this.gameObject.transform, moveSpeed, _Char_Technique.Cd_Time);
         _Char_Technique._Char_Utama._Aiming_Direction.On_Aiming_Direction ( this.gameObject.transform, moveSpeed, 1);
    }

    #region Char_Technique
    [HideInInspector]public Char_Technique _Char_Technique;
    public void On_Set_Data (Char_Technique Char_Technique_V) {
        _Char_Technique = Char_Technique_V;
        On_Refresh_Data ();
    }
    #endregion
}
