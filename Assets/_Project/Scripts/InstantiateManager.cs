using System;
using UnityEngine;

namespace _Project.Scripts
{
    public enum InstantiatableTypes
    {
        Cube,
        Sphere
    }
    
    public class InstantiateManager : MonoBehaviour
    {
        [SerializeField] private Instantiatable cubePrefab;
        [SerializeField] private Vector3 customCubeScale = Vector3.one;
        
        [SerializeField, Space] private Instantiatable spherePrefab;
        [SerializeField] private Vector3 customSphereScale = Vector3.one;
        
        [Space]
        [SerializeField] private InstantiatableTypes selectedType;

        public InstantiatableTypes SelectedTypes => selectedType;

        public static event Action<ISelectable> OnNewInstantiated;

        private Vector3 _targetPos;
        

        private ReferenceManager _referenceManager;
        

        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
        }

        public void ChangeType(InstantiatableTypes newType)
        {
            selectedType = newType;
        }


        [ContextMenu("Instantiate")]
        public void Instantiate(Vector3 targetPoint)
        {
            _targetPos = targetPoint;
            
            if (selectedType == InstantiatableTypes.Cube)
            {
                InstantiateCube();
            }
            else if (selectedType == InstantiatableTypes.Sphere)
            {
                InstantiateSphere();
            }
        }


        private void InstantiateCube()
        {
            Transform cube = Instantiate(cubePrefab, _referenceManager.objectsParent).transform;
            cube.localScale = customCubeScale;

            Vector3 yOffset = new Vector3(0, customCubeScale.y / 2.0f, 0);
            
            cube.localPosition = _targetPos+yOffset;
            
            OnNewInstantiated?.Invoke(cube.GetComponent<ISelectable>());
        }

        private void InstantiateSphere()
        {
            Transform sphere = Instantiate(spherePrefab, _referenceManager.objectsParent).transform;
            sphere.localScale = customSphereScale;
            
            Vector3 yOffset = new Vector3(0, customSphereScale.y / 2.0f, 0);
            sphere.localPosition = _targetPos+yOffset;
            
            OnNewInstantiated?.Invoke(sphere.GetComponent<ISelectable>());
        }
    }
}
