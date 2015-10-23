using UnityEngine;
using System.Collections;
using System;

public class AngleChecking : MonoBehaviour
{
	readonly int SHOT_RANGE = 2;

	public Camera cam;
	public UILabel 현재각도Y;
	public UILabel 현재각도X;

	public bool 각도사용Y = false;
	public bool 각도사용X = false;

	public int 상대각도 = 0;

	private int 상대값 = 0;
	private int 임시상대값 = 0;
	private int 값X = 0;

	public int 자동스샷 = 0;

	public GameObject RulerY;
	public GameObject RulerX;

	public GameObject[] TargetPoints = new GameObject[12];
	public GameObject[] TargetPointsX = new GameObject[2];

	public static AngleChecking angle;

	void Awake()
	{
		angle = this;
	}

	void Start()
	{

	}

	void Update()
	{

	}

	bool InRangeOfAutoShot(int curAngleX, int curAngleY, int targetX, int targetY)
	{
		if((targetX - SHOT_RANGE <= curAngleX) && (curAngleX <= targetX + SHOT_RANGE) &&
			(targetY - SHOT_RANGE <= curAngleY) && (curAngleY <= targetY + SHOT_RANGE))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void FixedUpdate()
	{
		if(각도사용Y)
		{
			상대값 = Convert.ToInt32(cam.transform.localEulerAngles.y - 상대각도);

			if(상대값 >= 0)
			{
				임시상대값 = 상대값;
				현재각도Y.text = string.Format("{0}", 임시상대값);
				RulerY.transform.localPosition = new Vector3((상대값 * 4), 0, 0);
			}
			else
			{
				임시상대값 = (상대값 + 361);
				현재각도Y.text = string.Format("{0}", 임시상대값);
				RulerY.transform.localPosition = new Vector3((상대값 + 361) * 4, 0, 0);
			}

			#region MyRegion
			/*
            // 타겟 포인트 부분 활성화
            // 첫번째 각도 포인트 이미지
            if (임시상대값 > 0 && 임시상대값 <= 30)
            {
                타겟포인트_활성화(1);
            }
            else if (임시상대값 > 30 && 임시상대값 <= 60)
            {
                타겟포인트_활성화(2);
            }
            else if (임시상대값 > 60 && 임시상대값 <= 90)
            {
                타겟포인트_활성화(3);
            }
            else if (임시상대값 > 90 && 임시상대값 <= 120)
            {
                타겟포인트_활성화(4);
            }
            else if (임시상대값 > 120 && 임시상대값 <= 150)
            {
                타겟포인트_활성화(5);
            }
            else if (임시상대값 > 150 && 임시상대값 <= 180)
            {
                타겟포인트_활성화(6);
            }
            else if (임시상대값 > 180 && 임시상대값 <= 210)
            {
                타겟포인트_활성화(7);
            }
            else if (임시상대값 > 210 && 임시상대값 <= 240)
            {
                타겟포인트_활성화(8);
            }
            else if (임시상대값 > 240 && 임시상대값 <= 270)
            {
                타겟포인트_활성화(9);
            }
            else if (임시상대값 > 270 && 임시상대값 <= 300)
            {
                타겟포인트_활성화(10);
            }
            else if (임시상대값 > 300 && 임시상대값 <= 330)
            {
                타겟포인트_활성화(11);
            }

            // 두번째 각도 포인트 이미지
            else if (임시상대값 > 0 && 임시상대값 <= 15)
            {
                타겟포인트_활성화(12);
            }
            else if (임시상대값 > 15 && 임시상대값 <= 45)
            {
                타겟포인트_활성화(13);
            }
            else if (임시상대값 > 45 && 임시상대값 <= 75)
            {
                타겟포인트_활성화(14);
            }
            else if (임시상대값 > 75 && 임시상대값 <= 105)
            {
                타겟포인트_활성화(15);
            }
            else if (임시상대값 > 105 && 임시상대값 <= 135)
            {
                타겟포인트_활성화(16);
            }
            else if (임시상대값 > 135 && 임시상대값 <= 165)
            {
                타겟포인트_활성화(17);
            }
            else if (임시상대값 > 165 && 임시상대값 <= 195)
            {
                타겟포인트_활성화(18);
            }
            else if (임시상대값 > 195 && 임시상대값 <= 225)
            {
                타겟포인트_활성화(19);
            }
            else if (임시상대값 > 225 && 임시상대값 <= 255)
            {
                타겟포인트_활성화(20);
            }
            else if (임시상대값 > 255 && 임시상대값 <= 285)
            {
                타겟포인트_활성화(21);
            }
            else if (임시상대값 > 285 && 임시상대값 <= 315)
            {
                타겟포인트_활성화(22);
            }
            else if (임시상대값 > 315 && 임시상대값 <= 345)
            {
                타겟포인트_활성화(23);
            }

            // 세번째 각도 포인트 이미지
            //else if (임시상대값 > 315 && 임시상대값 <= 345)
            //{
            //    타겟포인트_활성화(24);
            //}
            //else if (임시상대값 > 315 && 임시상대값 <= 345)
            //{
            //    타겟포인트_활성화(25);
            //}
            //else if (임시상대값 > 315 && 임시상대값 <= 345)
            //{
            //    타겟포인트_활성화(26);
            //}

            else
            {
                타겟포인트_활성화(0);
            }
             * */

			#endregion


			타겟포인트_활성화(MobileCamera.cam.스크린샷이미지.Count);

			// 자동 스크린샷 찍기 모드
			if(Step01_Events.step01.오토모드)
			{
				switch(MobileCamera.cam.스크린샷이미지.Count)
				{
					#region 첫번째 12장 자동 스크린샷 (0 - 11)

					case 0:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 0) && (자동스샷 == 0))
						{
							자동스샷 = 1;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 1:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 30) && (자동스샷 == 1))
						{
							자동스샷 = 2;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 2:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 60) && (자동스샷 == 2))
						{
							자동스샷 = 3;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 3:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 90) && (자동스샷 == 3))
						{
							자동스샷 = 4;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 4:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 120) && (자동스샷 == 4))
						{
							자동스샷 = 5;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 5:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 150) && (자동스샷 == 5))
						{
							자동스샷 = 6;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 6:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 180) && (자동스샷 == 6))
						{
							자동스샷 = 7;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 7:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 210) && (자동스샷 == 7))
						{
							자동스샷 = 8;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 8:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 240) && (자동스샷 == 8))
						{
							자동스샷 = 9;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 9:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 270) && (자동스샷 == 9))
						{
							자동스샷 = 10;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 10:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 300) && (자동스샷 == 10))
						{
							자동스샷 = 11;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 11:
						if(InRangeOfAutoShot(값X, 임시상대값, 20, 330) && (자동스샷 == 11))
						{
							자동스샷 = 12;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					#endregion

					#region 두번째 12장 자동 스크린샷 (12 - 23)

					case 12:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 15) && (자동스샷 == 12))
						{
							자동스샷 = 13;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 13:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 45) && (자동스샷 == 13))
						{
							자동스샷 = 14;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 14:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 75) && (자동스샷 == 14))
						{
							자동스샷 = 15;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 15:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 105) && (자동스샷 == 15))
						{
							자동스샷 = 16;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 16:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 135) && (자동스샷 == 16))
						{
							자동스샷 = 17;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 17:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 165) && (자동스샷 == 17))
						{
							자동스샷 = 18;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 18:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 195) && (자동스샷 == 18))
						{
							자동스샷 = 19;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 19:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 225) && (자동스샷 == 19))
						{
							자동스샷 = 20;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 20:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 255) && (자동스샷 == 20))
						{
							자동스샷 = 21;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 21:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 285) && (자동스샷 == 21))
						{
							자동스샷 = 22;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 22:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 315) && (자동스샷 == 22))
						{
							자동스샷 = 23;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 23:
						if(InRangeOfAutoShot(값X, 임시상대값, 40, 345) && (자동스샷 == 23))
						{
							자동스샷 = 24;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					#endregion

					#region 세번째 3장 자동 스크린샷 (24 - 26)

					case 24:
						if(InRangeOfAutoShot(값X, 임시상대값, 60, 0) && (자동스샷 == 24))
						{
							자동스샷 = 25;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 25:
						if(InRangeOfAutoShot(값X, 임시상대값, 60, 120) && (자동스샷 == 25))
						{
							자동스샷 = 26;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

					case 26:
						if(InRangeOfAutoShot(값X, 임시상대값, 60, 240) && (자동스샷 == 26))
						{
							자동스샷 = 27;
							Step01_Events.step01.Step01_스크린샷저장();
						}
						break;

						#endregion
				}
			}
		}
		else
		{
			현재각도Y.text = string.Format("0");
		}

		// 각도의 이미지를 자동 출력
		if(각도사용X)
		{
			값X = Convert.ToInt32(cam.transform.localEulerAngles.x);

			현재각도X.text = string.Format("{0}", 값X);
			RulerX.transform.localPosition = new Vector3(0, (값X * 4), 0);

			switch(MobileCamera.cam.스크린샷이미지.Count)
			{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
					TargetPointsX[0].SetActive(true);
					TargetPointsX[1].SetActive(false);
					TargetPointsX[2].SetActive(false);
					break;

				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
					TargetPointsX[0].SetActive(false);
					TargetPointsX[1].SetActive(true);
					TargetPointsX[2].SetActive(false);
					break;

				case 24:
				case 25:
				case 26:
					TargetPointsX[0].SetActive(false);
					TargetPointsX[1].SetActive(false);
					TargetPointsX[2].SetActive(true);
					break;
			}
		}
	}

	public void 타겟포인트_활성화(int index)
	{
		for(int i = 0; i < TargetPoints.Length; i++)
		{
			if(index == i)
			{
				TargetPoints[i].SetActive(true);
			}
			else
			{
				TargetPoints[i].SetActive(false);
			}
		}
	}

	public void 타겟포인트_모두비활성화()
	{
		for(int i = 0; i < TargetPoints.Length; i++)
		{
			TargetPoints[i].SetActive(false);
		}
	}

	public void 상대각도계산()
	{
		상대각도 = Convert.ToInt32(cam.transform.localEulerAngles.y);
		각도사용Y = true;
	}

	public void 상대각도계산사용안함()
	{
		각도사용Y = false;
	}
}
