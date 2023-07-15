
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Common
{
    public interface IResetable {void OnReset();}
    public class GameObjectPool:MonoSingleton<GameObjectPool>
    {
        public Dictionary<string, List<GameObject>> cache = new Dictionary<string, List<GameObject>>();

        public override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
        }
        public GameObject CreateObject(string key,GameObject prefab,Vector3 pos,Quaternion rotate)
        {
            GameObject go = FindUsableObject(key);
            if(go == null)
            {
               go = AddObject(key,prefab);
            }

            UseObject(go, pos, rotate);
            return go;
        }

        public GameObject CreateObject(string key, GameObject prefab,string name, Vector3 pos, Quaternion rotate)
        {
            GameObject go = FindUsableObject(key);
            if (go == null)
            {
                go = AddObject(key, prefab, name);
            }

            UseObject(go, pos, rotate);
            return go;
        }

        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate,Transform father)
        {
            GameObject go = FindUsableObject(key);
            if (go == null)
            {
                go = AddObject(key, prefab);
                if (father != null)
                    go.transform.SetParent(father);
            }

            UseObject(go, pos, rotate);
            return go;
        }

        /// <summary>
        /// 回收对象池对象
        /// </summary>
        /// <param name="prefab">预制件</param>
        /// <param name="delay">延时</param>
        public void CollectObject(GameObject prefab,float delay = 0)
        {
            //延时回收一个脚本只能回收一个
            //写两个函数是因为协程是下一帧执行，没法做到及时回收
            if (delay == 0)
                MyCollectObject(prefab);
            else
                StartCoroutine(MyCollectObject(prefab, delay));
        }
        /// <summary>
        /// MyCollectObject用于回收对象池创建的对象
        /// </summary>
        /// <param name="prefab"></param>
        private void MyCollectObject(GameObject prefab)
        {
            if((prefab != null))//&& (prefab.activeInHierarchy))
                prefab.SetActive(false);
        }

        private IEnumerator MyCollectObject(GameObject prefab,float delay)
        {
            yield return new WaitForSeconds(delay);
            if ((prefab != null) && (prefab.activeInHierarchy))
                prefab.SetActive(false);
        }

        private void UseObject(GameObject go, Vector3 pos, Quaternion rotate)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);

            //执行组件下组件挂载的各种复位方式
            foreach(var prefab in go.GetComponents<IResetable>())
            {
                prefab.OnReset();
            } 
        }

        /// <summary>
        /// 查找可用的组件
        /// </summary>
        private GameObject FindUsableObject(string key)
        {
            if(cache.ContainsKey(key))
               return cache[key].Find(s => !s.activeInHierarchy);
            return null;
        }

        public List<GameObject> FindUsingObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].FindAll(s => s.activeInHierarchy);
            return null;
        }

        private GameObject AddObject(string key,GameObject prefab)
        {
           GameObject go = Instantiate(prefab);
            if (!cache.ContainsKey(key))
                cache.Add(key, new List<GameObject>());

            cache[key].Add(go);
            return go;
        }

        private GameObject AddObject(string key, GameObject prefab,string name)
        {
            GameObject go = Instantiate(prefab);
            go.name = name;
            if (!cache.ContainsKey(key))
                cache.Add(key, new List<GameObject>());
            
            cache[key].Add(go);
            return go;
        }

        //清空某个类别
        public void Clear(string key)
        {
            for (int i = cache[key].Count-1; i >= 0; i--)
            {
                Destroy(cache[key][i]);
            }

            cache.Remove(key);
        }
        //清空所有
        public void ClearAll()
        {
            //将字典中所有键存入集合中再进行清空操作
            foreach (var key in new List<string>(cache.Keys))
            {
                Clear(key);
            }
        }
    }
}
