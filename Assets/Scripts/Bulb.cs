using UnityEngine;

public class Bulb : MonoBehaviour
{
    [SerializeField] private int maxMultiplierY = 2;
    private int childScale = 1;
    private Vector3 tempScaleVector= Vector3.one; 
    private void Start()
    {
        childScale = Random.Range(1, maxMultiplierY+1);
        ParentManager.Instance.SetMultiplierForCell(childScale);
        tempScaleVector.x = gameObject.transform.localScale.x;
        tempScaleVector.y = gameObject.transform.localScale.y * childScale;
        tempScaleVector.z = gameObject.transform.localScale.z;
        gameObject.transform.localScale = tempScaleVector; 
    }
}
