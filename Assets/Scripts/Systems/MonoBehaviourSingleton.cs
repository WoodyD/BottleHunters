using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour {

	protected static T _instance;
	
    public static T Instance {
		get {
			return _instance;
		}
	}

	protected virtual void Awake() {
		if (_instance != default(T) && _instance != this) {
			DestroyImmediate(gameObject);
		} else {
			_instance = gameObject.GetComponent<T>();
		}
	}
	
    public static bool IsNull {
        get {
            return _instance == default(T);
        }
    }

	protected virtual void OnDestroy() {
		if (_instance != this)
			return;
		
		_instance = default(T);
	}

}
