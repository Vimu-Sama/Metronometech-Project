using UnityEngine;

public enum RotationEnum //enum for storing rotation of particular bulb
{
    up,
    left,
    right,
    down
}

public class Triplet //custom made class
{
    private int firstvar;
    private int secondvar;
    RotationEnum rotateDir;

    public Triplet(int _first, int _second, RotationEnum _rotateDir)
    {
        firstvar = _first;
        secondvar = _second;
        rotateDir = _rotateDir;
    }

    public int Firstvar
    {
        get
        {
            return firstvar;
        }
    }

    public int Secondvar
    {
        get
        {
            return secondvar;
        }
    }

    public RotationEnum RotateDir
    {
        get
        {
            return rotateDir;
        }
    }

};



public class ParentManager : GenericSingleton<ParentManager>  // main parent manager script
{
    [SerializeField] private Color parentColor;
    private Bulb[,] bulbs = new Bulb[3, 3];
    private bool[,] alreadyRemoved = new bool[3, 3];
    private int[] arX = { 0, -1, 1, 0 };
    private int[] arY = { 1, 0, 0, -1 };
    private RotationEnum[] arEnum = { RotationEnum.right, RotationEnum.up, RotationEnum.down, RotationEnum.left };
    private bool tempBool = false;

    public void CreateReference(Bulb temp, int row, int col) //referencing all child objects
    {
        if (temp == null)
            return;
        bulbs[row, col] = temp;
        alreadyRemoved[row, col] = false;
    }


    public void CheckBulbMultiplier() //check the size of bulb
    {
        Triplet tempTriplet;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (bulbs[i, j].ChildScale > 1)
                {
                    tempTriplet = PseudoBfs(i, j);
                    if (tempTriplet.Firstvar == -1)
                    {
                        Debug.Log("Rotation not possible for this box! " + "row: " + i + " col: " + j);
                        bulbs[i, j].transform.parent.GetComponentInParent<SpriteRenderer>().color = Color.black; //disabling the parent slot with anomly
                        bulbs[i, j].GetComponent<SpriteRenderer>().enabled = false;
                        continue;
                    }
                    else
                    {
                        bulbs[i, j].transform.parent.GetComponentInParent<SpriteRenderer>().color = parentColor;
                        bulbs[tempTriplet.Firstvar, tempTriplet.Secondvar].transform.parent.GetComponentInParent<SpriteRenderer>().color = parentColor;
                        bulbs[tempTriplet.Firstvar, tempTriplet.Secondvar].GetComponent<SpriteRenderer>().enabled = false;
                        alreadyRemoved[i, j] = true; //setting true, if particularr cell is edited after spawning
                        alreadyRemoved[tempTriplet.Firstvar, tempTriplet.Secondvar] = true;
                        RotateAndSetPosition(i, j, tempTriplet);
                    }
                }
            }
        }
    }
    private Triplet PseudoBfs(int row, int col) //checking all cells around the current cell for exapnding the bulb on
    {
        for (int i = 0; i < 4; i++)
        {
            tempBool = BfsHelper(row + arX[i], col + arY[i]);
            if (tempBool)
                return new Triplet(row + arX[i], col + arY[i], arEnum[i]);
        }
        return new Triplet(-1, -1, RotationEnum.up);
    }

    private bool BfsHelper(int row, int col) //helper function for checking validation, if the current cell can be used or not
    {
        if (row < 0 || row >= 3 || col < 0 || col >= 3 || bulbs[row, col].ChildScale > 1 || alreadyRemoved[row, col])
            return false;
        return true;
    }

    private void RotateAndSetPosition(int row, int col, Triplet _tempTriplet) // for rotating and setting position of child/bulb
    {
        if (_tempTriplet.RotateDir == RotationEnum.up)
        {
            bulbs[row, col].transform.position += new Vector3(0, 0.5f, 0);
            bulbs[row, col].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (_tempTriplet.RotateDir == RotationEnum.left)
        {
            bulbs[row, col].transform.position += new Vector3(-0.5f, 0, 0);
            bulbs[row, col].transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (_tempTriplet.RotateDir == RotationEnum.right)
        {
            bulbs[row, col].transform.position += new Vector3(0.5f, 0, 0);
            bulbs[row, col].transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        else
        {
            bulbs[row, col].transform.position += new Vector3(0, -0.5f, 0);
            bulbs[row, col].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
