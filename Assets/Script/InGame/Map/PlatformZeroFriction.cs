using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformZeroFriction : MonoBehaviour
{
    private void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.friction = 0;
        collider.sharedMaterial = material;
    }
}
