﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadingProgress : MonoBehaviour
    {
        private AsyncOperation asyncOperation;

        public bool IsDone { get; private set; }
        public float Progress { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(int index)
        {
            IsDone = false;
            Progress = 0.0f;
            StartCoroutine(StartLoadScene(index));
        }

        private IEnumerator StartLoadScene(int index)
        {
            asyncOperation = SceneManager.LoadSceneAsync(index);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone || Progress < 1f)
            {
                Progress = asyncOperation.progress % 0.9f;
                yield return null;
            }
            IsDone = true;
            yield break;
        }

        public void ActiveScene()
        {
            asyncOperation.allowSceneActivation = true;
        }
    }
}