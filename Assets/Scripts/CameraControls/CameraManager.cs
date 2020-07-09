using UnityEngine;

namespace CameraControls
{
    public class CameraManager : MonoBehaviour
    {
    
        [SerializeField] private Camera mainCamera;

        //Some Max Limits
        public static float MAX_SCREEN_WIDTH_IN_WORLD_UNITS = 0;

        private void Awake()
        {
            mainCamera = Camera.main;
        
            //Update Screen Width
            if (mainCamera != null)
            {
                MAX_SCREEN_WIDTH_IN_WORLD_UNITS = Mathf.Abs(mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).x);
            }
        }
    }
}
