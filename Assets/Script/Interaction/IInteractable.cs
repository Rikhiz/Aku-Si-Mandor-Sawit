using UnityEngine;

public interface IInteractable
{
    // Dipanggil saat player menekan tombol interaksi
    void Interact(GameObject interactor);
    
    // Berguna jika nanti kamu mau buat teks pop-up seperti "Tekan E untuk menggunakan laptop"
    string GetInteractPrompt();
    
}
