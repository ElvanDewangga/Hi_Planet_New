using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Visual_Novel_Dialog_Fade : MonoBehaviour {
    public TMP_Text textComponent; // Komponen TextMeshPro
    public Image background; // Latar belakang
    public float fadeDuration = 1.0f; // Durasi fade in latar belakang
    public float charDisplayInterval = 0.05f; // Interval waktu antara huruf
    public float charFadeDuration = 1.0f; // Durasi fade in untuk setiap karakter
    private bool isTextFadeComplete = false;
    private string fullText;

    public void On_Start()
    {
        if ( !IsTextFadeComplete () && fullText != null) {
         On_Direct_Show_All_Text ();
        }
        isTextFadeComplete = false;
        fullText = textComponent.text;
        textComponent.text = ""; // Kosongkan teks pada awalnya
        //  Color bgColor = textComponent.color;
        //bgColor.a = 0;
        //textComponent.color = bgColor;
      
       // background.color = bgColor;
      //  StartCoroutine(FadeInBackground());
       C_DisplayText = StartCoroutine(DisplayText());
    }

    IEnumerator FadeInBackground()
    {
        float elapsedTime = 0;
        Color bgColor = background.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            bgColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            background.color = bgColor;
            yield return null;
        }

        bgColor.a = 1;
        background.color = bgColor;
       // StartCoroutine(DisplayText());
    }
    Coroutine C_DisplayText;
    IEnumerator DisplayText()
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;
            for (int i = 0; i < fullText.Length; i++)
            {
                textComponent.text += fullText[i];
                textComponent.ForceMeshUpdate();
                StartCoroutine(FadeInCharacter(i));
            yield return new WaitForSeconds(charDisplayInterval);
            }

        // Menandai bahwa animasi fade-in teks selesai
        isTextFadeComplete = true;
    }

    IEnumerator FadeInCharacter(int index)
    {
   
        TMP_TextInfo textInfo = textComponent.textInfo;
        Color32[] newVertexColors;
        Color32 c = textComponent.color;
        int currentCharacter = index;

        float alpha = 0;
        float fadeStep = 1.0f / charFadeDuration;
        
        while (alpha < 1.0f)
        {
            alpha += fadeStep * Time.deltaTime;
            byte alphaByte = (byte)(Mathf.Clamp01(alpha) * 255);

            if (textInfo.characterCount > currentCharacter)
            {
                var charInfo = textInfo.characterInfo[currentCharacter];
                if (!charInfo.isVisible) continue;

                int vertexIndex = charInfo.vertexIndex;
                newVertexColors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;

                newVertexColors[vertexIndex + 0].a = alphaByte;
                newVertexColors[vertexIndex + 1].a = alphaByte;
                newVertexColors[vertexIndex + 2].a = alphaByte;
                newVertexColors[vertexIndex + 3].a = alphaByte;

                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

            yield return null;
        } 
    }

    public bool IsTextFadeComplete()
    {
        return isTextFadeComplete;
    }

    // Visual_Novel_System :
    public void On_Direct_Show_All_Text () {
        if (C_DisplayText != null) {
            StopCoroutine (C_DisplayText);
        }
        textComponent.text = fullText;
        isTextFadeComplete = true;
    }
}
