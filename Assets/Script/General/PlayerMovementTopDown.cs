using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public CharacterController controller;
    
    public float speed = 8f;              // Kecepatan lari
    public float turnSmoothTime = 0.05f;  // Seberapa cepat karakter berputar (lebih kecil lebih cepat)
    float turnSmoothVelocity;

    public float gravity = -20f;          // Gravitasi yang lebih kuat agar terasa padat
    public float jumpHeight = 1.0f;
    Vector3 velocity;
    
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    [Header("State")]
    public bool canMove = true;

    void Update()
    {
        // 1. Cek Ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Menjaga agar tetap nempel tanah
        }

        // Jika pergerakan dikunci (misal sedang UI laptop), hentikan mengambil input
        if (!canMove)
        {
            // Memastikan gravitasi tetap berjalan
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            return;
        }

        // 2. Ambil Input (Tanpa normalisasi untuk akselerasi analog yang halus)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // 3. Pergerakan dan Rotasi
        if (direction.magnitude >= 0.1f)
        {
            // Menghitung sudut rotasi berdasarkan input saja (bukan kamera)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Gerakan karakter ke arah target (W = Maju ke utara, S = Mundur ke selatan)
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // 4. Lompat (Opsional untuk gaya Overcooked, tapi Animal Crossing ada lompatan kecil)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 5. Terapkan Gravitasi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}