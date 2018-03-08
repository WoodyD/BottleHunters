using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    private static DontDestroyOnLoad gameComponent;

	void Awake () {
        
		DontDestroyOnLoad(this);

        if(gameComponent == null)
            gameComponent = this;         
        else
            Destroy(this);
        
	}

}
