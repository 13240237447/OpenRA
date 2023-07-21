using System;
using UnityEngine;

namespace OpenRA
{
    public enum WorldType { Regular, ShellMap, Editor }

    public sealed class World : MonoBehaviour
    {
        public Mobile TestMobile;

        private void Start()
        {
            if (TestMobile)
            {
                TestMobile.QueueActivity(new Drag(TestMobile, TestMobile.GetPosition(), TestMobile.GetPosition(), 0));
            }
        }

        public void Test()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TestMobile)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Ground"))
                        {
                            // 这里可以获取射线与地面的交点位置
                            Vector3 groundPoint = hit.point;
                            Debug.DrawLine(Camera.main.transform.position,groundPoint,Color.red,1);
                            Debug.Log("地面交点位置：" + groundPoint);
                            TestMobile.QueueActivity(true,
                                new Drag(TestMobile,TestMobile.GetPosition(),new Vector2(groundPoint.x,groundPoint.z),1));
                        }
                    }
                }
            }
        }

        private void Update()
        {
            Test();
            TestMobile.Tick();
        }
    
    }
}
