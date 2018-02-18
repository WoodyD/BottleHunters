using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Territory { Own, Neutral, Enemy }

public class Person : MonoBehaviour {

    public MainData PlayerStats;

    [System.Serializable]
    public class MainData {
        public int playerID;
        public float health;
        public float speed;
        public float jumpSpeed;
        //private float health;
        //public float Health
        //{
        //    get { return health; }
        //    set { health = value; }
        //}
        //private float speed;
        //public float Speed
        //{
        //    get { return speed; }
        //    set { speed = value; }
        //}
        //private float power;
        //public float Power
        //{
        //    get { return power; }
        //    set { power = value; }
        //}
        //private int capasity;
        //public int Capasity
        //{
        //    get { return capasity; }
        //    set { capasity = value; }
        //}
    }


}
