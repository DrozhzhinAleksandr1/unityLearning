using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string dfaultSaveFile = "save";
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        private void Load()
        {
            //call to saving sistem
            GetComponent<SavingSystem>().Load(dfaultSaveFile);
        }
        private void Save()
        {
            GetComponent<SavingSystem>().Save(dfaultSaveFile);

        }

    }
}