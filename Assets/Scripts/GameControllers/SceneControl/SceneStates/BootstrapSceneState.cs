using DG.Tweening;

namespace LandsHeart
{
    public sealed class BootstrapSceneState : BaseSceneState
    {
        #region Constants

        private const float TIME_BEFORE_START_GAME = 0f;

        #endregion


        #region Constructor

        public BootstrapSceneState(SceneStateNames stateName, SceneNames sceneName) : base(stateName, sceneName) { }

        #endregion


        #region Methods

        public override void EnterState(bool doLoadScene)
        {
            base.EnterState(doLoadScene);
            DOVirtual.DelayedCall(TIME_BEFORE_START_GAME, () => SetStartState());
        }

        private void SetStartState()
        {
            SceneStateMachine.Instance.SetState(SceneStateNames.MainScene);
        }

        public override void ExitState()
        {
            //TODO
        }

        #endregion
    }
}
