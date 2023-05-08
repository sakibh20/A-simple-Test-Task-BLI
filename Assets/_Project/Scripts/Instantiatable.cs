using _Project.Core.Custom_Debug_Log.Scripts;
using UnityEngine;

namespace _Project.Scripts
{
    public class Instantiatable : MonoBehaviour, ISelectable
    {
        public void OnSelect()
        {
            CustomDebug.LogWarning("Manage On Select");
        }
    }
}
