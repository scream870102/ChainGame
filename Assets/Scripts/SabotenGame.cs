using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotenGame : Game
{
    protected override void Update() {
        base.Update();
        if (Input.GetMouseButtonDown(0)) {
            isCleared = true;
        }
            
    }
}
