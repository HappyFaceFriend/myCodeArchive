using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
    public T Singleton { get { return instance; } }
    static T instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
