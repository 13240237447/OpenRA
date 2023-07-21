using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Util
{
    public static class GameObjectExts
    {
        public static void DestroySelf(this GameObject @this)
        {
            GameObject.Destroy(@this);
        }

        public static void DestroySelf(this GameObject @this, float t)
        {
            GameObject.Destroy(@this, t);
        }

        public static void DestroyAllChilds(this GameObject @this, bool isImmediate = false)
        {
            for (int i = @this.transform.childCount - 1; i >= 0; i--)
            {
                if (isImmediate)
                {
                    GameObject.DestroyImmediate(@this.transform.GetChild(i).gameObject);
                }
                else
                {
                    GameObject.Destroy(@this.transform.GetChild(i).gameObject);
                }
            }
        }

        public static GameObject Active(this GameObject @this)
        {
            @this.SetActive(true);
            return @this;
        }

        public static GameObject Deactive(this GameObject @this)
        {
            @this.SetActive(false);
            return @this;
        }

        /// <summary>
        /// 优化的设置SetActive方法，可以节约重复设置Active的开销
        /// </summary>
        public static GameObject SetActiveOptimize(this GameObject @this, bool isActive)
        {
            if (@this.activeSelf != isActive)
            {
                @this.SetActive(isActive);
            }

            return @this;
        }

        public static GameObject SetName(this GameObject @this, string name)
        {
            @this.name = name;
            return @this;
        }

        public static GameObject DontDestroy(this GameObject @this)
        {
            GameObject.DontDestroyOnLoad(@this);
            return @this;
        }

        public static T GetOrAddComponent<T>(this GameObject @this) where T : Component
        {
            T component = @this.GetComponent<T>();
            if (component == null)
            {
                component = @this.AddComponent<T>();
            }

            return component;
        }

        public static Component GetOrAddComponent(this GameObject @this, Type type)
        {
            Component component = @this.GetComponent(type);
            if (component == null)
            {
                component = @this.AddComponent(type);
            }

            return component;
        }

        public static T GetComponentInParent<T>(this GameObject @this, string parentName)
            where T : Component
        {
            var parent = @this.GetComponentsInParent<Transform>();
            var length = parent.Length;
            Transform parentTrans = null;
            for (int i = 0; i < length; i++)
            {
                if (parent[i].name == parentName)
                {
                    parentTrans = parent[i];
                    break;
                }
            }

            if (parentTrans == null)
                return null;
            var comp = parentTrans.GetComponent<T>();
            return comp;
        }

        public static T GetOrAddComponentInParent<T>(this GameObject @this, string parentName)
            where T : Component
        {
            var parent = @this.GetComponentsInParent<Transform>();
            var length = parent.Length;
            Transform parentTrans = null;
            for (int i = 0; i < length; i++)
            {
                if (parent[i].name == parentName)
                {
                    parentTrans = parent[i];
                    break;
                }
            }

            if (parentTrans == null)
                return null;
            var comp = parentTrans.GetComponent<T>();
            if (comp == null)
            {
                comp = @this.gameObject.AddComponent<T>();
            }

            return comp;
        }

        public static T GetComponentInChildren<T>(this GameObject @this, string childName)
            where T : Component
        {
            var childs = @this.GetComponentsInChildren<Transform>();
            var length = childs.Length;
            Transform childTrans = null;
            for (int i = 0; i < length; i++)
            {
                if (childs[i].name == childName)
                {
                    childTrans = childs[i];
                    break;
                }
            }

            if (childTrans == null)
                return null;
            var comp = childTrans.GetComponent<T>();
            return comp;
        }

        public static T GetOrAddComponentInChildren<T>(this GameObject @this, string childName)
            where T : Component
        {
            var childs = @this.GetComponentsInChildren<Transform>();
            var length = childs.Length;
            Transform childTrans = null;
            for (int i = 0; i < length; i++)
            {
                if (childs[i].name == childName)
                {
                    childTrans = childs[i];
                    break;
                }
            }

            if (childTrans == null)
                return null;
            var comp = childTrans.GetComponent<T>();
            if (comp == null)
                comp = childTrans.gameObject.AddComponent<T>();
            return comp;
        }

        public static T GetComponentInPeer<T>(this GameObject @this, string peerName)
            where T : Component
        {
            Transform tran = @this.transform.parent.Find(peerName);
            if (tran != null)
            {
                return tran.GetComponent<T>();
            }

            return null;
        }

        public static T GetOrAddComponentInPeer<T>(this GameObject @this, string peerName)
            where T : Component
        {
            Transform tran = @this.transform.parent.Find(peerName);
            if (tran != null)
            {
                var comp = tran.GetComponent<T>();
                if (comp == null)
                    @this.gameObject.AddComponent<T>();
                return comp;
            }

            return null;
        }

        public static T[] GetComponentsInPeer<T>(this GameObject @this, bool includeSrc = false)
            where T : Component
        {
            Transform parentTrans = @this.transform.parent;
            var childTrans = parentTrans.GetComponentsInChildren<Transform>();
            var length = childTrans.Length;
            Transform[] trans;
            if (!includeSrc)
                trans = Utility.Algorithm.FindAll(childTrans, t => t.parent == parentTrans);
            else
                trans = Utility.Algorithm.FindAll(childTrans, t => t.parent == parentTrans && t != @this);
            var transLength = trans.Length;
            T[] src = new T[transLength];
            int idx = 0;
            for (int i = 0; i < transLength; i++)
            {
                var comp = trans[i].GetComponent<T>();
                if (comp != null)
                {
                    src[idx] = comp;
                    idx++;
                }
            }

            T[] dst = new T[idx];
            Array.Copy(src, 0, dst, 0, idx);
            return dst;
        }

        public static GameObject TryRemoveComponent<T>(this GameObject @this) where T : Component
        {
            var t = @this.GetComponent<T>();

            if (t != null)
            {
                Object.Destroy(t);
            }

            return @this;
        }

        public static GameObject TryRemoveComponent(this GameObject @this, System.Type type)
        {
            var t = @this.GetComponent(type);

            if (t != null)
            {
                Object.Destroy(t);
            }

            return @this;
        }

        public static GameObject TryRemoveComponent(this GameObject @this, string type)
        {
            var t = @this.GetComponent(type);

            if (t != null)
            {
                Object.Destroy(t);
            }

            return @this;
        }

        public static GameObject TryRemoveComponents<T>(this GameObject @this) where T : Component
        {
            var t = @this.GetComponents<T>();

            for (var i = 0; i < t.Length; i++)
            {
                Object.Destroy(t[i]);
            }

            return @this;
        }

        public static GameObject TryRemoveComponents<T>(this GameObject @this, System.Type type)
        {
            var t = @this.GetComponents(type);

            for (var i = 0; i < t.Length; i++)
            {
                Object.Destroy(t[i]);
            }

            return @this;
        }

        public static GameObject SetLayer(this GameObject @this, int layer)
        {
            var trans = @this.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trans)
            {
                t.gameObject.layer = layer;
            }

            return @this;
        }

        /// <summary>
        /// 设置层级；
        /// 此API会令对象下的所有子对象都被设置层级； 
        /// </summary>
        public static GameObject SetLayer(this GameObject @this, string layerName)
        {
            var layer = LayerMask.NameToLayer(layerName);
            var trans = @this.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trans)
            {
                t.gameObject.layer = layer;
            }

            return @this;
        }

        /// <summary>
        /// 在本帧直接销毁
        /// </summary>
        /// <param name="@this"></param>
        public static void DestroyNow(this GameObject @this)
        {
            GameObject.DestroyImmediate(@this);
        }

        /// <summary>
        /// 获取或创建GameObject
        /// </summary>
        /// <param name="@this"></param>
        public static GameObject FindOrCreateGameObject(this GameObject @this, string GameObjectName)
        {
            var trans = @this.transform.Find(GameObjectName);
            if (trans == null)
            {
                var go = new GameObject(GameObjectName).SetParent(@this);
                return go;
            }
            else
            {
                return trans.gameObject;
            }
        }

        /// <summary>
        /// 获取或创建GameObject
        /// </summary>
        /// <param name="@this"></param>
        public static GameObject FindOrCreateGo(this GameObject @this, string GameObjectName)
        {
            return @this.FindOrCreateGameObject(GameObjectName);
        }

        /// <summary>
        /// 获取或创建GameObject
        /// </summary>
        /// <param name="@this"></param>
        public static GameObject FindOrCreateGameObject(this GameObject @this, string GameObjectName,
            params System.Type[] Components)
        {
            var trans = @this.transform.Find(GameObjectName);
            if (trans == null)
            {
                var go = new GameObject(GameObjectName, Components).SetParent(@this);
                return go;
            }
            else
            {
                return trans.gameObject;
            }
        }

        public static GameObject CreateGameObject(this GameObject @this, string GameObjectName)
        {
            var go = new GameObject(GameObjectName).SetParent(@this);
            return go;
        }

        public static GameObject CreateGameObject(this GameObject @this, string GameObjectName,
            params System.Type[] Components)
        {
            var go = new GameObject(GameObjectName, Components).SetParent(@this);
            return go;
        }

        /// <summary>
        /// 设置父级GameObject
        /// </summary>
        /// <param name="@this">返回自身</param>
        public static GameObject SetParent(this GameObject @this, GameObject parentGameObject)
        {
            @this.transform.SetParent(parentGameObject.transform);
            return @this;
        }

        /// <summary>
        /// 设置世界坐标
        /// </summary>
        /// <param name="@this"></param>
        public static GameObject SetPosition(this GameObject @this, Vector3 position)
        {
            if (@this.transform != null)
            {
                @this.transform.position = position;
            }

            return @this;
        }

        /// <summary>
        /// 设置本地坐标
        /// </summary>
        /// <param name="@this"></param>
        public static GameObject SetLocalPosition(this GameObject @this, Vector3 localPosition)
        {
            if (@this.transform != null)
            {
                @this.transform.localPosition = localPosition;
            }

            return @this;
        }

        public static GameObject SetLocalScale(this GameObject @this, Vector3 scaleValue)
        {
            if (@this.transform != null)
            {
                @this.transform.localScale = scaleValue;
            }

            return @this;
        }

        public static GameObject SetRotation(this GameObject @this, Quaternion value)
        {
            if (@this.transform != null)
            {
                @this.transform.rotation = value;
            }

            return @this;
        }

        public static GameObject SetLocalRotation(this GameObject @this, Quaternion value)
        {
            if (@this.transform != null)
            {
                @this.transform.localRotation = value;
            }

            return @this;
        }

        public static GameObject SetEulerAngles(this GameObject @this, Vector3 value)
        {
            if (@this.transform != null)
            {
                @this.transform.eulerAngles = value;
            }

            return @this;
        }

        /// <summary>
        /// 检查组件是否存在
        /// </summary>
        public static bool HasComponent<T>(this GameObject @this) where T : Component
        {
            return @this.GetComponent<T>() != null;
        }

        /// <summary>
        /// 检查组件是否存在
        /// </summary>
        /// <param name="@this">Game object</param>
        /// <param name="type">组件类型</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent(this GameObject @this, string type)
        {
            return @this.GetComponent(type) != null;
        }

        /// <summary>
        /// 检查组件是否存在
        /// </summary>
        /// <param name="@this">Game object</param>
        /// <param name="type">组件类型</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent(this GameObject @this, System.Type type)
        {
            return @this.GetComponent(type) != null;
        }

        public static void DetachChildren(this GameObject go)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            go.transform.DetachChildren();
        }

        public static GameObject GetChild(this GameObject go, int index)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            Transform childTrans = go.transform.GetChild(index);
            GameObject childObj = null;

            if (null != childTrans)
                childObj = childTrans.gameObject;

            return childObj;
        }

        public static void SetParent(this GameObject go, GameObject parentObj, bool worldPostionStays)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            Transform parentTrans = null;
            if (null != parentObj)
                parentTrans = parentObj.transform;

            go.transform.SetParent(parentTrans, worldPostionStays);
        }

        public static Vector3 InverseTransformDirection(this GameObject go, Vector3 direction)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            return go.transform.InverseTransformDirection(direction);
        }

        public static Vector3 InverseTransformPoint(this GameObject go, Vector3 position)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            return go.transform.InverseTransformPoint(position);
        }

        public static bool IsChildOf(this GameObject go, GameObject parentObj)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            if (null == parentObj)
                return false;

            return go.transform.IsChildOf(parentObj.transform);
        }

        public static void LookAt(this GameObject go, Vector3 worldPosition, Vector3 worldUp)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            go.transform.LookAt(worldPosition, worldUp);
        }

        public static void Rotate(this GameObject go, Vector3 angles, UnityEngine.Space relativeTo)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            go.transform.Rotate(angles, relativeTo);
        }

        public static void RotateAround(this GameObject go, Vector3 point, Vector3 axis, float angles)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            go.transform.RotateAround(point, axis, angles);
        }

        public static Vector3 TransformDirection(this GameObject go, Vector3 direction)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            return go.transform.TransformDirection(direction);
        }

        public static Vector3 TransformPoint(this GameObject go, Vector3 position)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            return go.transform.TransformPoint(position);
        }

        public static void Translate(this GameObject go, Vector3 translation, UnityEngine.Space relativeTo)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            go.transform.Translate(translation, relativeTo);
        }

        //返回结果在 go 的 parents 中，且是 parent 的子元素。无符合要求的元素时，返回 null
        public static GameObject FindInParentChildOf(this GameObject go, GameObject parent)
        {
            if (null == go)
            {
                throw new ArgumentNullException("go");
            }

            Transform current = go.transform;
            Transform parentTrans = parent.transform;
            while (current != null && current.parent != parentTrans)
                current = current.parent;

            return current == null ? null : current.gameObject;
        }

        public static GameObject InstantiateIntoParent(this GameObject original, GameObject parent,
            Boolean instantiateInWorldSpace)
        {
            return GameObject.Instantiate(original, parent.transform, instantiateInWorldSpace);
        }

        public static GameObject FindChildRecursively(this GameObject self, String childName)
        {
            //int pos = childname.IndexOfOrdinal("/");
            int pos = childName.IndexOf('/');
            if (pos == -1)
                return FindChildDirect(self, childName, true);
            if (pos == 0)
                return null;
            var trans = self.transform.Find(childName);
            return trans != null ? trans.gameObject : null;
        }

        public static GameObject FindChildDirect(GameObject obj, string childname, bool recursive)
        {
            if (childname == ".")
            {
                return obj;
            }
            else if (childname == "..")
            {
                Transform parentTras = obj.transform.parent;
                return parentTras == null ? null : parentTras.gameObject;
            }

            Transform childtrans;

            if (recursive)
            {
                childtrans = FindChildRecursive(obj.transform, childname);
            }
            else
            {
                childtrans = obj.transform.Find(childname);
            }

            return childtrans ? childtrans.gameObject : null;
        }

        public static Transform FindChildRecursive(Transform objtrans, string childname)
        {
            var childtrans = objtrans.Find(childname);

            if (childtrans)
                return childtrans;

            for (int i = 0; i < objtrans.childCount; ++i)
            {
                Transform trans = objtrans.GetChild(i);
                childtrans = FindChildRecursive(trans, childname);
                if (childtrans)
                    return childtrans;
            }

            return null;
        }


        /// <summary>
        /// 查找 Child
        /// </summary>
        /// <param name="obj">在此对象中查找</param>
        /// <param name="name">子对象名</param>
        /// <returns>子对象。未找到时返回 null</returns>
        public static GameObject FindChild(this GameObject obj, String name)
        {
            Transform child = obj.transform.Find(name);
            return child ? child.gameObject : null;
        }

        /// <summary>
        /// 查找 Component
        /// </summary>
        /// <typeparam name="TComponent">Component 类型</typeparam>
        /// <param name="obj">在此对象中查找</param>
        /// <param name="childName">子对象名</param>
        /// <returns></returns>
        public static TComponent FindChildComponent<TComponent>(this GameObject obj, String childName)
            where TComponent : Component
        {
            GameObject child = FindChild(obj, childName);
            if (child != null)
                return child.GetComponent<TComponent>();
            else
                return null;
        }

        /// <summary>
        /// 查找 Child，未找到时抛出异常
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="child">返回的 Child</param>
        /// <param name="name"></param>
        public static GameObject RequireChild(this GameObject obj, String name)
        {
            GameObject child = FindChild(obj, name);
            if (child == null)
                throw new Exception(String.Format("Child with name {0} not found in {1}", name, obj));
            return child;
        }

        /// <summary>
        /// 查找 Component，未找到时抛出异常
        /// </summary>
        /// <typeparam name="TComponent">Component 类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="component">返回的 Component</param>
        /// <param name="name"></param>
        public static void RequireChildComponent<TComponent>(this GameObject obj, out TComponent component, String name)
            where TComponent : Component
        {
            component = RequireChildComponent<TComponent>(obj, name);
        }

        /// <summary>
        /// 查找 Component，未找到时抛出异常
        /// </summary>
        public static TComponent RequireChildComponent<TComponent>(this GameObject obj, String name)
            where TComponent : Component
        {
            TComponent childComponent = FindChildComponent<TComponent>(obj, name);
            if (childComponent == null)
                throw new Exception(String.Format("Component with child name {0} and type {1} not found in {2}", name,
                    typeof(TComponent).Name, obj));
            return childComponent;
        }

        public static GameObject FindChildGameObjectRecursively(this GameObject obj, String name,
            string ignoreName = null)
        {
            var objname = obj.name;

            if (ignoreName != null && objname == ignoreName)
                return null;

            if (objname == name)
                return obj;

            var trans = obj.transform;

            for (Int32 i = 0; i < trans.childCount; ++i)
            {
                var child = FindChildGameObjectRecursively(trans.GetChild(i).gameObject, name, ignoreName);
                if (child != null)
                    return child;
            }

            return null;
        }

        public static TComponent FindChildComponentRecursively<TComponent>(this GameObject obj, String childName)
            where TComponent : Component
        {
            GameObject child = FindChildGameObjectRecursively(obj, childName);
            if (child != null)
                return child.GetComponent<TComponent>();
            else
                return null;
        }
        
        public static void DoSomeOnChild<T>(this GameObject obj , Action<T> action) where T : Component
        {
            T[] components = obj.GetComponentsInChildren<T>(true);
            if (components != null)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    T comp = components[i];
                    if (comp.gameObject != obj)
                    {
                        action?.Invoke(comp);
                    }
                }
            }
        }

        public static void RemoveCloneSuffix(this GameObject obj)
        {
            obj.name = obj.name.Replace("(Clone)", "");
        }
    }
}