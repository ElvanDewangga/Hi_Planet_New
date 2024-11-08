using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobing : MonoBehaviour
{
    float amplitude;
    float speed;

    void Start()
    {
        amplitude = 1.5f;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, boby());
    }
    float boby()
    {
        return transform.position.y - Mathf.Sin(Time.time * speed) * amplitude * Time.deltaTime;
    }
}
