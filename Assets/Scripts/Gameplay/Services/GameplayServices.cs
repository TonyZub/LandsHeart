using System;

namespace LandsHeart
{
	public sealed class GameplayServices : IDisposable
	{
        #region Fields

        private CameraService _cameraService;
        private DialogueService _dialogueService;
        private ResourcesService _resourcesService;
        private PhysicsService _physicsService;

        #endregion


        #region Properties

        public CameraService CameraService => _cameraService;
        public DialogueService DialogueService => _dialogueService;
        public ResourcesService ResourcesService => _resourcesService;
        public PhysicsService PhysicsService => _physicsService;

        #endregion


        #region Constructor

        public GameplayServices()
        {
            _cameraService = new CameraService(ObjectFinder.FindObjectOfType<CameraHolder>(true));
            _dialogueService = new DialogueService();
            _resourcesService = new ResourcesService();
            _physicsService = new PhysicsService(Data.PhysicsServiceData);
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            _resourcesService.Dispose();
        }

        #endregion
    }
}