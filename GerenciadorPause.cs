using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorPause : MonoBehaviour
{
    public GameObject painelPause;
    private bool pausado = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado) Retomar();
            else Pausar();
        }
    }

    public void Pausar()
    {
        painelPause.SetActive(true); 
        Time.timeScale = 0f;       
        pausado = true;
    }

    public void Retomar()
    {
        painelPause.SetActive(false);
        Time.timeScale = 1f;         
        pausado = false;
    }

    public void IrParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
