using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAttack))]
public class Aibehave : MonoBehaviour
{
    instantproperty insprop;
    float wanderdelay;
    public bool isalive = true;
    private bool canattack = true;

    [HideInInspector]public NavMeshAgent nav;
    [HideInInspector]public GameObject target;
    [HideInInspector]public bool onattacking;
    [HideInInspector]public int indexaction = 2;
    [Space]
    public float followdistance;[HideInInspector]public float startfollowdistance;
    public float DistanceAttack;
    public float patrolRange;
    public float delayafterAttack = 3f;
    [Space]
    EnemyAttack AttackScript;

    [Header("AreaAffected")]
    public bool isAreaAffected;
    public float affectedRange;
    // Start is called before the first frame update
    void Start()
    {
        insprop = GetComponent<instantproperty>();
        startfollowdistance = followdistance;
        InvokeRepeating("updatetarget",0,1f);
        wanderdelay = Random.Range(0, 2f);
        startpos = transform.position;
        randomDirectionWander();
        AttackScript = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    float delaywanderpro;
    void Update()
    {
        if(isalive == true)
        {
            //transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 0);
            if(nav != null)
            {
                if(target != null)
                {
                    if(Vector2.Distance(transform.position, target.transform.position) > followdistance)
                    {
                        indexaction = 0; //patrol
                    }else if(Vector2.Distance(transform.position, target.transform.position) < followdistance && Vector2.Distance(transform.position, target.transform.position) > DistanceAttack)
                    {
                        followdistance = startfollowdistance;
                        indexaction = 1; //follow
                    }else if(Vector2.Distance(transform.position, target.transform.position) < DistanceAttack && Vector2.Distance(transform.position, target.transform.position) < followdistance)
                    {
                        indexaction = 2; //attack
                        nav.destination = pos;
                    }
                    

                    if(target.transform.position.x < transform.position.x)
                    {
                        transform.parent.transform.rotation = Quaternion.Euler(270, 180f, 0f);
                    }else{
                        transform.parent.transform.rotation = Quaternion.Euler(270, 0f, 0f);
                    }
                }else{
                    indexaction = 0;

                    if(pos.x < transform.position.x)
                    {
                        transform.parent.transform.rotation = Quaternion.Euler(270, 180f, 0f);
                    }else{
                        transform.parent.transform.rotation = Quaternion.Euler(270, 0f, 0f);
                    }
                }

               // Debug.Log("Action :" + " " + indexaction.ToString());
                switch(indexaction)
                {
                    case 0:
                    {
                        nav.stoppingDistance = 0.2f;
                        delaywanderpro += Time.deltaTime;
                        if(delaywanderpro >= wanderdelay)
                        {
                            randomDirectionWander();
                            wanderdelay = Random.Range(0, 2.5f);
                            delaywanderpro = 0;
                        }

                        if(AttackScript.anim != null)
                        {
                            if(Vector2.Distance(transform.parent.position, pos) <= 0.2f)
                            {
                                AttackScript.anim.SetInteger("isrun", 0);
                            }else if(Vector2.Distance(transform.parent.position, pos) > 0.2f){
                                AttackScript.anim.SetInteger("isrun", 1);
                            }
                        }
                        nav.SetDestination(pos);
                    }break;

                    case 1:
                    {
                        nav.stoppingDistance = DistanceAttack;
                        AttackScript.anim.SetInteger("isrun", 1);
                        nav.SetDestination(new Vector2(target.transform.position.x, target.transform.position.y));
                    }break;

                    case 2:
                    {
                        nav.stoppingDistance = DistanceAttack;
                        AttackScript.anim.SetInteger("isrun", 0);
                        if(canattack == true)
                        {
                            StartCoroutine(attacks(delayafterAttack));
                            canattack = false;
                        }
                    }break;
                }
            }
        }else{
            nav.isStopped = true;
        }
    }

    //Char_Animation
    #region attack_state
    IEnumerator attacks(float delay)
    {
        onattacking = true;
        nav.isStopped = true;
        if(AttackScript != null)
        {
            AttackScript.attack();
        }
        StartCoroutine(delaynextattack(delay));
        yield return null;
    }
    IEnumerator delaynextattack(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Enemyattacknext");
        canattack = true;
        yield return null;
    }
    public void endattack()
    {
        onattacking = false;
        transform.parent.GetComponent<NavMeshAgent>().isStopped = false;
    }
    #endregion

    #region patroling_state
    Vector2 pos;
    Vector2 startpos;
    void randomDirectionWander()
    {
        float randvalx = Random.Range(-patrolRange, patrolRange);
        float randvaly = Random.Range(-patrolRange, patrolRange);
        pos = new Vector2(startpos.x + randvalx, startpos.y + randvaly);
    }
    #endregion

    #region targetingmultipleplayer
    GameObject[] musuh;
    float tersingkat;
    GameObject terdekat;
    float jarakantara;
    void updatetarget()
    {
        musuh = GameObject.FindGameObjectsWithTag("Player");
        tersingkat = Mathf.Infinity;
        terdekat = null;

        foreach(GameObject enemys in musuh)
        {
            jarakantara = Vector2.Distance(transform.position, enemys.transform.position);
            if(jarakantara < tersingkat)
            {
                tersingkat = jarakantara;
                terdekat = enemys;
            }
        }

        if(terdekat != null && tersingkat <= followdistance)
        {
            target = terdekat;
            insprop.target = target;
        }else{
            target = null;
        }
    }
    #endregion

    #region AffectedArea
    public void AffectArea()
    {
        Vector3 Area = transform.position;
        Collider[] colliders = Physics.OverlapSphere(Area, affectedRange);
        foreach (Collider hit in colliders)
        {
            if(hit.transform.tag == "Enemy")
            {
                StartCoroutine(hit.GetComponent<Char_Utama>()._Char_Status.ab.ECollaborateArea());
            }
        }
    }

    public IEnumerator ECollaborateArea()
    {
        if(isalive == true)
        {
            followdistance = affectedRange;
            updatetarget();
            if(target != null)
            {
                yield return new WaitUntil(() => Vector2.Distance(transform.position, target.transform.position) < DistanceAttack);
                followdistance = startfollowdistance;
            }
            yield return null;
        }
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceAttack);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followdistance);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, affectedRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startpos, patrolRange);
    }
}
