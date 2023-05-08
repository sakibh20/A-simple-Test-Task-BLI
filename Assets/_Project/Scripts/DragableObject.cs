using _Project.Core.Custom_Debug_Log.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class DragableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Camera _mainCamera;
        public bool canDrag;
        public bool translate;
        private Instantiatable _instantiatable;
        private Vector3 _initialMousePosition;

        private void Start ()
        {
            _instantiatable = GetComponent<Instantiatable>();    
            
            _mainCamera = Camera.main;
            if (_mainCamera != null && _mainCamera.GetComponent<PhysicsRaycaster>() == null)
            {
                CustomDebug.LogError("Camera doesn't have a physics raycaster.");
            }
        }
	
        public void OnDrag(PointerEventData eventData)
        {
            if (!canDrag)
            {
                return;
            }


            if (translate)
            {
                Translate();
            }
            else
            {
                Rotate(eventData);
            }
        }

        private void Translate()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
            Vector3 position = transform.position; // Take current position of this draggable object as Plane's Origin
            Vector3 planesNormal = -_mainCamera.transform.forward; // Take current negative camera's forward as Plane's Normal
            float t = Vector3.Dot(position - ray.origin, planesNormal) / Vector3.Dot(ray.direction, planesNormal); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
            Vector3 newPosition = ray.origin + ray.direction * t; // Find the new point.


            Vector3 moveDirection = (newPosition-transform.position);


            RaycastHit hit;
            if (Physics.Raycast(transform.position + (moveDirection*2), transform.TransformDirection(-Vector3.up), out hit, 50))
            {
                if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("selectable"))
                {
                    transform.position = new Vector3(newPosition.x, transform.localPosition.y,newPosition.z);
                }
            }
        }

        private void Rotate(PointerEventData eventData)
        {
            Vector3 currentMousePosition = eventData.position;
            Vector3 mouseDelta = currentMousePosition - _initialMousePosition;
            float rotationY = -mouseDelta.x * Time.deltaTime * 250.0f;
            transform.Rotate(0, rotationY, 0);
            _initialMousePosition = currentMousePosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!canDrag)
            {
                return;
            }
            
            _instantiatable.PauseTween();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _initialMousePosition = Vector3.zero;
            
            if (!canDrag)
            {
                return;
            }
            
            _instantiatable.ResumeTween();
        }
    }
}
