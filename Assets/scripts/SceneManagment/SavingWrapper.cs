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

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(dfaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            if (fader != null)
            {
                fader.FadeOutImmediate();
            }
            if (fader != null)
            {
                yield return fader.FadeIn(fadeInTime);
            }
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

            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
            }
        }

        public void Load()
        {
            //call to saving sistem
            GetComponent<SavingSystem>().Load(dfaultSaveFile);
            print("Loaded");
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(dfaultSaveFile);
            print("Saved");
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(dfaultSaveFile);
            print("Deleted");
        }

    }
}