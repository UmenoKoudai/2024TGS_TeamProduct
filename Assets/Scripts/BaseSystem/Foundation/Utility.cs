
using UnityEngine;

static public class Utility
{
    static public GameObject Instantiate(GameObject origin, Transform parent = null)
    {
        var ret = GameObject.Instantiate(origin, parent);
        ret.name = ret.name.Replace("(Clone)", "");
        return ret;
    }
}