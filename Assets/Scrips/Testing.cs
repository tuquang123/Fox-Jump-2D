using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Transform DamePopup;

    private void Start()
    {
        Instantiate(DamePopup,Vector3.zero,Quaternion.identity);
    }
}
