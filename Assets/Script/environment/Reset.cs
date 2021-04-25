using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    [SerializeField] private GameObject _playerPosition;
    [SerializeField] private Transform _spawnPosition;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _playerPosition)
        {
            _playerPosition.transform.position = _spawnPosition.position;
        }

    }

}
