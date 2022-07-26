using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirstScene : MonoBehaviour
{
    private void Start()
    {
        EventManager.loadOpeningScene?.Invoke();
    }
}
