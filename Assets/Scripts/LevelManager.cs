using Unity.Entities;
using Unity.Entities.Serialization;
using Unity.Scenes;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private SceneAsset _gameSubScene;
        private Entity _loadedScene;
        
        private readonly SceneSystem.LoadParameters _loadParameters = new(){AutoLoad = true, Flags = SceneLoadFlags.BlockOnStreamIn | SceneLoadFlags.NewInstance};
        private EntitySceneReference _entitySceneReference;

        private void Awake() => _entitySceneReference = new EntitySceneReference(_gameSubScene);

        private void Start() => LoadScene();
        
        private void LoadScene() => _loadedScene = SceneSystem.LoadSceneAsync(World.DefaultGameObjectInjectionWorld.Unmanaged, _entitySceneReference, _loadParameters);

        public void RestartGame()
        {
            SceneSystem.UnloadScene(World.DefaultGameObjectInjectionWorld.Unmanaged, _loadedScene);
            LoadScene();
        }
    }
}