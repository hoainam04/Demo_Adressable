using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SpawnerObject : MonoBehaviour
{
    [SerializeField] AssetReference objPlayer;
    [SerializeField] AssetLabelReference objRocks;
    [SerializeField] Button Button;
    void Start()
    {
        SpawnObject();
        SpawnObjects();
        Button.onClick.AddListener(LoadScene);
    }

    private void SpawnObject()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(objPlayer);
        handle.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            Instantiate(task.Result);
            Debug.Log("Instantiate Object: " + objPlayer.ToString());
        };
    }
    private void SpawnObjects()
    {
        var handle = Addressables.LoadAssetsAsync<GameObject>(objRocks, (GameObject result) =>
        {
            Instantiate(result);
        }
        );
    }
    private void LoadScene()
    {
        Addressables.LoadSceneAsync("Assets/Scenes/SampleScene.unity");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene();
        }
    }
}
