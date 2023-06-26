using UnityEngine;
using GM;
using UnityEngine.UI;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] Image blackImage;

    void Start()
    {
        GameFuncs.BlackImage = blackImage;
    }
}
