namespace GameFramework.Utilities
{

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.SceneManagement;
    using GameFramework.Logging;

    public enum UnityScene
    {
        Splash,
        Portal,
        Game,
    }

    public class SceneLoader : GameFramework.Pattern.MonoSingleton<SceneLoader>
    {
        // Events
        private UnityAction<Scene, LoadSceneMode> _onSceneLoaded;
        private UnityAction<Scene> _onSceneUnloaded;

        // Loading
        private UnityScene _curLoadingSceneId;
        private AsyncOperation _curLoadingSceneOp;
        private LoadSceneMode _curLoadingSceneMode;


        // Unloading
        private AsyncOperation _curUnloadingSceneOp;


        private bool _wasSplashActived = false;
        public bool WasSplashActive
        {
            get
            {
                return _wasSplashActived;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _wasSplashActived = GetSceneName(UnityScene.Splash)
                                .ToLower()
                                .EndsWith(SceneManager.GetActiveScene().name.ToLower());
            UnityLog.LogI("[SceneLoader] current active scene " + SceneManager.GetActiveScene().name);
            UnityLog.LogI("[SceneLoader] is splash active " + _wasSplashActived);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private string GetSceneName(UnityScene sceneId)
        {
            switch (sceneId)
            {
                case UnityScene.Splash:
                    return @"Scenes/Splash";
                case UnityScene.Portal:
                    return @"Scenes/Portal";
                case UnityScene.Game:
                    return @"Scenes/Level1";
            }
            return string.Empty;
        }

        private void Update()
        {
            if (_curLoadingSceneOp == null) { return; }


            UnityLog.LogI(string.Format("[SceneLoader] Load {0} Process {1}", _curLoadingSceneId, _curLoadingSceneOp.progress));
            bool isLoaded = _curLoadingSceneOp.progress >= 0.9f;
            if (isLoaded)
            {
                OnSceneLoaded(SceneManager.GetSceneByPath(GetSceneName(_curLoadingSceneId)), _curLoadingSceneMode);
            }
        }

        public AsyncOperation LoadAsync(UnityScene sceneId, UnityAction<Scene, LoadSceneMode> onSceneLoaded, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            UnityLog.LogI(string.Format("[SceneLoader] Load Async {0}", sceneId));
            _curLoadingSceneId = sceneId;
            _curLoadingSceneMode = mode;
            _curLoadingSceneOp = SceneManager.LoadSceneAsync(GetSceneName(sceneId), mode);
            _curLoadingSceneOp.allowSceneActivation = false;
            _onSceneLoaded = onSceneLoaded;
            return _curLoadingSceneOp;
        }
        public void Load(UnityScene sceneId, UnityAction<Scene, LoadSceneMode> onSceneLoaded, LoadSceneMode mode = LoadSceneMode.Single)
        {
            UnityLog.LogI(string.Format("[SceneLoader] Load Sync {0}", sceneId));
            _curLoadingSceneId = sceneId;
            _curLoadingSceneMode = mode;
            _onSceneLoaded = onSceneLoaded;
            SceneManager.LoadScene(GetSceneName(sceneId), mode);
        }

        public AsyncOperation Unload(UnityScene sceneId, UnityAction<Scene> onSceneUnloaded)
        {
            UnityLog.LogI(string.Format("[SceneLoader] Unload {0}", sceneId));
            _onSceneUnloaded = onSceneUnloaded;
            _curUnloadingSceneOp = SceneManager.UnloadSceneAsync(GetSceneName(sceneId));
            return _curUnloadingSceneOp;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UnityLog.LogI(string.Format("[SceneLoader] On Scene Loaded {0}", scene.path));
            if (_onSceneLoaded != null)
            {
                _onSceneLoaded.Invoke(scene, mode);
                _onSceneLoaded = null;
            }
            _curLoadingSceneOp = null;
        }
        private void OnSceneUnloaded(Scene scene)
        {
            UnityLog.LogI(string.Format("[SceneLoader] On Scene Unloaded {0}", scene.path));
            if (_onSceneUnloaded != null)
            {
                _onSceneUnloaded.Invoke(scene);
                _onSceneUnloaded = null;
            }
            _curUnloadingSceneOp = null;
        }
    }
}