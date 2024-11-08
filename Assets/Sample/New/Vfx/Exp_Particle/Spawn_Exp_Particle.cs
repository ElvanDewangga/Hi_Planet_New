using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pasangkan ini ke musuh :
public class Spawn_Exp_Particle : MonoBehaviour
{
    public int expParticleCount = 5;      // Jumlah partikel exp yang akan muncul
    public float spawnRadius = 2f;        // Radius spawn partikel di sekitar musuh
    public int Cur_Exp_Point = 3;
    // Method ini dipanggil saat musuh mati
    #region Char_Status
    public void On_Spawn_Exp()
    {
        SpawnExpParticles();
        Destroy(gameObject);  // Menghapus musuh dari scene setelah mati
    }
    #endregion
    [SerializeField]
    GameObject expParticlePrefab;

    private void SpawnExpParticles()
    {
        for (int i = 0; i < expParticleCount; i++)
        {
            // Tentukan posisi spawn random di sekitar musuh
            Vector3 spawnOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);

            // Instantiate prefab partikel exp di posisi spawn
            GameObject expParticle = Instantiate(expParticlePrefab, spawnPosition, Quaternion.identity);
            expParticle.transform.localScale = new Vector3 (1,1,1);
            // Pastikan script ExpParticle menerima referensi pemain
            Exp_Particle expScript = expParticle.GetComponent<Exp_Particle>();
            expScript.On_Cur_Exp (Cur_Exp_Point);
        }
    }
}
