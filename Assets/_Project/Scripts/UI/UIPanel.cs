using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour
    {
        public abstract UniTask Hide();

        public abstract UniTask Show();
    }
}