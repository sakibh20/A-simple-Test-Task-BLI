using UnityEngine;

namespace _Project.Core.Custom_Debug_Log.Scripts
{
    public static class CustomDebug
    {
        private static CustomDebugSettings CustomDebugSettings;

        [RuntimeInitializeOnLoadMethod]
        static void LoadSettings()
        {
            CustomDebugSettings = Resources.Load<CustomDebugSettings>("Custom Debug/Custom Debug Settings");
            
            if (!CustomDebugSettings)
            {
                Debug.LogError("Custom Debug Log Settings Not Found. Please Ensure That, The Resource Path Is Correct");
            }
        }

        public static void Log(string message)
        {
            if (!CustomDebugSettings)
            {
                Debug.LogError("Custom Debug Log Settings Not Found. Please Ensure That, The Resource Path Is Correct");
                return;
            }
            
            if (CustomDebugSettings.showLog)
            {
                Debug.Log(message % CustomDebugColorize.Green % CustomDebugFontFormat.Bold );
            }
        }
        
        public static void LogWarning(string message)
        {
            if (!CustomDebugSettings)
            {
                Debug.LogError("Custom Debug Log Settings Not Found. Please Ensure That, The Resource Path Is Correct");
                return;
            }

            if (CustomDebugSettings.showLog)
            {
                Debug.Log(message % CustomDebugColorize.Yellow % CustomDebugFontFormat.Bold );
            }
        }

        public static void LogError(string message)
        {
            if (!CustomDebugSettings)
            {
                Debug.LogError("Custom Debug Log Settings Not Found. Please Ensure That, The Resource Path Is Correct");
                return;
            }

            if (CustomDebugSettings.showLog)
            {
                Debug.LogError(message % CustomDebugColorize.RedNice % CustomDebugFontFormat.Bold);
            }
        }
    }
}
