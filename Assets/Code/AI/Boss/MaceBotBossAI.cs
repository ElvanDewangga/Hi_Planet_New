using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MaceBotAttack))]
[RequireComponent(typeof(BossProperty))]
public class MaceBotBossAI : MonoBehaviour
{
    instantproperty insprop;
    [HideInInspector]public bool isalive = true;
    [HideInInspector]public NavMeshAgent nav;
    [HideInInspector]public GameObject target;
    public bool canattack;

    [HideInInspector]public int indexaction = 2;
    [HideInInspector]public MaceBotAttack maceattack;
    
    [Header("RangeDetect")]
    public float Distance_attack1;
    public float Distance_attack2;
    public float Distance_attack3;
    [Space]
    public float patrolRange;
    
    // Start is called before the first frame update
    void Start()
    {
        insprop = GetComponent<instantproperty>();
        InvokeRepeating("updatetarget",0,1f);
        startpos = transform.position;
    }
    public void GetStartPropety()
    {
        maceattack = GetComponent<MaceBotAttack>();
        if(nav != null)
        {
            BossDungeonManager.instance._BossScript = this;
        }
    }

    public void activeboss()
    {
        isalive = true;
        randomDirectionWander();
    }

    // Update is called once per frame
    void Update()
    {
        if(isalive == true)
        {
            transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 0);
            if(nav != null)
            {
                if(target != null)
                {
                    if(Vector2.Distance(transform.position, target.transform.position) < Distance_attack1)
                    {
                        maceattack.attackindex = 0;
                        indexaction = 0;
                    }else if(Vector2.Distance(transform.position, target.transform.position) < Distance_attack2 && Vector2.Distance(transform.position, target.transform.position) > Distance_attack1)
                    {
                        maceattack.attackindex = 1;
                        indexaction = 0;
                    }else if(Vector2.Distance(transform.position, target.transform.position) < Distance_attack3 && Vector2.Distance(transform.position, target.transform.position) > Distance_attack2)
                    {
                        maceattack.attackindex = 2;
                        indexaction = 0;
                    }

                    if(target.transform.position.x < transform.position.x)
                    {
                        transform.parent.transform.rotation = Quaternion.Euler(270, 180f, 0f);
                    }else{
                        transform.parent.transform.rotation = Quaternion.Euler(270, 0f, 0f);
                    }
                }

                switch(indexaction)
                {
                    case 0:
                    {
                        if(canattack == true)
                        {
                            StartCoroutine(attacks());
                            canattack = false;
                        }
                    }break;

                    case 1:
                    {
                        nav.isStopped = true;
                    }break;
                }
            }
        }else{
            if(nav != null)
            {
                nav.isStopped = true;
            }
        }
    }

    //Char_Animation
    #region attack_state
    IEnumerator attacks()
    {
        nav.isStopped = true;
        maceattack.attack();
        StartCoroutine(delaynextattack());
        yield return null;
    }
    IEnumerator delaynextattack()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Enemyattacknext");
        canattack = true;
        yield return null;
    }
    #endregion

    #region patroling_state
    IEnumerator PatrolAreaAttack()
    {
        StartCoroutine(PatrolReachDestination());
        yield return new WaitForSeconds(1);
        while(Vector2.Distance(transform.position, pos) > 0.1f)
        {
            if(nav != null)
            {
                nav.SetDestination(pos);
            }
            yield return null;
        }
        yield return null;
    }
    IEnumerator PatrolReachDestination()
    {
        yield return new WaitUntil(() => Vector2.Distance(transform.position, pos) < 0.2f);
        randomDirectionWander();
        yield return null;
    }

    Vector2 pos;
    Vector2 startpos;
    void randomDirectionWander()
    {
        float randvalx = Random.Range(-patrolRange, patrolRange);
        float randvaly = Random.Range(-patrolRange, patrolRange);
        pos = new Vector2(startpos.x + randvalx, startpos.y + randvaly);
        StartCoroutine(PatrolAreaAttack());
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

        if(terdekat != null && tersingkat <= 100)
        {
            target = terdekat;
            insprop.target = target;
        }else{
            target = null;
        }
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Distance_attack1);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Distance_attack2);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Distance_attack3);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startpos, patrolRange);
    }
}
