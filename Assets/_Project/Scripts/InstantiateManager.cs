using System;
using _Project.Core.Custom_Debug_Log.Scripts;
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

        private void OnEnable()
        {
            SaveSystem.InfoLoaded += OnInfoLoaded;
        }

        private void OnDisable()
        {
            SaveSystem.InfoLoaded -= OnInfoLoaded;
        }


        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
        }

        public void ChangeType(InstantiatableTypes newType)
        {
            selectedType = newType;
        }

        private void OnInfoLoaded(AllItemsData allInfo)
        {
            if (_referenceManager == null)
            {
                _referenceManager = FindObjectOfType<ReferenceManager>();
            }
            for (int i = 0; i < allInfo.allSelectableItems.Count; i++)
            {
                SelectableItem item = allInfo.allSelectableItems[i];

                if (item.isCube)
                {
                    InstantiateCube(item.position, item.rotation, item.scale);
                }
                else
                {
                    InstantiateSphere(item.position, item.rotation, item.scale);
                }
            }
        }

        
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

            ISelectable selectable = cube.GetComponent<ISelectable>();
            
            Instantiatable instantiatable = cube.GetComponent<Instantiatable>();
            instantiatable.type = InstantiatableTypes.Cube;

            OnNewInstantiated?.Invoke(selectable);
        }
        
        private void InstantiateCube(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Transform cube = Instantiate(cubePrefab, _referenceManager.objectsParent).transform;
            cube.localScale = scale;

            cube.localPosition = position;
            cube.localEulerAngles = rotation;

            ISelectable selectable = cube.GetComponent<ISelectable>();
            
            Instantiatable instantiatable = cube.GetComponent<Instantiatable>();
            instantiatable.type = InstantiatableTypes.Cube;

            OnNewInstantiated?.Invoke(selectable);
        }

        private void InstantiateSphere()
        {
            Transform sphere = Instantiate(spherePrefab, _referenceManager.objectsParent).transform;
            sphere.localScale = customSphereScale;
            
            Vector3 yOffset = new Vector3(0, customSphereScale.y / 2.0f, 0);
            sphere.localPosition = _targetPos+yOffset;
            
            ISelectable selectable = sphere.GetComponent<ISelectable>();
            
            Instantiatable instantiatable = sphere.GetComponent<Instantiatable>();
            instantiatable.type = InstantiatableTypes.Sphere;
            
            OnNewInstantiated?.Invoke(selectable);
        }
        
        private void InstantiateSphere(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Transform sphere = Instantiate(spherePrefab, _referenceManager.objectsParent).transform;
            sphere.localScale = scale;
            sphere.localEulerAngles = rotation;
            
            sphere.localPosition = position;
            
            ISelectable selectable = sphere.GetComponent<ISelectable>();
            
            Instantiatable instantiatable = sphere.GetComponent<Instantiatable>();
            instantiatable.type = InstantiatableTypes.Sphere;
            
            OnNewInstantiated?.Invoke(selectable);
        }
    }
}
