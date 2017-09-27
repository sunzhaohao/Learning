using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;   //最大血量
    //检测一个属性，当服务端改变值时，会同步到客户端
    //当值改变时，会调用一个方法 如：OnChangeHealth
    [SyncVar(hook = "OnChangeHealth")]   
    public int currentHealth = maxHealth;   //当前血量
    public Slider healthSlider;

    public bool destroyOnDeath = false;   //用来判断该不该销毁该物体
    private NetworkStartPosition[] spawnPoints;   //得到这个 NetworkStartPosition 组件

    void Start ()
    {
        if (isLocalPlayer)    //如果是客户端
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    //当玩家和子弹碰撞时调用的方法
	public void TakeDamage(int damage)
    {
        if (!isServer)   //如果不是服务端
        {
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)   //如果当前血量小于等于0   
        {
            if (destroyOnDeath)    //判断是不是可以销毁的对象
            {
                Destroy(gameObject);
                return;
            }
            currentHealth = maxHealth;    //就让他回复满血状态，方便后面我们让他重生时需要
            RpcRespawn();     //调用一个方法，设置重生的位置
        }
        
    }

    //当服务端检测的属性值改变时调用的方法
    //health：改变后的值
    void OnChangeHealth (int health)
    {
        healthSlider.value = (float)health / maxHealth;    //修改Slider的比例
    }

    //远程调用，表示调用这个方法时就会在客户端调用，方法名必须以Rpc开头
    [ClientRpc]   
    void RpcRespawn()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 spawnPosition = Vector3.zero;
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            //随机获取NetworkStartPosition数组中的一个，在得到它的位置
            spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }
        transform.position = spawnPosition;  
    }
}
