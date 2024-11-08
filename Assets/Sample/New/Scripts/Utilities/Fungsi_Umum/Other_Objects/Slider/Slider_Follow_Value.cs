using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slider_Follow_Value : MonoBehaviour
{
    public TMP_Text sliderValueText; // TMP text untuk menampilkan nilai slider
    public Slider slider; // Slider yang akan diubah nilainya
    public int x = 0; // Nilai target yang ingin dicapai slider
    public float duration = 2f; // Durasi untuk mencapai nilai target
    private float currentValue = 0f; // Nilai saat ini dari slider
    private float elapsedTime = 0f; // Waktu yang sudah berlalu

    void Start()
    {
        slider.value = currentValue;
        UpdateSliderText();
    }

    public void On_Active (int Cur_Exp_V) {
        if (Cur_Exp_V < currentValue) {
            currentValue =0;
        }
        x = Cur_Exp_V;
        b_Active = true;
    }

    bool b_Active = false;
    void Update()
    {
        if (b_Active) {
        // Cek apakah nilai slider belum mencapai nilai target dan hanya tambahkan nilai
        if (currentValue < x)
        {
            elapsedTime += Time.deltaTime;

            // Menghitung kecepatan peningkatan berdasarkan perbedaan antara nilai target dan nilai saat ini
            float speed = Mathf.Abs(x - currentValue) / duration;
            
            // Lerp untuk mendapatkan nilai slider yang smooth
            currentValue = Mathf.MoveTowards(currentValue, x, speed * Time.deltaTime);
            slider.value = currentValue;

            UpdateSliderText();
        }
        else
        {
            elapsedTime = 0f; // Reset waktu setelah mencapai target
            b_Active = false;
        }
        }
    }

    void UpdateSliderText()
    {
        // sliderValueText.text = Mathf.RoundToInt(slider.value).ToString();
    }
}
