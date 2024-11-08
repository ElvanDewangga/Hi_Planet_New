using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Char_AI_Follow : Char_AI_Event {
   [SerializeField]
   Char_AI _Char_AI;
   public override void Play () {
    base.Play ();
    targetObject = _Char_AI._Char_AI_Zone_Target.On_Get_Target_Object ().transform;
    //agent.isStopped = false;
   }

   public override void Stop () {
    base.Stop ();
    targetObject = null;
    agent.isStopped = true;
   }


   Transform targetObject; // Target player

    private NavMeshAgent agent;

    void Start()
    {
        agent = _Char_AI._Char_Utama.gameObject.GetComponent <NavMeshAgent> ();
    }

    void Update()
    {
        if (b_Play) {
            if (targetObject != null)
            {
                // Set the destination to the player's position
                agent.SetDestination(targetObject.position);

                if (targetObject.position.x < _Char_AI._Char_Utama.gameObject.transform.position.x) {
                    if (_Char_AI._Char_Utama._Char_Direction_2d.Code_Direction_2d != "Left") {
                        _Char_AI._Char_Utama._Char_Utama_Source.On_Char_Direction_2d ("Left");
                    }
                } else {
                    if (_Char_AI._Char_Utama._Char_Direction_2d.Code_Direction_2d != "Right") {
                        _Char_AI._Char_Utama._Char_Utama_Source.On_Char_Direction_2d ("Right");
                    }
                }
            }
        }
    }
}
