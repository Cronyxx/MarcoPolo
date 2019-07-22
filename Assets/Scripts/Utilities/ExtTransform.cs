using UnityEngine;
public static class Transforms
{

    public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
    {
        foreach (Transform child in t)
        {
            if (destroyImmediately)
                Object.DestroyImmediate(child.gameObject);
            else
                Object.Destroy(child.gameObject);
        }
    }
}