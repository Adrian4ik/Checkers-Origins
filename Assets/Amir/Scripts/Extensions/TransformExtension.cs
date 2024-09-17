using UnityEngine;

public static class TransformExtension
{
    public static void DestroyChildren(this Transform obj)
    {
        if (obj.childCount > 0)
            while (obj.childCount > 0)
                Object.DestroyImmediate(obj.GetChild(0));
    }
}
