using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Toggle cubeToggle;
        [SerializeField] private Toggle sphereToggle;
        [SerializeField] private RectTransform infoUi;

        private ReferenceManager _referenceManager;


        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
        }

        private void OnEnable()
        {
            cubeToggle.onValueChanged.AddListener(OnCubeToggleValueChanged);
            sphereToggle.onValueChanged.AddListener(OnSphereToggleValueChanged);

            ModeManager.OnModeChange += OnModeChange;
        }

        private void OnDisable()
        {
            cubeToggle.onValueChanged.RemoveListener(OnCubeToggleValueChanged);
            sphereToggle.onValueChanged.RemoveListener(OnSphereToggleValueChanged);
            
            ModeManager.OnModeChange -= OnModeChange;
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

        private void OnModeChange()
        {
            if (_referenceManager.modeManager.ActiveMode == Modes.Edit)
            {
                ShowInfo();
            }
            else
            {
                HideInfo();
            }
        }

        private void ShowInfo()
        {
            infoUi.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutExpo);
        }

        private void HideInfo()
        {
            infoUi.DOAnchorPosX(-350, 0.5f).SetEase(Ease.InExpo);
        }
    }
}
