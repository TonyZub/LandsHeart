using echo17.EndlessBook;
using UnityEngine;
using Zenject;

namespace LandsHeart
{
	public sealed class MainSceneInstaller : MonoInstaller
	{
        #region Fields

        [SerializeField] private EndlessBook _bookModel;

        #endregion


        public override void InstallBindings()
        {
            GlobalContext.Instance.RegisterDependency(_bookModel);
        }
    }
}