using ReturnCenter.Core.Domain;
using UnityEngine;

namespace ReturnCenter.Presentation
{
    public sealed class InteractionController : MonoBehaviour
    {
        [SerializeField] private float interactionDistance = 3.0f;
        private FirstPersonController _player;
        private ReturnPackageInteractable _focused;

        public string Prompt { get; private set; } = string.Empty;
        public ReturnPackageInteractable LastPackage { get; private set; }

        private void Awake()
        {
            _player = GetComponent<FirstPersonController>();
        }

        private void Update()
        {
            UpdateFocus();
            if (_focused == null) return;

            if (Input.GetKeyDown(KeyCode.E))
                _focused.Inspect();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                _focused.Process(DispositionRoute.Resell);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _focused.Process(DispositionRoute.Repair);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _focused.Process(DispositionRoute.Recycle);

            Prompt = _focused.GetPrompt();
        }

        private void UpdateFocus()
        {
            _focused = null;
            Prompt = string.Empty;

            if (_player == null || _player.ViewCamera == null) return;
            var ray = new Ray(_player.ViewCamera.transform.position, _player.ViewCamera.transform.forward);
            if (!Physics.Raycast(ray, out var hit, interactionDistance)) return;

            _focused = hit.collider.GetComponentInParent<ReturnPackageInteractable>();
            if (_focused == null || _focused.IsConsumed) return;

            LastPackage = _focused;
            Prompt = _focused.GetPrompt();
        }
    }
}
