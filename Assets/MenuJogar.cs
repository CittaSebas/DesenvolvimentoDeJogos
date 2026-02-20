using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuJogar : MonoBehaviour
{
    public void jogar()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
