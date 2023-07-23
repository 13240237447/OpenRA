using UnityEngine;

namespace OpenRA
{
    public class MapSettle : MonoBehaviour
    {
        public Transform rockTransform;

        public Transform plantTransform;

        public Transform treeTransform;

        public void AutoGroup()
        {
             Transform[] childs = transform.GetComponentsInChildren<Transform>(true);
             foreach (Transform s in childs)
             {
                 if (s.parent == this.transform)
                 {
                     if (s.name.StartsWith("SM_Rock"))
                     {
                         s.SetParent(rockTransform,true);
                     }
                     else if (s.name.StartsWith("SM_Plant"))
                     {
                         s.SetParent(plantTransform,true);
                     }
                     else if (s.name.StartsWith("SM_Tree"))
                     {
                         s.SetParent(treeTransform,true);
                     }
                 }
             }
        }

        public void SetTag()
        {
            Transform[] childs = transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform s in childs)
            {
                s.gameObject.tag = "Ground";
            }
        }
    }
}