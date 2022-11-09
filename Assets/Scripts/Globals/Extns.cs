using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extns
{
    public static IEnumerator Tweeng(this float duration,
               System.Action<float> var, float aa, float zz)
    {
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT)
        {
            float t = (Time.time - sT) / duration;
            var(Mathf.SmoothStep(aa, zz, t));
            yield return null;
        }

        var(zz);
    }

    public static IEnumerator Tweeng(this float duration, System.Action<Vector3> var, Vector3 aa, Vector3 zz)
    {
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT)
        {
            float t = (Time.time - sT) / duration;
            var(Vector3.Lerp(aa, zz, Mathf.SmoothStep(0f, 1f, t)));
            yield return null;
        }

        var(zz);
    }

    public static IEnumerator TweengC(this float duration, System.Action<Color32> var, Color32 aa, Color32 zz)
    {
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT)
        {
            float t = (Time.time - sT) / duration;
            var(Color32.Lerp(aa, zz, Mathf.SmoothStep(0f, 1f, t)));
            yield return null;
        }

        var(zz);
    }

    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }
}