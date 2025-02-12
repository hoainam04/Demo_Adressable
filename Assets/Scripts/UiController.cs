using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] Button Button;
    void Start()
    {
        Button.onClick.AddListener(LoadScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LoadScene()
    {
        Addressables.LoadSceneAsync("Assets/Scenes/Main.unity");
    }
}
