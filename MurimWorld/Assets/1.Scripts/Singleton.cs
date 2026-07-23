using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static bool _applicationIsQuitting = false;
    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] 어플리케이션 종료 중 {typeof(T)}를 요청하여 null을 반환.");
                return null;
            }
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name + " (Singleton)");
                    _instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"[{typeof(T).Name}] 중복된 싱글톤 매니저가 감지되어{gameObject.name}을 파괴합니다.");
            Destroy(gameObject);
        }
    }

    protected void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }

    protected void OnDestroy()
    {
        _applicationIsQuitting = true;
    }
}
