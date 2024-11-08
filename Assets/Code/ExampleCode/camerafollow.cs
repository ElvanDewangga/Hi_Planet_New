using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    Vector3 cameoff;
    public Transform player;
    [Range(0.01f, 10f)]
    public float smoothfactor;
    public bool on;
    // Start is called before the first frame update
    void Start()
    {
        cameoff = transform.position - player.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(on)
        {
            Vector3 campos = player.position + cameoff;
            campos.z = -10;
            transform.position = Vector3.Slerp(transform.position, campos, smoothfactor * Time.deltaTime);
        }
    }
}
