using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpawn : NetworkBehaviour {
    public GameObject enemyPre;  //用来得到炮灰这个物体
    public int enemyCount = 6;   //定义要生成的炮灰的数量

    //NetworkBehaviour 的内置方法，当服务器启动时调用
    public override void OnStartServer()
    {
        for (int i = 0; i < enemyCount; i ++)
        {
            //给要生产的炮灰一个随机位置，高度不变
            Vector3 ePosition = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
            //给他一个方向，就是围绕Y轴旋转，其他不变
            //Quaternion.Euler：把一个旋转转成四元素
            Quaternion eRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            //实例化出这个炮灰
            GameObject enemy = Instantiate(enemyPre, ePosition, eRotation) as GameObject;

            NetworkServer.Spawn(enemy);   //在每个客户端上都生成一个物体
        }
    }
}
