using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint
{
    public string itemName;

    public int numOfRequirements;

    public string req1;
    public string req2;

    public int req1Amount;
    public int req2Amount;

    public Blueprint(string name, int reqNum, string r1, int r1Number, string r2, int r2Number)
    {
        itemName = name;

        numOfRequirements = reqNum;

        req1 = r1;
        req2 = r2;

        req1Amount = r1Number;
        req2Amount = r2Number;
    }
}
