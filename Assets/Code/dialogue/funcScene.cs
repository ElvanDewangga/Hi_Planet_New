using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum CutProp {Cut1_Prop, Cut2_Prop, Cut3_Prop, Cut4_Prop}

public class funcScene : MonoBehaviour
{
    public CutProp Prop;
    [Header("Cut1")]
    public GameObject moolu1;
    public Transform moolusLab;

    [Header("cut2")]
    public Transform labdoor;
    public GameObject moolu2;

    public void startEvent()
    {
        EventManager.instance.startEvent();
    }
    public void cutsceneTrigger()
    {
        CDialogManager.instance.CutsceneTrigger();
    }

    public void OpenCosmicEnergyStore()
    {
        UIManager.instance.OpenCosmicEnergyStore();
    }
    public void OpenCosmicStore()
    {
        UIManager.instance.OpenCosmicStore();
    }
    public void OpenCosmicForum()
    {
        UIManager.instance.OpenCosmicForum();
    }
    public void OpenBookPanel()
    {
        UIManager.instance.OpenBookPanel();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    public void OpenCraftPanel()
    {
        UIManager.instance.OpenCraftPanel();
    }
    public void OpenRefinePanel()
    {
        UIManager.instance.OpenRefinePanel();
    }
    public void OpenEnchancePanel()
    {
        UIManager.instance.OpenEnchancePanel();
    }   

// [CustomEditor (typeof (funcScene))]
// public class CutsProp : Editor {
// 	void OnInspectorGUI () {
// 		funcScene cut = (funcScene)target;
		
// 		// Display dropdown
// 		cut.Prop = (CutProp)EditorGUILayout.EnumPopup ("CutProp", cut.Prop);
//         switch(cut.Prop)
//         {
//             case CutProp.Cut1_Prop:
//             {
//                 cut.obj = (GameObject)EditorGUILayout.ObjectField("MoolusLab", cut.obj, typeof(GameObject), true);
//             }break;

//             case CutProp.Cut2_Prop:
//             {
//                 //
//             }break;

//             case CutProp.Cut3_Prop:
//             {
//                 //
//             }break;

//             case CutProp.Cut4_Prop:
//             {
//                 //
//             }break;
//         }
// 	}
// }
}
