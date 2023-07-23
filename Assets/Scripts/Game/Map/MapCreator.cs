using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace OpenRA
{
    public class MapCreator : MonoBehaviour
    {
        public GameObject cellTemplate;

        public int size;

        public float cellSize;
        
        private GameObject mapRoot;

        private List<GameObject> cellList = new List<GameObject>();

        private void Start()
        {
            CreateMap();
        }

        public void CreateMap()
        {
            if (size <= 0 || cellTemplate == null )
            {
                return;
            }
            cellList.Clear();
            mapRoot = this.gameObject;
            mapRoot.transform.DestroyAllChilds();

            float startPos = size * 1.0f / 2 * cellSize;
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Vector3 pos = new Vector3(-startPos + i * cellSize, 0, startPos - j * cellSize);
                    GameObject o = GameObject.Instantiate(cellTemplate,pos, Quaternion.Euler(90,0,0),mapRoot.transform);
                    o.name = ($"{i + 1}_{j + 1}");
                    cellList.Add(o);
                }
            }
        }
    }
}