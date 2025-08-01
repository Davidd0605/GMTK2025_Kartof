using UnityEngine;

public class ItemInteractionMatrix
{
    private static readonly int[,] interactionMatrix;

    private const int MatrixSize = 10;

    static ItemInteractionMatrix()
    {
        interactionMatrix = new int[MatrixSize, MatrixSize];

        for (int i = 0; i < MatrixSize; i++)
        {
            for (int j = 0; j < MatrixSize; j++)
            {
                interactionMatrix[i, j] = -1;
            }
        }
        //Add new combinations here
        interactionMatrix[1,2] = interactionMatrix[2,1] = 3; //axe = wood + stone


    }

    public static int GetCombinationResult(int itemCode1, int itemCode2)
    {
        //robustness brotha
        if (itemCode1 < 0 || itemCode2 < 0 ||
            itemCode1 >= MatrixSize || itemCode2 >= MatrixSize)
        {
            return -1;
        }
        return interactionMatrix[itemCode1, itemCode2];
    }
}
