using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des : MonoBehaviour
{
    public float timeDestroy;
    void Update()
    {
        Destroy();
    }

    private void Destroy()
    {
        
        Destroy(this.gameObject,timeDestroy);
    }
}
