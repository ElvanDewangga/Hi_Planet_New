using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Sample_Scene : MonoBehaviour
{
    public static Sample_Scene Ins;

    // Char_Data_Source :
    public Transform World_Object;

    [SerializeField] private Camera Main_Camera;

    public Search_Active_Object _Search_Active_Object_World;

    [Header("GetLoginAutomatic")] public UnityEvent OnAutoLogin;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        //login otomatis ketika perpindahan scene
        StartCoroutine(LoginAutomatic());
    }

    // Enemy_Test_Scene :
    public void On_Keluar_Scene()
    {
        World_Object.gameObject.SetActive(false);
    }

    private IEnumerator LoginAutomatic()
    {
        yield return new WaitForSeconds(0.25f);
        if (PlayerPrefs.HasKey("FromScene"))
        {
            OnAutoLogin.Invoke();
            PlayerPrefs.DeleteKey("FromScene");
        }
    }
}