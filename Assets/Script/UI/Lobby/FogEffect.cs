using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEffect : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x > 10) transform.position = new Vector3(-10, transform.position.y, transform.position.z);
    }
}

