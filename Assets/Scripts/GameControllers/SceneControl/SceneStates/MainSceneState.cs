namespace LandsHeart
{
    public sealed class MainSceneState : BaseSceneState
    {
        #region Constructor

        public MainSceneState(SceneStateNames stateName, SceneNames sceneName) : base(stateName, sceneName) { }

        #endregion


        #region Methods

        public override void EnterState(bool doLoadScene)
        {
            //TODO
            base.EnterState(doLoadScene);
        }

        protected override void OnSceneLoadingComplete()
        {
            GlobalContext.Instance.GlobalServices.LocalizationService.UpdateDialogueSystemLanguage();
            GlobalContext.Instance.RegisterDependency(new GameplayServices(), GlobalContext.DisposableTypes.Local);
            GlobalContext.Instance.RegisterDependency(new GameCycleController(), GlobalContext.DisposableTypes.Global);
            GlobalContext.Instance.RegisterDependency(new ObjectsMovementController(), GlobalContext.DisposableTypes.Local);
            //GlobalContext.Instance.GetDependency<GlobalServices>().AudioService.ChangeMusic(MusicSoundNames.Lvl_1_music,
            //    SCENE_MUSIC_SWITCH_TIME);
            base.OnSceneLoadingComplete();
        }

        public override void ExitState()
        {
            //GlobalContext.Instance.GlobalServices.AudioService?.ChangeMusic(MusicSoundNames.None, SCENE_MUSIC_SWITCH_TIME);
        }

        #endregion
    }
}

