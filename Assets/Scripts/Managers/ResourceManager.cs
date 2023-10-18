using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public void Init()
    {
    }

    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(Sprite))
        {
            if (sprites.TryGetValue(path, out Sprite sprite))
                return sprite as T;

            Sprite sp = Resources.Load<Sprite>(path);
            sprites.Add(path, sp);
            return sp as T;
        }

        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');

            if (index >= 0)
                name = name.Substring(index + 1);
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Vector3 pos)
    {
        return Instantiate(path, pos, Quaternion.identity);
    }
    public GameObject Instantiate(string path, Vector3 pos, Vector3 euler)
    {
        Quaternion q = Quaternion.Euler(euler);

        return Instantiate(path, pos, q);
    }
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            throw new System.Exception("null GameObject");
        }

        return Instantiate(original, original.transform.position, original.transform.rotation, parent);
    }
    public GameObject Instantiate(string path, Vector3 position, Quaternion q, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            throw new System.Exception("null GameObject");
        }

        return Instantiate(original, position, q, parent);
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion q, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, position, q, parent);
        go.name = prefab.name;
        return go;
    }


    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        if (!go.activeSelf)
            return;

        Object.Destroy(go);
        go = null;
    }
}
