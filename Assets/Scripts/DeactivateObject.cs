using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeactivateObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDeactivate;
    [SerializeField] private float _timeToLive = 3f;

    public void Deactivate()
    {
        StartCoroutine(DeactivateDelay());
    }

    private IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(_timeToLive);
        OnDeactivate?.Invoke();
        gameObject.SetActive(false);
    }
}
