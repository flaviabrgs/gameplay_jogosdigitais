using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorGameOver : MonoBehaviour
{
    public GameObject panelGameOver;

    public void AtivarGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f; // Para o jogo quando morre
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Index 0 é o seu Menu Principal
    }
}
