using ReturnCenter.Application;
using ReturnCenter.Core.Domain;
using UnityEngine;

namespace ReturnCenter.Presentation
{
    public sealed class ReturnPackageInteractable : MonoBehaviour
    {
        private ManualProcessingSession _session;
        private bool _consumed;

        public ManualProcessingSession Session => _session;
        public bool IsConsumed => _consumed;

        public void Initialize(ManualProcessingSession session)
        {
            _session = session;
            gameObject.name = $"ReturnPackage_{session.CurrentProduct.Id}";
        }

        public string GetPrompt()
        {
            if (_consumed) return string.Empty;
            if (_session.CurrentProduct.State == ProductLifecycleState.Received)
                return "E: inspect return package";

            if (_session.CurrentProduct.State == ProductLifecycleState.Inspected)
                return "1: Resell   2: Repair   3: Recycle";

            return string.Empty;
        }

        public void Inspect()
        {
            if (_consumed || _session.CurrentProduct.State != ProductLifecycleState.Received) return;
            _session.Inspect();
        }

        public void Process(DispositionRoute route)
        {
            if (_consumed || _session.CurrentProduct.State != ProductLifecycleState.Inspected) return;
            _session.Process(route);
            _consumed = true;

            var renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.enabled = false;

            var collider = GetComponent<Collider>();
            if (collider != null)
                collider.enabled = false;
        }
    }
}
