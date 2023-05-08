using System;
using UnityEngine;

namespace _Project.Scripts
{
    public enum Modes
    {
        Instantiate,
        Edit,
        None
    }
    public class ModeManager : MonoBehaviour
    {
        [SerializeField] private Modes activeMode = Modes.Instantiate;

        public Modes ActiveMode => activeMode;

        //set => activeMode = value;
        public event Action OnModeChange;
        
        private ReferenceManager _referenceManager;
        
        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
        }

        public void ChangeMode(Modes newMode)
        {
            activeMode = newMode;
            OnModeChange?.Invoke();
        }
    }
}
