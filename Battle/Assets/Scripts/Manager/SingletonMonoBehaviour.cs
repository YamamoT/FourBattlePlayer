using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T instance;

    static public T Instance
    {
        get
        {
            if (instance == null)
                instance = (T)FindObjectOfType(typeof(T));

            //if (instance == null)
            //    Debug.LogError(typeof(T) + "is nothing");

            return instance;
        }
    }
}
