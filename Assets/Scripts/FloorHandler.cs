using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandler : MonoBehaviour
{
    public float sizeModifierX = 1.0f;
    [HideInInspector] public float sizeModifierY = 1.0f;
    public float sizeModifierZ = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResizeObject()
    {
        transform.localScale = new Vector3(sizeModifierX, sizeModifierY, sizeModifierZ);
    }
}
