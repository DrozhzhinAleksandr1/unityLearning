using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectSpawner;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPerstentObjects();

            hasSpawned = true;
        }

        private void SpawnPerstentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectSpawner);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
