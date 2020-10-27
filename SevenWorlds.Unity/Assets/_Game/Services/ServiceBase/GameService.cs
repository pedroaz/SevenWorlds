using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService<T> : SetupMonoBehaviour
{
    [HideInInspector]
    public static T Object;
}
