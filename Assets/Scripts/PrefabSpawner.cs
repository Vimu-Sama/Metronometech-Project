using UnityEngine;
//prefab spawenr script
public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnStartTransformX = 0f;
    [SerializeField] private float spawnStartTransformY = 0f;
    [SerializeField] private float spawnStartTransformZ = 0f;
    private int[,] prefabGrid = new int[3, 3];
    private float tempVar = 0;
    private void Start()
    {
        tempVar = spawnStartTransformX;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject prefabGameObject = GameObject.Instantiate<GameObject>(prefab);
                prefabGameObject.transform.position =
                    new Vector3(spawnStartTransformX, spawnStartTransformY, spawnStartTransformZ);
                ParentManager.Instance.CreateReference(prefabGameObject.GetComponentInChildren<Bulb>(), i, j); //passing the child object reference to the ParentManager
                spawnStartTransformX += 1f;
            }
            spawnStartTransformX = tempVar;
            spawnStartTransformY -= 1f;
        }
        ParentManager.Instance.CheckBulbMultiplier(); //calling the Final function for arranging the bulbs properly
    }
}
