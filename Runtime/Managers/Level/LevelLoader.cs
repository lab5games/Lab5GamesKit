using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Lab5Games
{
    public class LevelLoader : MonoBehaviour
    {
        [Space]
        [SerializeField] LoadSceneMode loadMode = LoadSceneMode.Single;
        [SerializeField] LevelReference levelRef;
        
        LevelAsync _op;

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore =20)]
        [ShowIf("loadMode", LoadSceneMode.Additive)]
#endif
        [SerializeField] UnityEvent onLoaded;

        public void Load()
        {
            _op = LevelManager.LoadLevel(
                levelRef,
                loadMode,
                loadMode == LoadSceneMode.Single ? true : false);

            if (loadMode == LoadSceneMode.Additive)
                _op.onLoaded += x => { onLoaded?.Invoke(); };
        }

        public void SetLevelVisible()
        {
            _op.SetVisible();
        }
    }
}
