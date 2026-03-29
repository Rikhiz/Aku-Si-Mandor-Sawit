using UnityEngine;

public class LaptopInteractable : MonoBehaviour, IInteractable
{
    [Header("Referensi UI")]
    public LaptopUIManager laptopUIManager;

    [Header("Pengaturan Prompt Text")]
    [SerializeField] private string promptMessage = "Buka Laptop";

    public void Interact(GameObject interactor)
    {
        // Coba kunci pergerakan player
        PlayerMovementTopDown playerMovement = interactor.GetComponent<PlayerMovementTopDown>();

        if (playerMovement != null)
        {
            // Matikan pergerakan saat ini
            playerMovement.canMove = false;
        }

        // Panggil UI manager untuk tampilkan laptop
        if (laptopUIManager != null)
        {
            laptopUIManager.OpenLaptopUI(playerMovement);
        }
        else
        {
            Debug.LogError("Laptop UI Manager belum dikaitkan (di-drag & drop) di dalam inspektor milik " + gameObject.name, gameObject);
        }
    }

    public string GetInteractPrompt()
    {
        return promptMessage; // Ini dikembalikan ketika nanti ada sistem UI Pop-up
    }
}
