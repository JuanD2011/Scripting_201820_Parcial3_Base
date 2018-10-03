using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Factory : MonoBehaviour
{
    private static Factory instace;

    public static Factory Instace
    {
        get
        {
            return instace;
        }
    }

    private void Awake()
    {
        instace = this;
    }

    public GameObject FactoryObject(GameObject _object)
    {
        GameObject ai = null;
        ai = Instantiate(_object);
        return ai;
    }
}
