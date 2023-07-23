using System;
using UnityEngine;
using UnityEngine.AI;

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

                    Debug.DrawRay(ray.origin,ray.direction,Color.green,1);
                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 groundPoint = hit.point;
                        Debug.DrawLine(Camera.main.transform.position,groundPoint,Color.red,1);
                        if (hit.collider.CompareTag("Ground"))
                        {
                            // 这里可以获取射线与地面的交点位置
                            Debug.Log("地面交点位置：" + groundPoint);
                            TestMobile.GetComponent<NavMeshAgent>().destination = groundPoint;
                            // TestMobile.QueueActivity(true,
                                // new Drag(TestMobile,TestMobile.GetPosition(),groundPoint,1));
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
