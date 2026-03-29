using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;        // Drag objek Player ke sini
    
    [Header("Posisi")]
    public Vector3 offset = new Vector3(0f, 10f, -6f); // Jarak tinggi dan mundur (Z negatif)
    
    [Header("Sudut Pandang")]
    public float cameraPitch = 60f; // Sudut kamera menunduk (60-70 untuk Top-down, 30-45 untuk Isometric)
    
    [Header("Kehalusan")]
    public float smoothSpeed = 0.125f; // Semakin kecil semakin lambat kameranya mengikuti

    void Start()
    {
        // Set sudut kamera di awal
        transform.rotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Tentukan posisi target kamera
        Vector3 desiredPosition = target.position + offset;
        
        // Haluskan pergerakan kamera (agar tidak kaku)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Terapkan posisi baru
        transform.position = smoothedPosition;
    }
}