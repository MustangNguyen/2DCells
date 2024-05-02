using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testpopup : MonoBehaviour
{
    private void Start()
    {
        string prefabName = "Prefab/UI/PopupPrefabs/PopupConfirmYesNo";
                GameObject prefab = Resources.Load<GameObject>(prefabName);

        // Kiểm tra xem prefab có được tải thành công hay không
        if (prefab != null)
        {
            Debug.Log("Prefab đã được tải thành công.");

            // Lấy đường dẫn của prefab
            string prefabPath = GetPrefabPath(prefab);
            Debug.Log("Đường dẫn của prefab: " + prefabPath);
        }
        else
        {
            Debug.Log("Không thể tải prefab.");
        }
        string GetPrefabPath(GameObject prefab)
    {
        // Lấy đường dẫn tương đối của prefab
        string path = UnityEditor.AssetDatabase.GetAssetPath(prefab);

        // Chuyển đổi đường dẫn tương đối thành đường dẫn tuyệt đối
        string absolutePath = System.IO.Path.Combine(Application.dataPath, path);

        return absolutePath;
    }
    }
}
