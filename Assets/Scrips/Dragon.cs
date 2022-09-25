using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public GameObject boss;
    private void Death()
    {
        Destroy(this.boss);
    }
}
