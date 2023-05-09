using UnityEngine;

namespace _Project.Scripts
{
    public interface ISelectable
    {
        public InstantiatableTypes Type();
        public Transform Transform();
        public void OnSelect();
        public void OnDeSelect();
    }
}
