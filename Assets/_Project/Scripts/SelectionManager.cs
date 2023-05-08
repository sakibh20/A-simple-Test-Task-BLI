using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private List<ISelectable> allSelectables;

        private ISelectable _currentlySelected;

        private ReferenceManager _referenceManager;

        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
            allSelectables = new List<ISelectable>();
        }

        private void OnEnable()
        {
            InstantiateManager.OnNewInstantiated += OnInstantiated;
            ModeManager.OnModeChange += OnModeChanged;
        }

        private void OnDisable()
        {
            InstantiateManager.OnNewInstantiated -= OnInstantiated;
        }

        private void OnInstantiated(ISelectable selectable)
        {
            if(!allSelectables.Contains(selectable))
            {
                allSelectables.Add(selectable);
            }
        }
        
        private void OnDeleted(ISelectable selectable)
        {
            if(allSelectables.Contains(selectable))
            {
                allSelectables.Remove(selectable);
            }
        }

        public void OnSelected(ISelectable selectable)
        {
            if (_currentlySelected != null)
            {
                _currentlySelected.OnDeSelect();
            }
            
            _currentlySelected = selectable;
        }

        private void OnModeChanged()
        {
            if (_referenceManager.modeManager.ActiveMode == Modes.Instantiate)
            {
                if (_currentlySelected != null)
                {
                    _currentlySelected.OnDeSelect();
                }
            }
        }
    }
}
