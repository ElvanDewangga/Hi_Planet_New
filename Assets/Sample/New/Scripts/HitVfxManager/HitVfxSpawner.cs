using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVfxSpawner : MonoBehaviour {
    // CharBase
    public void SetHitVfx(GameObject from, GameObject to, Config_Char_Hit config)
    {
        
            GameObject Ins = GameObject.Instantiate(config.Hit_Vfx);
            
            if (config)
            {
                Ins.transform.localScale = config.V3_Scale;
                Ins.transform.position = to.gameObject.transform.position + config.V3_Position;
                // Ins.GetComponent<SelfDestruct>().On_Set_Time(config.Destroy_Duration);
                StartCoroutine (DestroyNumerator (config.Destroy_Duration));
                foreach (Transform Ts in Ins.transform)
                Ts.transform.localScale = config.V3_Scale_Child;
                On_Set_Object_Variant_Config_Handler(config, from, to, Ins);
                Ins.SetActive(true);
                Ins.transform.SetParent (this.transform);
            } else {
                Debug.LogError ("No Hit Vfx Detected.");
            }
            
    }

    IEnumerator DestroyNumerator (float Time) {
        yield return new WaitForSeconds (Time);
        Destroy (this.gameObject);
    }

    private void On_Set_Object_Variant_Config_Handler(Config_Char_Hit config, GameObject from,
        GameObject to, GameObject Vfx_Go)
    {
        if (config.b_Light_Setup) On_Set_b_Light_Setup(config, to, Vfx_Go);
    }

    [SerializeField] private Logic_Light_Setup _Logic_Light_Setup;

    private void On_Set_b_Light_Setup(Config_Char_Hit config, GameObject GO_To, GameObject Vfx_Go)
    {
        if (!Vfx_Go) Debug.LogError("Vfx_Go");
        if (!GO_To) Debug.LogError("GO_To");
        _Logic_Light_Setup.ApplySetup(GO_To.transform, Vfx_Go.transform, config._Config_Light_Setup);
    }
}
