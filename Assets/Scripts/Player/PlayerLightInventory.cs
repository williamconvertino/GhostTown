using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightInventory : MonoBehaviour
{
    public bool isFlipped = false;

    public GameObject defaultLight;
    
    private GameObject _currentLight;

    private GameObject _activeLightHolder;

    private void Start()
    {
        if (defaultLight != null)
        {
            SetLight(defaultLight);
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && _currentLight != null)
        {
            GetComponentInChildren<LightObject>().ToggleLight();
        }

        // if (currentLight != null)
        // {
        //     Vector3 scale = currentLight.transform.localScale;
        //     currentLight.transform.localScale = new Vector3(scale.x * (isFlipped ? -1: 1), scale.y, scale.z);
        //     Vector3 position = currentLight.GetComponent<LightObject>().offset;
        //     currentLight.transform.localPosition = new Vector3(position.x * (isFlipped ? -1: 1), position.y, position.z);
        // }

        if (_activeLightHolder != null && Input.GetKeyDown(KeyCode.E))
        {
            SetLight(_activeLightHolder.GetComponent<LightHolder>().lightPrefab);
            Destroy(_activeLightHolder);
            _activeLightHolder = null;
        }
    }

    void SetLight(GameObject light)
    {
        if (_currentLight != null)
        {
            Destroy(_currentLight);
        }
        _currentLight = Instantiate(light, transform.position, Quaternion.identity, transform);
        _currentLight.transform.localPosition = _currentLight.GetComponent<LightObject>().offset;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LightHolder"))
        {
            _activeLightHolder = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == _activeLightHolder)
        {
            _activeLightHolder = null;
        }
    }
}

