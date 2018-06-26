using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Game {

	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        //
        if (Input.GetMouseButtonDown(0))
            isCleared = true;
    }
}
