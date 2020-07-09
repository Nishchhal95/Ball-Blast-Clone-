using CameraControls;
using InputControls;
using UnityEngine;

namespace PlayerControls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 mouseRealWorldPos;
        [SerializeField] private float canonSpeed = 10f;
        [SerializeField] private Transform _transform;
    
        //Some Max Limits
        [SerializeField] private float myPlayerWidth;
        [SerializeField] private float maxApproachableLimitX = 0;

        [SerializeField] private IInput gameInput;

        // Start is called before the first frame update
        private void Start()
        {
            _transform = transform;
            gameInput = GetComponent<IInput>();

            myPlayerWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        
            //Update Approachable Screen width
            maxApproachableLimitX = CameraManager.MAX_SCREEN_WIDTH_IN_WORLD_UNITS - myPlayerWidth;
        }

        // Update is called once per frame
        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            //Grab Input
            mouseRealWorldPos = gameInput.MouseRealWorldPos;
        
            Vector2 finalPlayerPos;
            if (Vector2.Distance(transform.position, mouseRealWorldPos) > 0.01f)
            {
                finalPlayerPos = Vector3.Lerp(transform.position, mouseRealWorldPos, Time.deltaTime * canonSpeed);
            }

            else
            {
                finalPlayerPos = mouseRealWorldPos;
            }
        
        
            //Player Position Set
            var finalXPos = Mathf.Clamp(finalPlayerPos.x, -maxApproachableLimitX,
                maxApproachableLimitX);
            _transform.position = new Vector3(finalXPos, transform.position.y);
        }
    }
}
