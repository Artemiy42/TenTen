using UnityEngine;

namespace CodeBase.UI.Panels
{
    public abstract class Panel : MonoBehaviour, IPanel
    {
        public abstract void Show();
        public abstract void Hide();
    }
}