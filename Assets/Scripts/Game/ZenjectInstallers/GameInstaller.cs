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
        [SerializeField] private GameView gameView;

        public override void InstallBindings()
        {
            Container.BindInstance(gameView).AsSingle();
            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLogicController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
        }
    }
}