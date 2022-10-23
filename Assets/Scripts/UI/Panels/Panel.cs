using UnityEngine;

namespace TenTen
{
    public abstract class Panel : MonoBehaviour, IPanel
    {
        public abstract void Show();
        public abstract void Hide();
    }
}