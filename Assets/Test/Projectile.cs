using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    Action action = () => { };

    Vector3 dir = Vector3.zero;
    float speed = 0;
    float limitDistance = 0;
    Vector3 oldPos = Vector3.zero;
    Vector3 curPos = Vector3.zero;

    public void Init(Vector3 pos, Vector3 _dir, float _speed, float _limitDistance, float scale)
    {
        if(_limitDistance <= 0)
        {
            Debug.LogError("_limitDistance <= 0");
        }

        transform.position = pos;
        oldPos = pos;
        dir = _dir;
        speed = _speed;
        limitDistance = _limitDistance;
        transform.localScale = new Vector3(scale, scale, scale);

        action = Move;
    }

    void Update()
    {
        action();
    }

    void Move()
    {
        transform.position += dir * Time.deltaTime * speed;
        curPos = transform.position;

        float distance = Vector3.Distance(oldPos, curPos);
        if(limitDistance < distance)
        {
            action = () => { };
            gameObject.SetActive(false);

            return;
        }
    }
}
