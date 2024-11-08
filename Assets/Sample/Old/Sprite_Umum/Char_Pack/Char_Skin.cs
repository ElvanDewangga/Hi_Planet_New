using UnityEngine;

public class Char_Skin : MonoBehaviour
{
    public string Char_Skin_Code = "";
    public Transform[] A_Part;

    // A_Aseprite
    public void Off_Char_Skin()
    {
        foreach (var s in A_Part) s.gameObject.SetActive(false);
    }

    public void On_Char_Skin()
    {
        foreach (var s in A_Part) s.gameObject.SetActive(true);
    }
}