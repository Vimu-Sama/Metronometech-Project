using UnityEngine;

//child object script for positioning
public class Bulb : MonoBehaviour
{
    [SerializeField] private int maxMultiplierY = 2;
    private int childScale = 1;
    private Vector3 tempScaleVector = Vector3.one;
    private void Awake()
    {
        childScale = Random.Range(1, maxMultiplierY + 1);
        tempScaleVector.x = gameObject.transform.localScale.x;
        tempScaleVector.y = gameObject.transform.localScale.y * childScale;
        tempScaleVector.z = gameObject.transform.localScale.z;
        gameObject.transform.localScale = tempScaleVector;
    }

    public int ChildScale
    {
        get { return childScale; }
    }

}
