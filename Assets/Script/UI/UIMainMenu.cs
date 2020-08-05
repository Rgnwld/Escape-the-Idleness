using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] PlayerMovement pm;

    public void Sair ()
    {
        Application.Quit();
    }

    public void Iniciar ()
    {
        SceneManager.LoadScene(1);
    }

    public void Creditos ()
    {
        GameObject creditPainel = transform.Find("PainelCreditos").gameObject;
        creditPainel.SetActive(!creditPainel.activeSelf);
    }

    public void creditToDunjo ()
    {
        Application.OpenURL("https://arks.itch.io/dungeon-platform-tileset");
    }
}
