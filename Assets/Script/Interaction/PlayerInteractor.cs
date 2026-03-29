using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("Titik referensi untuk mendeteksi interaksi (bisa Player itu sendiri atau object kecil didepannya)")]
    public Transform interactorSource; 
    public float interactRange = 1.5f; // Jarak deteksi
    
    private PlayerMovementTopDown _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovementTopDown>();
        
        if (interactorSource == null) 
            interactorSource = transform; // Default ke diri sendiri
    }

    private void Update()
    {
        // Kalau player terkunci (karena sedang main laptop/UI dll), kita tidak usah mendeteksi E
        if (_playerMovement != null && !_playerMovement.canMove)
            return;

        // Cek jika menekan tombol E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        // Mendeteksi semua objek di sekeliling interactorSource dalam radius
        Collider[] colliders = Physics.OverlapSphere(interactorSource.position, interactRange);
        
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                // Jika ketemu barang yang mengimplementasikan IInteractable, panggil fungsinya
                interactable.Interact(this.gameObject);
                
                // Break agar player tidak berinteraksi langsung dengan >1 benda jika bertumpuk
                break; 
            }
        }
    }

    // Tampilkan bola kuning agar lebih gampang atur radiusnya di Editor / Scene
    private void OnDrawGizmosSelected()
    {
        if (interactorSource == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactorSource.position, interactRange);
    }
}
