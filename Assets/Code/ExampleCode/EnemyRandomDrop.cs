using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomDrop : MonoBehaviour
{
    public GameObject[] _dropMaterials;
    public int itemcounts = 1;
    public int getreversepercentage = 4;
    public float rad = 0;

    GameObject ins;
    public void dropRandom()
    {
        int dropCount = Random.Range(0, 10);
        for(int i = 0;i < itemcounts;i++)
        {
            if(getreversepercentage == 0)
            {
                int randomget = Random.Range(0, _dropMaterials.Length);
                Vector3 randomdir = transform.position + Random.insideUnitSphere * rad;
                GameObject ins = Instantiate(_dropMaterials[randomget], transform.position, Quaternion.identity);
                ins.transform.LeanMove(randomdir, 0.6f).setEase(LeanTweenType.easeOutQuart);
            }else{
                if(dropCount > getreversepercentage)
                {
                    int randomget = Random.Range(0, _dropMaterials.Length);
                    Vector3 randomdir = transform.position + Random.insideUnitSphere * rad;
                    GameObject ins = Instantiate(_dropMaterials[randomget], transform.position, Quaternion.identity);
                    ins.transform.LeanMove(randomdir, 0.6f).setEase(LeanTweenType.easeOutQuart);
                }
            }
        }
    }

    // void OnDestroy()
    // {
    //     ds.countDefeated++;
    //     ds.CheckTargetDefeatedReach();
    // }
}
