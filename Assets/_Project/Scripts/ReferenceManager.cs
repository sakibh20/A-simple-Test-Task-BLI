using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ReferenceManager : MonoBehaviour
    {
        [Header("Scripts")] 
        public InstantiateManager instantiateManager;
        public ModeManager modeManager;
        public SelectionManager selectionManager;
        
        [Space, Header("Objects")]
        public Transform objectsParent;
        
        public static ReferenceManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
