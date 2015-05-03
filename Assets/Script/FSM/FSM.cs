using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour
{
    //目标
    protected Transform playerTransform;
    //目的地
    protected Vector3 destPos;
    //寻路点
    protected GameObject[] pointList;
    //射击频率
    protected float shootRate;
    //距离上一次射击的时间
    protected float elapsedTime;

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}