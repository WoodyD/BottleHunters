using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    readonly System.Action<bool> keyPressed;

    bool pushed;
    public void GetKeyEvent(KeyCode key, bool singlePush = true) {
        if (singlePush && !pushed) {
            if (Input.GetKey(key)) {
                pushed = true;
                keyPressed(true);
            }
            keyPressed(false);
        } else if (singlePush && pushed) {
            pushed &= !Input.GetKeyUp(key);
            keyPressed(false);
        } else {
            keyPressed(Input.GetKey(key));
        }
    }

}
