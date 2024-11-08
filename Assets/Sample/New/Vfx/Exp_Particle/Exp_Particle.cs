using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Particle : MonoBehaviour
{
    public float spawnRadius = 2f;         // Radius spawn partikel di sekitar musuh
    public float moveAwayDistance = 1f;    // Jarak yang ditempuh saat bergerak menjauh secara acak
    public float moveSpeed = 3f;           // Kecepatan bergerak mendekati pemain
    public float attractDistance = 5f;     // Radius untuk mulai mendekat ke pemain
    
    private Vector3 randomDirection;       // Arah acak untuk gerakan awal
    private bool isAttracted = false;      // Apakah partikel tertarik ke pemain

     int Cur_Exp = 3;
    #region Spawn_Exp_Particle
    public void On_Cur_Exp (int Cur_Exp_V) {
        Cur_Exp = Cur_Exp_V;
        this.gameObject.SetActive (true);
    }
    #endregion
    void Start()
    {
        // Tentukan posisi awal partikel exp di sekitar musuh
        Vector3 spawnOffset = Random.insideUnitCircle * spawnRadius;
        transform.position += new Vector3(spawnOffset.x, spawnOffset.y, 0);

        // Tentukan arah random untuk gerakan menjauh
        randomDirection = Random.insideUnitSphere.normalized * moveAwayDistance;
       // Latest Char_Data.Ins.Spawn_System_Parenting (this.gameObject);
       this.gameObject.transform.SetParent (this.transform, false);
       // this.gameObject.SetActive (true);
        // Mulai gerakan awal secara acak menjauh
        StartCoroutine(MoveAway());
    }

    void Update()
    {
        if (AccountManager.instance.player) {
            if (isAttracted)
            {
                // Gerakan mendekat ke pemain jika dalam radius yang ditentukan
                transform.position = Vector3.MoveTowards(transform.position, AccountManager.instance.player.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                // Periksa apakah pemain berada dalam jarak attractDistance
                float distanceToPlayer = Vector3.Distance(transform.position, AccountManager.instance.player.transform.position);
                if (distanceToPlayer < attractDistance)
                {
                    isAttracted = true;
                }
            }
        }
    }

    private IEnumerator MoveAway()
    {
        // Gerakan awal menjauh secara acak untuk beberapa waktu
        float moveDuration = 0.5f;
        float elapsedTime = 0;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + randomDirection;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter (Collider Col) {
        if (Col.gameObject.tag == "Player") {
            if (AccountManager.instance.player.gameObject == Col.gameObject) {
                Col.gameObject.GetComponent <PlayerCharBase> ().IncreaseExp (Cur_Exp);
                Destroy (this.gameObject);
            }
        }
    }
}

