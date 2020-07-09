using UnityEngine;

namespace InputControls
{
    public class TouchInput : MonoBehaviour, IInput
    {
        [SerializeField] private Camera mainCamera;
        public Vector2 MouseRealWorldPos { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            PlayerInputControls();
        }
    
        private void PlayerInputControls()
        {
            //Touch Position
        
            //We need to assign touch position
        
            //For reference Look MouseInput.cs
            //MouseRealWorldPos = 
        }
    }
}
