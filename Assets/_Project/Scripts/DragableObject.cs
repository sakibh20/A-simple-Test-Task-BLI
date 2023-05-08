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
        private Instantiatable _instantiatable;

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
            
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
            Vector3 position = transform.position; // Take current position of this draggable object as Plane's Origin
            Vector3 planesNormal = -_mainCamera.transform.forward; // Take current negative camera's forward as Plane's Normal
            float t = Vector3.Dot(position - ray.origin, planesNormal) / Vector3.Dot(ray.direction, planesNormal); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
            Vector3 newPosition = ray.origin + ray.direction * t; // Find the new point.


            Vector3 moveDirection = (newPosition-transform.position);
            
            // Debug.DrawRay(transform.position + (moveDirection), transform.TransformDirection(Vector3.down) * 50, Color.yellow);
            
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (moveDirection*2), transform.TransformDirection(-Vector3.up), out hit, 50))
            {
                if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("selectable"))
                {
                    transform.position = new Vector3(newPosition.x, transform.localPosition.y,newPosition.z);
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _instantiatable.PauseTween();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _instantiatable.ResumeTween();
        }
    }
}
