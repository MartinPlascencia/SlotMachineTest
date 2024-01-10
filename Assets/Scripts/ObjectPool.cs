using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public GameObject ObjectPrefab;
    private Stack<GameObject> _objectPool = new Stack<GameObject>();

    public GameObject GetGameObjectFromPool()
    {
        if(_objectPool.Count > 0)
        {
            return _objectPool.Pop();
        }
        else
        {
            return CreateNewGameObject();
        }
    }

    public void ReturnGameObjectToPool(GameObject gameObject)
    {
        //gameObject.transform.SetParent(null);
        _objectPool.Push(gameObject);
    }

    public GameObject CreateNewGameObject()
    {
        GameObject clone = GameObject.Instantiate(ObjectPrefab);
        clone.transform.name = ObjectPrefab.name;
        if(!clone.TryGetComponent<ReturnGameObjectToPool>(out ReturnGameObjectToPool returnGameObjectToPool))
        {
            ReturnGameObjectToPool component = clone.AddComponent<ReturnGameObjectToPool>();
            component.ObjectPool = this;
        }
        
        return clone;
    }
}
