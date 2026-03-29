using UnityEngine;

public class LaptopUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject laptopCanvas; // Drag canvas utama UI mu ke sini didalam inspector

    // Simpan data player sehingga bisa kembalikan pergerakannya
    private PlayerMovementTopDown _lastPlayer;

    private void Start()
    {
        // Pastikan Canvas ini sembunyi saat game baru mulai
        if (laptopCanvas != null)
        {
            laptopCanvas.SetActive(false);
        }
    }

    public void OpenLaptopUI(PlayerMovementTopDown player)
    {
        _lastPlayer = player;
        
        // Aktifkan tampilan layar laptop
        laptopCanvas.SetActive(true);

        // Munculkan kursor mouse untuk klik tombol
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // NOTE: Fungsi ini harus kamu panggil di Event OnClick (Button) nya Tombol [X]
    public void CloseLaptopUI()
    {
        // Tutup tampilan layar laptop
        Debug.Log("BUTTON KEKLIK");
        laptopCanvas.SetActive(false);

        // Buka lagi gembok pergerakan player
        if (_lastPlayer != null)
        {
            _lastPlayer.canMove = true;
            _lastPlayer = null;
        }
    }

    // ==== Contoh jika ada Aplikasi 1 ==== 
    public void OpenApp_Word()
    {
        Debug.Log("Membuka Aplikasi Word di Laptop...");
    }

    public void Update() 
    {
        // Opsional: Tutup laptop juga jika menekan ESCAPE (Selain tombol merah UI)
        if (laptopCanvas != null && laptopCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLaptopUI();
        }
    }
}
