using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService<T> : SetupMonoBehaviour
{
    [HideInInspector]
    protected static T Object;
}
