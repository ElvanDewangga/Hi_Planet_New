using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Char Status", menuName = "StarSky/Create Char Status", order = 1)]
public class CharStatus : ScriptableObject{
   [SerializeField] private string characterName = "";

    // Char_Status :
    public int life, attack, defense, speed, intelligence;

    public string type;

    public string element;

    public Sprite bodySample;

    public Sprite handLeftSample;

    public Sprite handRightSample;

    public RuntimeAnimatorController animatorHandLeft;
    public RuntimeAnimatorController animatorHandRight;
    public RuntimeAnimatorController animatorBody;
    public Object_Variant_Config [] skillConfigs;

    public AnimationData [] animationDatas;
    
    [System.Serializable]
    public class AnimationData {
        public string name;
        public AnimationClip clip;
    }
    
}
