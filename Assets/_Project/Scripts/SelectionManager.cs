using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        private List<ISelectable> allSelectables;

        private ISelectable _currentlySelected;

        public static event Action<List<ISelectable>> OnListRefreshed;
        
        private ReferenceManager _referenceManager;

        private void Awake()
        {
            
            allSelectables = new List<ISelectable>();
        }

        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
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

                OnListRefreshed?.Invoke(allSelectables);
            }
        }

        public void RefreshList()
        {
            OnListRefreshed?.Invoke(allSelectables);
        }
        
        public void OnDeleted(ISelectable selectable)
        {
            if(allSelectables.Contains(selectable))
            {
                allSelectables.Remove(selectable);
                _currentlySelected = null;

                OnListRefreshed?.Invoke(allSelectables);
            }
        }

        public void OnSelected(ISelectable selectable)
        {
            if (_currentlySelected != null && _currentlySelected!= selectable)
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
