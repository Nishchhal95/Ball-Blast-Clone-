using UnityEngine;

namespace InputControls
{
    public class TouchInput : MonoBehaviour, IInput
    {
        [SerializeField] private Camera mainCamera;
        public Vector2 MouseRealWorldPos { get; private set; }
        public bool isShooting { get;  private set;}

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
            isShooting = false;
            
            //Touch Position
            if (Input.touchCount <= 0) return;
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            MouseRealWorldPos = mainCamera.ScreenToWorldPoint(touchPosition);

            isShooting = true;
        }
    }
}
