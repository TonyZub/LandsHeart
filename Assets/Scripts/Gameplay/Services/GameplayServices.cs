using System;

namespace LandsHeart
{
	public sealed class GameplayServices : IDisposable
	{
        #region Fields

        private DialogueService _dialogueService;
        private ResourcesService _resourcesService;

        #endregion


        #region Properties

        public DialogueService DialogueService => _dialogueService;
        public ResourcesService ResourcesService => _resourcesService;

        #endregion


        #region Constructor

        public GameplayServices()
        {
            _dialogueService = new DialogueService();
            _resourcesService = new ResourcesService();
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