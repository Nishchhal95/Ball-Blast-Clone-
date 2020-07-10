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

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float bulletSpeed = 20f;
        [SerializeField] private float MAX_GAP_BETWEEN_SHOTS = .2f;
        [SerializeField] private float currentTimeLeftForNextShot = 0f;

        [SerializeField] private Animator m_Animator;

        // Start is called before the first frame update
        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            currentTimeLeftForNextShot = MAX_GAP_BETWEEN_SHOTS;
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
            CheckAndExecuteShoot();
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
        
        private void CheckAndExecuteShoot()
        {
            currentTimeLeftForNextShot -= Time.deltaTime;
            if (gameInput.isShooting && currentTimeLeftForNextShot <= 0)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            currentTimeLeftForNextShot = MAX_GAP_BETWEEN_SHOTS;
            m_Animator.SetTrigger("Shoot");
            //Spawn a bullet and Shoot
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
            bulletRb.isKinematic = false;
            bulletRb.gravityScale = 0;
            bulletRb.velocity = Vector2.up * bulletSpeed;
            
            Destroy(bullet, 2f);
        }
        
    }
}
