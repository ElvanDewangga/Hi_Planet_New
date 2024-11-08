using System;
using UnityEngine;

public class Char_Pack : MonoBehaviour
{
    [SerializeField] private string Name = "";

    // Char_Status :
    [SerializeField] public int Life, Attack, Defense, Speed, Intelligence, ExpDrop;

    [SerializeField] public string Type;

    [SerializeField] public string Element;

    [SerializeField] public Sprite Sprite_Umum;

    [SerializeField] public Sprite Sprite_Hand_Left;

    [SerializeField] public Sprite Sprite_Hand_Right;

    public A_Aseprite A_Aseprite_Sample;
    /*
    [SerializeField]
    Basic_Attack _Basic_Attack;

    [SerializeField]
    Special_Attack [] A_Special_Attack;
    */

    public Char_Pack_Technique _Char_Pack_Technique;
    public Char_Pack_Technique _Char_Pack_Technique_Skill;
    public Char_Pack_Accesories _Char_Pack_Accesories; // Char_Accesories

    public C_Animation[] A_C_Animation;

    [Serializable]
    public class C_Animation
    {
        public string Name;
        public AnimationClip _AnimationClip;
    }
}