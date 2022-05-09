using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int SpawnerID = 0;

    public int GetID()
    {
        this.SpawnerID++;
        return SpawnerID;
    }
}
