using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] AssetReference layer1; // Tham chiếu đến đối tượng cần instantiate

    void Start()
    {
        // Gọi hàm để instantiate đối tượng khi bắt đầu
        InstantiateLayer();
    }

    void InstantiateLayer()
    {
        // Tải đối tượng từ AssetReference
        layer1.InstantiateAsync(transform).Completed += OnLayerInstantiated;
    }

    void OnLayerInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject instantiatedLayer = handle.Result; // Lấy đối tượng đã instantiate
            instantiatedLayer.transform.SetParent(transform); // Đặt đối tượng con là con của đối tượng chứa script này
            instantiatedLayer.transform.localPosition = Vector3.zero; // Đặt vị trí của đối tượng con
        }
        else
        {
            Debug.LogError("Failed to instantiate layer: " + handle.OperationException);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Có thể thêm logic khác ở đây nếu cần
    }
}