using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//Must inherit Class Game
//
public class SabotenGame : Game
{
    //must override update and write your game
    protected override void Update() {
        base.Update();
        //
        //
        //Write Your Game After
        if (Input.GetMouseButtonDown(0)) {
            isCleared = true;
        }
            
    }
}
