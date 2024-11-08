using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour {
   #region Enemy Spawner
    public GameObject enemyPrefab;
    public Transform enemyGroup;
    void Start () {
        foreach (Transform enemyspawn in enemyGroup) {
            Char_Spawn charSpawner = enemyspawn.GetComponent<Char_Spawn>();
            SpawnEnemy (charSpawner);
        }
    }

    public void SpawnEnemy(Char_Spawn _Char_Spawn)
    {
        // Debug.Log ("Char Data");

        var Ins = Instantiate(enemyPrefab);
        Ins.name = "Clone_Enemy";
        Ins.transform.position = _Char_Spawn.gameObject.transform.position;
        Ins.gameObject.SetActive(true);
        Ins.gameObject.SetActive(false);
        Ins.gameObject.SetActive(true);
        Char_Utama Cu = Ins.GetComponent<Char_Utama>();
        Cu.On_Set_Owner("Enemy");
        On_Set_Char_Pack(Cu, _Char_Spawn.Code_Char_Pack);
        Ins.tag = "Enemy";
        Cu._Char_Status.On_Get_Data_From_Char_Utama(0, 0, 0, 0, 0);
        Ins.transform.SetParent (this.transform);
        setEnemyIndiv(_Char_Spawn, Ins.GetComponent<Char_Utama>(), Ins);
    }

    private void setEnemyIndiv(Char_Spawn spawnindiv, Char_Utama Cuindiv, GameObject ins)
    {
        var insindiv = Instantiate(spawnindiv.transform.GetChild(0).gameObject, ins.transform);
        insindiv.transform.localPosition = Vector3.zero;
        var enemynav = ins.GetComponent<NavMeshAgent>();
        if (insindiv.TryGetComponent(out Aibehave ab))
        {
            ab.nav = enemynav;
            enemynav.stoppingDistance = ab.DistanceAttack;
        }

        if (insindiv.TryGetComponent(out MaceBotBossAI mbbai))
        {
            mbbai.nav = enemynav;
            mbbai.GetStartPropety();
            mbbai.maceattack.getCu();
            //enemynav.stoppingDistance = mbbai.DistanceAttack;
        }

        enemynav.enabled = true;
        enemynav.speed = Cuindiv._Char_Status.Speed;
    }
   #endregion
   #region Char_Spawn
   [SerializeField] private Transform charPacks;
     public void On_Set_Char_Pack(Char_Utama _Char_Utama, string Char_Pack_Name)
    {
        foreach (Transform t in charPacks)
        {
            var Cp = t.GetComponent<Char_Pack>();
            if (Cp.name == Char_Pack_Name)
            {
                _Char_Utama.On_Set_Char_Body_Component(Cp);
                return;
            }
        }
    }
   #endregion
}
