using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance;
    private List<Coroutine> _coroutines = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Coroutine Run(IEnumerator routine)
    {
        var c = StartCoroutine(Wrap(routine));
        _coroutines.Add(c);
        return c;
    }

    private IEnumerator Wrap(IEnumerator routine)
    {
        yield return routine;
        _coroutines.RemoveAll(c => c == null);
    }

    public void StopAll()
    {
        foreach (var c in _coroutines)
            if (c != null)
            {
                Debug.LogWarning("코루틴 종료");
                StopCoroutine(c);
            }

        _coroutines.Clear();
    }
}
