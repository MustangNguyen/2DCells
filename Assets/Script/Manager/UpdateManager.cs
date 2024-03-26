using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    private EnemyCell[] transformsPool;
    public int poolIndex = 0;
    public int maximumPool = 1000;
    public int enemiesCount = 0;
    public int transformPoolCount = 0;
    private void Start() {
        if(transformsPool == null){
             transformsPool = new EnemyCell[maximumPool];
        }
    }
    // private void Update() {
    //     for (int i = 0; i < transformPoolCount; i++)
    //     {
    //     }
    // }
    private void FixedUpdate()
    {
        for (int i = 0; i < transformPoolCount; i++)
        {
            transformsPool[i].CellUpdate();
        }
    }
    public void AddCellsToPool(EnemyCell transform){
        while(true){
            if (poolIndex >= maximumPool)
                poolIndex = 0;
            if (transformsPool[poolIndex] == null)
            {
                transformsPool[poolIndex] = transform;
                poolIndex++;

                enemiesCount++;
                break;
            }
            else{
                poolIndex++;
            }
        }
    }
}
