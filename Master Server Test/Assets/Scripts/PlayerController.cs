using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//为了能让脚本在连上局域网的同时还能分别控制物体，所以要继承 NetworkBehaviour
public class PlayerController : NetworkBehaviour {
    public float traSpeed = 3;  //移动的速度
    public float rotSpeed = 120;  //一秒旋转的角度
    public GameObject bulletPre;   //子弹的prefab
    public Transform bulletTrans;  //生成子弹的位置

	// Update is called once per frame
	void Update () {

        // isLocalPlayer 是 NetworkBehaviour 的内置属性
        if (!isLocalPlayer)    //如果不是本地客户端，就返回，不执行下面的操作
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * v * traSpeed * Time.deltaTime);   //朝某个方向移动
        transform.Rotate(Vector3.up * h * rotSpeed * Time.deltaTime);  //围绕某轴旋转

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    //这是重写 NetworkBehaviour 内的方法
    //这个方法只会在创建本地角色时调用
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command]    //在客户端调用，但是在服务端运行，这是方法必须以 Cmd 开头
    void CmdFire()
    {
         
        GameObject bullet = Instantiate(bulletPre,  bulletTrans.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 10;
        Destroy(bullet, 2);   //2秒后销毁子弹

        NetworkServer.Spawn(bullet);    //在所有客户端都生成一个物体
    }
}
