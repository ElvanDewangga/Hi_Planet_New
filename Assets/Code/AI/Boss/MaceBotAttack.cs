using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MaceBotAttack : MonoBehaviour
{
    MaceBotBossAI macebotai;
    Char_Utama cu;
    [HideInInspector]public Animator anim;
    [HideInInspector]public int attackindex;

    public void getCu()
    {
        cu = transform.parent.GetComponent<Char_Utama>();
        macebotai = GetComponent<MaceBotBossAI>();
        anim = cu._Char_Animation.GetComponent<Char_Animation>().animator;
    }

    public void attack()
    {
        switch(attackindex)
        {
            case 0:
            {
                anim.SetInteger("Attack", 2);
                StartCoroutine(waitanimationfinish(0));
            }break;

            case 1:
            {
                anim.SetInteger("Attack", 3);
                StartCoroutine(waitanimationfinish(0.5f));
            }break;

            case 2:
            {
                attackVFX();
                EnableNav();
            }break;
        }
    }
    
    #region Component
    IEnumerator waitanimationfinish(float delay)
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        yield return new WaitForSeconds(delay);
        anim.SetBool("b_idle", true);
        anim.SetInteger("Attack", 0);
        yield return new WaitForSeconds(1f);
        EnableNav();
        yield return null;;
    }
    void EnableNav()
    {
        transform.parent.GetComponent<NavMeshAgent>().isStopped = false;
    }

    //public Vector3 offset;
    Char_AI_Play_Attack_Shoot C_AI_Shoot;
    public void attackVFX()
    {
        C_AI_Shoot = cu._Char_AI._Char_AI_Attack.A_Char_AI_Play_Attack.transform.GetChild(0).GetComponent<Char_AI_Play_Attack_Shoot>();
        if(cu._Char_Animation.transform.GetChild(1).TryGetComponent(out Char_Technique ct))
        {
            C_AI_Shoot._Char_Technique = ct;
        }
        C_AI_Shoot.playVFX();
    }
    #endregion
}
