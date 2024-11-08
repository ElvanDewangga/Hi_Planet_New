using UnityEngine;
using TMPro;

public class Text_Follow_Value : MonoBehaviour
{
    [SerializeField]
    TMP_Text experienceText; // TMP text untuk menampilkan pengalaman
    public int Cur_Exp = 0; // Pengalaman saat ini
    public int Max_Exp = 100; // Pengalaman maksimum
    public float duration = 0.5f; // Durasi waktu yang diinginkan untuk mencapai target pengalaman
    private float displayedExp = 0f; // Pengalaman yang ditampilkan di UI
    public float speedMultiplier = 5f; // Pengali kecepatan untuk mempercepat atau memperlambat animasi

    void Start()
    {
        displayedExp = Cur_Exp; // Inisialisasi nilai yang ditampilkan sama dengan pengalaman saat ini
        UpdateExperienceText();
    }

    public void On_Active (int Cur_Exp_V, int Max_Exp_V) {
        if (Cur_Exp_V < displayedExp) {
            displayedExp = 0;
        }
        Cur_Exp = Cur_Exp_V; Max_Exp = Max_Exp_V;
        b_Active = true;
        UpdateExperienceText ();
    }

    bool b_Active = false;
    void Update()
    {
        if (b_Active) {
            // Cek apakah nilai pengalaman yang ditampilkan belum mencapai nilai pengalaman saat ini
            if (displayedExp < Cur_Exp)
            {
                // Menghitung kecepatan peningkatan pengalaman dengan pengali kecepatan
                float speed = Mathf.Abs(Cur_Exp - displayedExp) / duration * speedMultiplier;
                
                // Mengubah nilai displayedExp secara bertahap menuju Cur_Exp
                displayedExp = Mathf.MoveTowards(displayedExp, Cur_Exp, speed * Time.deltaTime);
                
                // Memperbarui teks pengalaman di UI
                UpdateExperienceText();
            } else {
                b_Active = false;
            }
        }
    }

    void UpdateExperienceText()
    {
        experienceText.text = Mathf.RoundToInt(displayedExp).ToString() + " / " + Max_Exp.ToString();

    }

    // Metode ini bisa dipanggil dari luar untuk menambahkan pengalaman
    public void AddExperience(int amount)
    {
        Cur_Exp += amount;
        if (Cur_Exp > Max_Exp)
        {
            Cur_Exp = Max_Exp; // Pastikan Cur_Exp tidak melebihi Max_Exp
        }
    }
}

