using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string dfaultSaveFile = "save";

        [SerializeField] float fadeInTime = 1f;

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(dfaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

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

        public void Load()
        {
            //call to saving sistem
            GetComponent<SavingSystem>().Load(dfaultSaveFile);
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(dfaultSaveFile);

        }

    }
}