using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace Lab5Games
{
    public class LevelLoader : MonoBehaviour
    {
        [Space]
        [SerializeField] LoadSceneMode loadMode = LoadSceneMode.Single;
        [SerializeField] LevelReference levelRef;
        
        LevelOperation _op;

        [PropertySpace(SpaceBefore =20)]
        [ShowIf("loadMode", LoadSceneMode.Additive)]
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
