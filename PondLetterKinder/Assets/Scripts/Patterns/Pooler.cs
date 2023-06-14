using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pooler
{
    private static Dictionary<string, Pool> pools = new();

    public static void OnStart()
    {
        if (pools.Count > 0)
            pools.Clear();
    }

    public static void Spawn(GameObject go, Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        string key = go.name.Replace("(Clone)", "");

        if (pools.ContainsKey(key))
        {
            if (pools[key].inactive.Count == 0)
            {
                Object.Instantiate(go, pos, rot);
            }
            else
            {
                obj = pools[key].inactive.Pop();
                obj.transform.SetPositionAndRotation(pos, rot);
                obj.SetActive(true);
            }
        }
        else
        {
            Object.Instantiate(go, pos, rot);
            Pool newPool = new();

            pools.Add(key, newPool);
        }
    }

    public static void Despawn(GameObject go)
    {
        string key = go.name.Replace("(Clone)", "");

        if (pools.ContainsKey(key))
        {
            pools[key].inactive.Push(go);
            go.SetActive(false);
        }
        else
        {
            Pool newPool = new();
            pools.Add(key, newPool);
            pools[key].inactive.Push(go);
            go.SetActive(false);
        }
    }
}
