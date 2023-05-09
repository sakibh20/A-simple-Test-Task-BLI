using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Project.Scripts
{
    public class SaveSystem : MonoBehaviour
    {
        public AllItemsData allSelectableItems;
        private readonly string _fileName = "ItemInfo.json";
        private string _path;

        public static event Action<AllItemsData> InfoLoaded; 

        private void Awake()
        {
            _path = Application.persistentDataPath + "/" + _fileName;
        }

        private void Start()
        {
            LoadInfo();
        }

        private void OnEnable()
        {
            SelectionManager.OnListRefreshed += RefreshSelectableList;
        }

        private void OnDisable()
        {
            SelectionManager.OnListRefreshed -= RefreshSelectableList;
        }

        private void RefreshSelectableList(List<ISelectable> allSelectables)
        {
            allSelectableItems = new AllItemsData();
            allSelectableItems.allSelectableItems = new List<SelectableItem>();
            

            for (int i = 0; i < allSelectables.Count; i++)
            {
                SelectableItem item = new SelectableItem();
                
                Transform itemTransform = allSelectables[i].Transform();

                item.position = itemTransform.position;
                item.rotation = itemTransform.localEulerAngles;
                item.scale = itemTransform.localScale;
                item.isCube = allSelectables[i].Type() == InstantiatableTypes.Cube;
                
                allSelectableItems.allSelectableItems.Add(item);
            }

            StoreInfo();
        }

        private void StoreInfo()
        {
            var json = JsonUtility.ToJson(allSelectableItems);
            File.WriteAllText(_path, json);
        }

        private void LoadInfo()
        {
            if (File.Exists(_path))
            {
                var jsonTextFile = File.ReadAllText(_path);
                allSelectableItems = JsonUtility.FromJson<AllItemsData>(jsonTextFile);
                
                InfoLoaded?.Invoke(allSelectableItems);
            }
        }
    }

    [Serializable]
    public class AllItemsData
    {
        public List<SelectableItem> allSelectableItems;
    }
    
    [Serializable]
    public class SelectableItem
    {
        public bool isCube;
        public Vector3 scale;
        public Vector3 position;
        public Vector3 rotation;
    }
}
