using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    //public float speed;
    Rigidbody2D rig;
    StartDialog sd;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sd = GameObject.FindObjectOfType<StartDialog>();
    }

    Vector3 dir;
    bool isCanInteract;
    [HideInInspector]public InteractedHolder ih;
    void Update()
    {
        if(isCanInteract == true)
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                ih.OnInteracted.Invoke();
                Debug.Log("getkeyB");
            }
        }
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            dir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.up;
            dir.z = 0;
            rig.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);

            if(Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
            }else{
                GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Npc"))
        {
            if(StartDialog.instance.target == null)
            {
                other.transform.GetChild(0).gameObject.SetActive(true);
                if(other.TryGetComponent(out npcDialog npcdialog))
                {
                    StartDialog.instance.target = npcdialog;
                }
                if(other.TryGetComponent(out npcDialogInteractive npcDialoginteractive))
                {
                    StartDialog.instance.npcInter = npcDialoginteractive;
                }
            }
        }

        if(other.gameObject.CompareTag("Interacted"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            ih = other.GetComponent<InteractedHolder>();
            isCanInteract = true;
        }

        if(other.gameObject.CompareTag("door"))
        {
            StartCoroutine(other.GetComponent<Dungeonsition>().GoToArea(gameObject));
        }

        // if(other.gameObject.CompareTag("trap"))
        // {
        //     Debug.Log("kena trap");
        //     StartCoroutine(UIManager.instance.ShowGameOverPanel());
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Npc"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            if(StartDialog.instance.target != null)
            {
                StartDialog.instance.target = null;
            }
            if(StartDialog.instance.npcInter != null)
            {
                StartDialog.instance.npcInter = null;
            }
        }

        if(other.gameObject.CompareTag("Interacted"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            isCanInteract = false;
            ih = null;
        }
    }
}
