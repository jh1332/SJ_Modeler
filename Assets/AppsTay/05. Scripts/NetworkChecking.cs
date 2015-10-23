using UnityEngine;
using System.Collections;

public class NetworkChecking : MonoBehaviour
{
    public bool netCheck = false;
    public float checkTime = 1;

    void Awake()
    {

    }

    void Start()
    {
        //netCheck = true;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (netCheck)
        {
            checkTime -= Time.deltaTime;

            if (checkTime <= 0)
            {
                if (네트워크체크())
                {
                    checkTime = 1;
                    netCheck = true;
                }
                else
                {
                    netCheck = false;
                    // 프로그램 종료..
                }
            }
        }
    }

    public bool 네트워크체크()
    {
        // 3G, 4G 연결 상태체크
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return true;
        }
        // 와이파이 연결 상태체크
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return true;
        }
        else
        {
            // 모두 연결 안됨..
            return false;
        }
    }
}
