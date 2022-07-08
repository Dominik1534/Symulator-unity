using UnityEngine;
using UnityEngine.SceneManagement;

public class WybórSceny : MonoBehaviour
{

    public void ŁadowanieSceny2(string name)
    {
        SceneManager.LoadScene(name);

    }
}
