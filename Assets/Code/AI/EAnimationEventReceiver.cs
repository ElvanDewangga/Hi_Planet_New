using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAnimationEventReceiver : MonoBehaviour
{
    EnemyAttack indiscript;
    public void EnemyAttackVFX()
    {
        indiscript = transform.parent.transform.parent.GetChild(transform.parent.transform.parent.childCount - 1).GetComponent<EnemyAttack>();
        indiscript.attackVFX();
    }
}
