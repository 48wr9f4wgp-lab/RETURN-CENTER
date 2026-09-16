using ReturnCenter.Core.Domain;
using UnityEngine;

namespace ReturnCenter.Presentation
{
    public sealed class BootstrapHud : MonoBehaviour
    {
        private InteractionController _interaction;
        private GUIStyle _title;
        private GUIStyle _body;

        public void Initialize(InteractionController interaction)
        {
            _interaction = interaction;
        }

        private void EnsureStyles()
        {
            if (_title != null) return;
            _title = new GUIStyle(GUI.skin.label) { fontSize = 22, fontStyle = FontStyle.Bold };
            _body = new GUIStyle(GUI.skin.label) { fontSize = 16 };
        }

        private void OnGUI()
        {
            EnsureStyles();
            GUI.Box(new Rect(16, 16, 420, 190), GUIContent.none);
            GUI.Label(new Rect(32, 28, 360, 28), "RETURN CENTER — Bootstrap", _title);
            GUI.Label(new Rect(32, 62, 380, 24), "WASD move / Mouse look / Esc unlock cursor", _body);

            if (_interaction == null) return;
            GUI.Label(new Rect(32, 92, 380, 24), string.IsNullOrEmpty(_interaction.Prompt) ? "Look at the package on the inspection desk." : _interaction.Prompt, _body);

            var package = _interaction.LastPackage;
            if (package?.Session == null) return;

            var p = package.Session.CurrentProduct;
            if (p.State != ProductLifecycleState.Received)
            {
                GUI.Label(new Rect(32, 122, 380, 24), $"{p.Archetype} | Condition {p.Condition} | Fault: {p.Fault}", _body);
                GUI.Label(new Rect(32, 146, 380, 24), $"Base ${p.BaseValue:0.00} | Repair ${p.RepairCost:0.00}", _body);
            }

            var outcome = package.Session.LastOutcome;
            if (outcome != null)
            {
                GUI.Label(new Rect(32, 170, 380, 24), $"{outcome.Route}: net ${outcome.NetProfit:0.00} | Cash ${package.Session.Economy.Cash:0.00}", _body);
            }

            GUI.Label(new Rect(Screen.width / 2f - 8, Screen.height / 2f - 14, 20, 20), "+", _title);
        }
    }
}
