using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnGameObjectToPool : MonoBehaviour
{
    public ObjectPool ObjectPool;

    private void OnDisable()
    {
        ObjectPool.ReturnGameObjectToPool(gameObject);
    }
}
