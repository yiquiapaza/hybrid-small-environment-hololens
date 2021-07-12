using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static GenericPosition GenericPositionJson(string JsonString)
    {
        return JsonUtility.FromJson<GenericPosition>(JsonString);
    }
}
