using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Char_Body_Component : MonoBehaviour
{
    [SerializeField] private Char_Utama _Char_Utama;

    [SerializeField] private C_Body_Component[] A_C_Body_Component = new C_Body_Component [0];

    #region Char_Technique

    public GameObject Get_Component_Object(string Component_Name)
    {
        GameObject In = null;
        foreach (var Cs in A_C_Body_Component)
            if (Cs.Component_Name == Component_Name)
                In = Cs.Component_Object;
        return In;
    }

    #endregion

    #region Char_Accesories

    public Char_Utama On_Get_Char_Utama()
    {
        return _Char_Utama;
    }

    #endregion

    [Serializable]
    public class C_Body_Component
    {
        public string Component_Name; // "Body", "Clone", "Hand_Left", "Hand_Right"
        public GameObject Component_Object;
    }

    #region Char_Utama

    // Char_Body_Component :
    public void On_Set_Char_Body_Component(string Code_Set, Sprite Sprite)
    {
        foreach (var s in A_C_Body_Component)
            if (s.Component_Name == Code_Set)
            {
                var Sp = s.Component_Object.GetComponent<SpriteRenderer>();
                Sp.sprite = Sprite;
            }
    }

    public void On_Diactive_All_Component()
    {
        foreach (var s in A_C_Body_Component) s.Component_Object.SetActive(false);
    }

    #endregion

    #region Char_Equipment

    [SerializeField] private Char_Accesories[] A_Char_Accesories;

    // Char_Equipment
    public void On_Set_Char_Accesories(Data_Item_Input From)
    {
        foreach (var s in A_Char_Accesories)
            if (s.Code_Accesories == From._Item_Input.Type)
            {
                s.On_Set_Char_Accesories(From);
                return;
            }
    }

    public void On_Unequip_Char_Accesories(string Type)
    {
        Debug.Log(Type);
        foreach (var s in A_Char_Accesories)
            if (s.Code_Accesories == Type)
            {
                Debug.Log("Type");
                s.On_Destroy_Clone_In_A_Sample();
                return;
            }
    }

    #region Canvas

    [SerializeField] private Canvas Upper_Canvas;

    [SerializeField] private TMP_Text Username_Text;

    [SerializeField] private Canvas Down_Canvas;

    [SerializeField] private Slider Energy_Slider;

    [SerializeField] private Image Energy_Slider_Fill;


    private readonly Color32 Color_Blue = new(92, 97, 255, 255);
    private readonly Color32 Color_Red = new(230, 40, 46, 255);

    private void On_Set_Upper_Canvas()
    {
        Upper_Canvas.gameObject.SetActive(true);
    }

    private void Off_Set_Upper_Canvas()
    {
        Upper_Canvas.gameObject.SetActive(false);
    }

    private void On_Set_Down_Canvas()
    {
        Down_Canvas.gameObject.SetActive(true);
    }

    private void Off_Set_Down_Canvas()
    {
        Down_Canvas.gameObject.SetActive(false);
    }

    public void On_Set_Canvas()
    {
        if (_Char_Utama.Owner == "Player")
        {
            On_Set_Upper_Canvas();
            On_Set_Down_Canvas();
            Energy_Slider_Fill.color = Color_Blue;
            Energy_Slider.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_Char_Utama.Owner == "Enemy")
        {
            Off_Set_Upper_Canvas();
            On_Set_Down_Canvas();
            Energy_Slider_Fill.color = Color_Red;
            Energy_Slider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    #endregion

    #endregion

    #region Char_Status

    public void On_Set_Username(string Username)
    {
        Username_Text.text = Username;
    }

    public void On_Set_Cur_Hp(int Cur_Hp)
    {
        Energy_Slider.value = Cur_Hp;
    }

    public void On_Set_Max_Hp(int Max_Hp)
    {
        Energy_Slider.maxValue = Max_Hp;
    }

    public void Off_Set_All_Canvas()
    {
        Off_Set_Down_Canvas();
        Off_Set_Upper_Canvas();
    }

    #endregion
}