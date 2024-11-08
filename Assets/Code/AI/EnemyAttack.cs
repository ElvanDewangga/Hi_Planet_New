using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    Aibehave ab;
    Char_Utama cu;
    [HideInInspector]public Animator anim;

    enum AttackMode{MoveAttack, ShootAttack, TriggerAttack};
    [SerializeField] AttackMode _AttackMode;

    //public GameObject _enemyAttackVFX;
    void Start()
    //Attack_1
    {
        Invoke("getCu", 0.2f);
    }
    void getCu()
    {
        cu = transform.parent.GetComponent<Char_Utama>();
        ab = GetComponent<Aibehave>();
        if(cu != null)
        {
            anim = cu._Char_Animation.GetComponent<Char_Animation>().animator;
        }
    }

    public void attack()
    {
        if(_AttackMode == AttackMode.MoveAttack)
        {
            if(ab.target != null)
            {
                StartCoroutine(makeamove());
                transform.parent.LeanMove(transform.parent.position + (-transform.parent.right * 5f), 0.5f).setEase(LeanTweenType.easeOutSine);
            }
        }else if(_AttackMode == AttackMode.ShootAttack)
        {
            Debug.Log("Enemyattacknextshoot");
            anim.SetTrigger("Attack_1");
            StartCoroutine(waitanimationfinish());
        }else if(_AttackMode == AttackMode.TriggerAttack)
        {
            anim.SetTrigger("Attack_1");
            StartCoroutine(waitanimationfinish());
        }
    }
    
    IEnumerator makeamove()
    {
        yield return new WaitForSeconds(0.5f);
        attackVFX();
        Vector3 dir = (ab.target.transform.position - transform.parent.transform.position).normalized;
        dir.z = 0;

        anim.SetTrigger("Attack_1");
        transform.parent.LeanMove(transform.parent.position + dir * 20f, 0.5f);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("b_idle", true);
        ab.endattack();
        yield return null;
    }

    IEnumerator waitanimationfinish()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        anim.SetBool("b_idle", true);
        yield return new WaitForSeconds(1f);
        ab.endattack();
        yield return null;;
    }

    //public Vector3 offset;
    Char_AI_Play_Attack_Shoot C_AI_Shoot;
    [SerializeField] GameObject enemyCharBasePrefab;
    EnemyCharBase enemyCharBaseSample;
    public void attackVFX()
    {
        /* Latest
        C_AI_Shoot = cu._Char_AI._Char_AI_Attack.A_Char_AI_Play_Attack.transform.GetChild(0).GetComponent<Char_AI_Play_Attack_Shoot>();
        if(cu._Char_Animation.transform.GetChild(1).TryGetComponent(out Char_Technique ct))
        {
            C_AI_Shoot._Char_Technique = ct;
        }
        C_AI_Shoot.playVFX();
        */

            Debug.Log ("Enemy attack Vfx");
        if (!enemyCharBaseSample) {
            GameObject cloneSample = GameObject.Instantiate (enemyCharBasePrefab.gameObject);
            cloneSample.transform.position = this.transform.position;
            cloneSample.transform.SetParent (this.transform);
            enemyCharBaseSample = cloneSample.GetComponent <EnemyCharBase> ();
            enemyCharBaseSample.gameObject.SetActive (true);
        }
        enemyCharBaseSample.AIPlaySkill ();
    }
}
