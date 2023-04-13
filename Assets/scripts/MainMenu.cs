using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _PanelLNH;
    [SerializeField] GameParams _easy;
    [SerializeField] GameParams _normal;
    [SerializeField] GameParams _hard;

    public static GameParams GameParams;
    
    private void Start()
    {
        _PanelLNH.SetActive(false);
    }
    
    public void pley()
    {
        _PanelLNH.SetActive(true);
    }
    
    public void leicht()
    {
        GameParams = _easy;
        SceneManager.LoadScene(1);
    }
    
    public void normal()
    {
        GameParams = _normal;
        SceneManager.LoadScene(1);
    }
    
    public void hard()
    {
        GameParams = _hard;
        SceneManager.LoadScene(1);
    }
}
