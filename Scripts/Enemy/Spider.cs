﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }

    //Use for Initialization
    public override void Init()
    {
        base.Init();
    }

    public void Damage()
    {
        
    }
}
