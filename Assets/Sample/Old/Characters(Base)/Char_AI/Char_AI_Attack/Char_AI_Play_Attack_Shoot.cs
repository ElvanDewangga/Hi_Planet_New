using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

 /* Type 2
            // Cek jika agent sudah mencapai posisi target
            if (!b_Attack && !agent.pathPending && agent.remainingDistance <= stopDistance)
            {
                // Musuh sudah sampai, memanggil metode attack
                On_Attack();
            }
            */

public class Char_AI_Play_Attack_Shoot : Char_AI_Play_Attack {
    
    public override void On_Set_Char_Technique (Char_Technique Ct) {
        base.On_Set_Char_Technique (Ct);
   }

    public override void Play () {
        base.Play ();
       
        On_Get_Position ();
        agent.isStopped = false;
        Debug.Log ("Attack 1.2");
    }

    public override void Stop () {
        base.Stop ();
        agent.isStopped = true;
    }
    
    public override void On_Get_Position () {
        base.On_Get_Position ();
        agent = _Char_AI._Char_Utama.GetComponent<NavMeshAgent>();
        player = _Char_AI._Char_AI_Zone_Target.On_Get_Target_Object ().transform;
        Target_Rot_Object = _Char_AI._Char_Utama._Char_Animation._A_Aseprite.gameObject.transform;
        Real_Attack_Center_Offset = attackCenterOffset;
        On_Get_Posisi_Lurus ();
    }

    public override void On_Change_Direction_2d (string v) {
      base.On_Change_Direction_2d (v);
    }
    
    public override void On_Attack () {
        base.On_Attack ();

        
    }

    public NavMeshAgent agent; 
    public Transform player; 
    public Vector3 boxSize = new Vector3(2f, 2f, 2f);
   //  public Vector3 attackCenterOffset = Vector3.zero; // pindah ke Char_AI_Play_Attack
    public float stopDistance = 0.5f;
    public Color gizmoColor = Color.green;

    // Tambahkan field Transform untuk referensi objek rotasi
    [HideInInspector]
    public Transform Target_Rot_Object;

    private void Start() {}

    public void On_Get_Posisi_Lurus() {
        _Char_AI._Char_AI_Follow.Play ();
    }

    void On_Check_Attack () {
        if (!b_Attack) {
            Vector3 attackCenter = transform.position + attackCenterOffset;

            if (IsPositionInRange(player.position, attackCenter)) {
                On_Attack();
            } else {
                Vector3 targetPosition = player.transform.position;
                agent.SetDestination(targetPosition);
            }
        }
    }

    // Menggambar area serangan berbentuk kotak di Scene View
    private void OnDrawGizmos() {
        Gizmos.color = gizmoColor;

        // Mendapatkan pusat area serangan
        // Vector3 attackCenter = transform.position + attackCenterOffset;
        Vector3 attackCenter = transform.position + attackCenterOffset;
       
        // Jika ada objek rotasi, gunakan rotasi objek tersebut
        Quaternion rotation = Target_Rot_Object != null ? Target_Rot_Object.rotation : Quaternion.identity;

        // Gambar kotak dengan rotasi
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(attackCenter, rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    private bool IsPositionInRange(Vector3 position, Vector3 attackCenter) {
        Vector3 difference = position - attackCenter;
        return Mathf.Abs(difference.x) <= boxSize.x / 2 &&
               Mathf.Abs(difference.y) <= boxSize.y / 2 &&
               Mathf.Abs(difference.z) <= boxSize.z / 2;
    }

    private void Update() {
        if (b_Play) {
            if (agent != null) {
                On_Check_Attack ();
            }
        }
    }
   
}
