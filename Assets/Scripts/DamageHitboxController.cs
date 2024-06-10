using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitboxController : MonoBehaviour
{

    public BoxCollider2D[] attackCollisions;

    float lastInputX = 1;
    float lastInputY = 0;
    public void ActivateHitBox(float x, float y)
    {
        if(x != 0)
        {
            lastInputX = x;
        }
        if(y != 0)
        {
            lastInputY = y;
        }

        if(x == 0 && y != 0)
        {
            lastInputX = 0;
        }
        if (lastInputX > 0)
        {
            attackCollisions[0].enabled = true ;
        }else if (lastInputX < 0)
        {
            attackCollisions[1].enabled = true;

        } else if (lastInputY < 0)
        {
            attackCollisions[2].enabled = true;
        }
        else if (lastInputY > 0)
        {
            attackCollisions[3].enabled = true;
        }

    }

    public void DesactivateHitBox()
    {
        for(int i = 0; i<attackCollisions.Length; i++)
        {
            attackCollisions[i].enabled = false;
        }
    }

}
