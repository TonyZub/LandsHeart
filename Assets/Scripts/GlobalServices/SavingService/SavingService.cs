using System;
using DG.Tweening;


namespace LandsHeart
{
    public sealed class SavingService
    {
        #region Constants

        private const float SAVE_DELAY = 0.1f;

        #endregion


        #region Events

        public event Action DataSaved;

        #endregion


        #region Fields

        private Tween _savingTween;

        #endregion


        #region Properties

        public bool IsSaving => _savingTween.IsPlaying();

        #endregion


        #region Methods

        public void SaveDataForCurrentScene()
        {
            SaveData(SceneStateMachine.Instance.CurrentSceneState.StateName);
        }

        public void SaveDataForCurrentSceneImmediately()
        {
            SaveDataImmediately(SceneStateMachine.Instance.CurrentSceneState.StateName);
        }

        public void SaveDataDefault(SceneStateNames startSceneState)
        {
            SaveDataImmediately(startSceneState);      
        }

        public void SaveDataForSceneSwitch(SceneStateNames nextSceneState)
        {
            SaveDataImmediately(nextSceneState);
        }

        private void SaveData(SceneStateNames sceneStateName)
        {
            _savingTween = DOVirtual.DelayedCall(SAVE_DELAY, () => SavingSystem.
                RewriteSaveData(CreateNewSaveData(sceneStateName), 0)).OnComplete(OnDataSaved);
        }

        private void SaveDataImmediately(SceneStateNames sceneStateName)
        {
            SavingSystem.RewriteSaveData(CreateNewSaveData(sceneStateName), 0);
        }

        private void OnDataSaved()
        {
            DataSaved?.Invoke();
        }

        #endregion


        #region Creating Save File Methods

        private SaveData CreateNewSaveData(SceneStateNames stateName)
        {
            return new SaveData(stateName);
        }

        #endregion
    }
}

