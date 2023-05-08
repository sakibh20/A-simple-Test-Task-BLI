using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using UnityEngine;

namespace _Project.Scripts
{
    public class InputManager : MonoBehaviour
    {
        private Camera _mainCamera;
        private RaycastHit _hit;

        private ReferenceManager _referenceManager;

        private void Start()
        {
            _mainCamera = Camera.main;
            _referenceManager = ReferenceManager.instance;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
                {
                    OnRaycastHit();
                }
            }
        }

        private void OnRaycastHit()
        {
            if(_hit.collider.gameObject.TryGetComponent(out ISelectable selectable))
            {
                selectable.OnSelect();
                EnableEditMode();
            }
            
            else if (_hit.collider.CompareTag("ground"))
            {
                if (_referenceManager.modeManager.ActiveMode != Modes.Instantiate)
                {
                    EnableInstantiateMode();
                    return;
                }
                
                _referenceManager.instantiateManager.Instantiate(_hit.point);
            }
        }

        private void EnableEditMode()
        {
            if (_referenceManager.modeManager.ActiveMode != Modes.Edit)
            {
                _referenceManager.modeManager.ChangeMode(Modes.Edit);
                CustomDebug.Log("Edit Mode Enabled");
            }
        }

        private void EnableInstantiateMode()
        {
            if (_referenceManager.modeManager.ActiveMode != Modes.Instantiate)
            {
                _referenceManager.modeManager.ChangeMode(Modes.Instantiate);
                CustomDebug.Log("Instantiate Mode Enabled");
            }
        }
    }
}
