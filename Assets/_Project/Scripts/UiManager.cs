using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Toggle cubeToggle;
        [SerializeField] private Toggle sphereToggle;

        private ReferenceManager _referenceManager;


        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
        }

        private void OnEnable()
        {
            cubeToggle.onValueChanged.AddListener(OnCubeToggleValueChanged);
            sphereToggle.onValueChanged.AddListener(OnSphereToggleValueChanged);
        }

        private void OnDisable()
        {
            cubeToggle.onValueChanged.RemoveListener(OnCubeToggleValueChanged);
            sphereToggle.onValueChanged.RemoveListener(OnSphereToggleValueChanged);
        }

        private void OnCubeToggleValueChanged(bool isOn)
        {
            if (isOn)
            {
                if (_referenceManager.instantiateManager.SelectedTypes != InstantiatableTypes.Cube)
                {
                    _referenceManager.instantiateManager.ChangeType(InstantiatableTypes.Cube);   
                }
            }
        }
        
        private void OnSphereToggleValueChanged(bool isOn)
        {
            if (isOn)
            {
                if (_referenceManager.instantiateManager.SelectedTypes != InstantiatableTypes.Sphere)
                {
                    _referenceManager.instantiateManager.ChangeType(InstantiatableTypes.Sphere);   
                }
            }
        }
    }
}
