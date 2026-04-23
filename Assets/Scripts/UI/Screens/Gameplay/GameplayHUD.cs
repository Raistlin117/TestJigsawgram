using Core.Audio;
using Cysharp.Threading.Tasks;
using Gameplay.Puzzle;
using Infrastructure.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Screens.Gameplay
{
    public class GameplayHUD : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        private ISceneLoader _sceneLoader;
        private PuzzleSession _session;
        private AudioService _audio;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, PuzzleSession session, AudioService audio)
        {
            _sceneLoader = sceneLoader;
            _session = session;
            _audio = audio;
        }

        private void Awake()
        {
            _backButton.onClick.AddListener(OnBackClicked);
        }

        private void OnBackClicked()
        {
            _audio.Play(AudioId.Click);
            _session.Reset();
            _sceneLoader.LoadAddressableSceneAsync(SceneKeys.Menu, false, destroyCancellationToken).Forget();
        }

        private void OnDestroy() => _backButton.onClick.RemoveAllListeners();
    }
}