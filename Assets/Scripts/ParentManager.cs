using UnityEngine;

public class ParentManager : GenericSingleton<ParentManager>
{

    private int[,] cellChildMultiplierRecord = new int[3, 3];
    private int row = 0;
    private int col = 0;
    private int num = 0;
    protected override void Awake()
    {
        base.Awake();
        for(int i=0;i<3;i++)
        {
            for (int j = 0; j < 3; j++)
                cellChildMultiplierRecord[i, j] = 1;
        }
    }


    public void SetMultiplierForCell(int _multiplier)
    {
        if (num > 8)
            return;
        row = num / 3;
        col = num % 3;
        cellChildMultiplierRecord[row, col] = _multiplier;
        num++;
    }


    private void Update()
    {
        //check if current gameobject is being overlapped
        //by the child of later spawned gameobject
        //for it, we will have to store the
    }
}
