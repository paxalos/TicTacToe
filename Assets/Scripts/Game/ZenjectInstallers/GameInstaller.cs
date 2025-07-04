using Game.Models;
using Game.PlayerLogics;
using Game.Presenters;
using Game.Views;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameWindowView gameWindow;

        public override void InstallBindings()
        {
            Container.BindInstance(gameWindow).AsSingle();
            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLogicPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameWinPresenter>().AsSingle();
        }
    }
}