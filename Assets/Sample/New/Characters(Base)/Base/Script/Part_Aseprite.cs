using UnityEngine;

public class Part_Aseprite : MonoBehaviour
{
    #region Char_Data_Variant_Attack

    [SerializeField] private GameObject Static_Part;

    [SerializeField] private GameObject Dynamic_Part;

    public void On_Change_Part(string v)
    {
        if (v == "Static")
        {
            Static_Part.gameObject.SetActive(true);
            Dynamic_Part.gameObject.SetActive(false);
        }
        else if (v == "Dynamic")
        {
            Static_Part.gameObject.SetActive(false);
            Dynamic_Part.gameObject.SetActive(true);
        }
    }

    public void Off_Part()
    {
        Static_Part.gameObject.SetActive(false);
        Dynamic_Part.gameObject.SetActive(false);
    }

    public void On_Part()
    {
        Static_Part.gameObject.SetActive(true);
        Dynamic_Part.gameObject.SetActive(false);
    }

    #endregion
}