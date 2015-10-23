using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Monster : MonoBehaviour
{
	[SerializeField]
	float speed = 1;

	[SerializeField]
	float deltaY = 1;

	[SerializeField]
	float projectileSpeed = 1;

	[SerializeField]
	float catchDistance = 1;

	[SerializeField]
	float attackDistance = 0.2f;

	enum State
	{
		Ready,
		Trace,
		Attack,
		Drift,
	}

	readonly int PROJECTILE_NUM = 3;
	readonly int PROJECTILE_POOL_NUM = 10;
	readonly float FIRE_RANGE = 0.5f;

	GameObject target = null;
	Transform firePivot = null;

	Vector3 targetDirOld = Vector3.zero;
	Vector3 targetDirCur = Vector3.zero;

	// 드리프트
	float curSpeedRate = 100;
	Vector3 driftMoveDir = Vector3.zero;

	// 공격
	bool isAttacking = false;
	List<Projectile> projectileList = new List<Projectile>();

	Action action = () => { };

	void Awake()
	{
		for(int _i = 0; _i < PROJECTILE_POOL_NUM; ++_i)
		{
			Projectile projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Projectile>();
			projectile.gameObject.SetActive(false);
			projectileList.Add(projectile);
		}

		firePivot = transform.FindChild("FirePivot");

		StartCoroutine(FindTarget());
	}

	void Update()
	{
		action();
	}

	void Trace()
	{
		targetDirCur = target.transform.forward;

		if(IsCatchDistance() && targetDirOld != targetDirCur) // 잡을 수 있는 거리이고 타겟의 진행방향이 바뀌었으면
		{
			ChangeState(State.Drift);

			return;
		}

		if(CanAttack())
		{
			ChangeState(State.Attack);

			return;
		}

		targetDirOld = targetDirCur;

		Vector3 _dir = GetLookAtTarget();

		// 이동     
		transform.position = transform.position + (_dir * Time.deltaTime * GetSpeed());

		// 회전
		transform.rotation = Quaternion.LookRotation(new Vector3(_dir.x, 0, _dir.z));
	}

	bool IsCatchDistance()
	{
		return IsIn(catchDistance);
	}

	bool CanAttack()
	{
		return IsIn(attackDistance);
	}

	bool IsIn(float _distance)
	{
		Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
		Vector2 pos = new Vector2(transform.position.x, transform.position.z);

		return Vector3.Distance(targetPos, pos) < _distance;
	}

	float GetSpeed()
	{
		return speed * curSpeedRate * 0.01f;
	}

	void ResetSpeed()
	{
		curSpeedRate = 100;
	}

	Vector3 GetLookAtTarget()
	{
		Vector3 targetPos = target.transform.position;
		targetPos.y += deltaY;

		return (targetPos - transform.position).normalized;
	}

	void ChangeState(State _state)
	{
		switch(_state)
		{
			case State.Ready:
				action = () => { };

				break;

			case State.Drift:
				ResetSpeed();
				targetDirOld = targetDirCur;
				driftMoveDir = transform.forward;
				action = Drift;

				break;

			case State.Trace:
				ResetSpeed();
				targetDirCur = target.transform.forward;
				targetDirOld = targetDirCur;

				action = Trace;

				break;

			case State.Attack:
				isAttacking = true;
				action = () => { };
				StartCoroutine(Fire());

				break;
		}
	}

	void Drift()
	{
		// 회전
		Vector3 _dir = GetLookAtTarget();

		transform.rotation = Quaternion.LookRotation(new Vector3(_dir.x, 0, _dir.z));

		// 감속
		curSpeedRate -= 1f;

		if(curSpeedRate <= 0)
		{
			ChangeState(State.Trace);

			return;
		}

		transform.position += driftMoveDir * Time.deltaTime * GetSpeed();
	}

	void CreateProjectile()
	{
		Projectile projectile = projectileList.Find(e => e.gameObject.activeSelf == false);
		if(null == projectile)
		{
			return;
		}

		projectile.gameObject.SetActive(true);

		Vector3 _dir = transform.forward;
		_dir.y = (target.transform.position - transform.position).normalized.y;

		_dir.x = UnityEngine.Random.Range(_dir.x - FIRE_RANGE, _dir.x + FIRE_RANGE);
		_dir.z = UnityEngine.Random.Range(_dir.z - FIRE_RANGE, _dir.z + FIRE_RANGE);

		projectile.Init(firePivot.position, _dir, projectileSpeed, 5, 0.1f);

	}

	IEnumerator Fire()
	{
		int projectileNum = 0;

		while(projectileNum < PROJECTILE_NUM)
		{
			CreateProjectile();
			projectileNum++;

			yield return new WaitForSeconds(0.2f);
		}

		ChangeState(State.Trace);
	}

	IEnumerator FindTarget()
	{
		while(true)
		{
			target = GameObject.Find("A");
			if(null != target)
			{
				GameObject catchRangeObj = new GameObject("Catch Range");
				catchRangeObj.transform.parent = transform;
				catchRangeObj.transform.localPosition = Vector3.zero;

				var catchRange = catchRangeObj.AddComponent<SphereCollider>();
				catchRange.radius = catchDistance;

				GameObject attackRangeObj = new GameObject("Attack Range");
				attackRangeObj.transform.parent = transform;
				attackRangeObj.transform.localPosition = Vector3.zero;

				var attackRange = attackRangeObj.AddComponent<SphereCollider>();
				attackRange.radius = attackDistance;

				ChangeState(State.Trace);

				break;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}
}
