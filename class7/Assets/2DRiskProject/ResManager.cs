using System;
using UnityEngine; 
using System.Collections.Generic;
using Object = UnityEngine.Object;


// ReSharper disable All

/// <summary>
/// 资源加载管理类，全局都可以使用，所有做成单例
/// </summary>
// ReSharper disable once UnusedMember.Global
public class ResManager : MonoBehaviour
{
    private static ResManager mInstance;

    public static ResManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject obj = new GameObject("ResManager");
                mInstance = obj.AddComponent<ResManager>();
            }
            return mInstance;
        }
    }

    /// <summary>
    /// 所有加载完成的字典
    /// </summary>
    private Dictionary<string,AssetPack> mAssetPacksDic = new Dictionary<string, AssetPack>();

    /// <summary>
    /// 当前正在加载的队列
    /// </summary>
    private List<AssetPack> mLoadList = new List<AssetPack>();

    /// <summary>
    /// 从Resource加载一个资源，会出现一个资源多次加载的情况，可以自己管理
    /// </summary>
    /// <param name="prefabName">资源名称</param>
    /// <param name="type">类型</param>
    /// <param name="callBack">加载完成后的回调</param>
    /// <param name="isKeepInMemory">是否常驻内存</param>
    public void LoadAssetAsync(string prefabName,Type type,ILoadCallBack callBack, bool isKeepInMemory = false)
    {
        if (mAssetPacksDic.ContainsKey(prefabName))
        {
            if (null != callBack)
            {
                callBack.Succeed(mAssetPacksDic[prefabName].request.asset);
            }
            return;
        }
        AssetPack assetPack = new AssetPack(prefabName, type, callBack, isKeepInMemory);
        mLoadList.Add(assetPack);
    }

    /// <summary>
    /// 从资源管理中释放一个资源
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="isRemove">是否可以释放</param>
    public void Remove(string assetName, bool isRemove = false)
    {
        if (!mAssetPacksDic.ContainsKey(assetName)) return;

        if (mAssetPacksDic[assetName].isKeepInMemory)
        {
            if (isRemove)
            {
                mAssetPacksDic[assetName] = null;
                mAssetPacksDic.Remove(assetName);
            }
        }
        else
        {
            mAssetPacksDic[assetName] = null;
            mAssetPacksDic.Remove(assetName);
        }
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 清空所有资源
    /// </summary>
    public void RemoveAll()
    {
//        List<string> removeList = new List<string>();
        foreach (KeyValuePair<string ,AssetPack> pair in mAssetPacksDic)
        {
            mAssetPacksDic[pair.Key] = null;
//            removeList.Add(pair.Key);
        }
        mAssetPacksDic.Clear();
        Resources.UnloadUnusedAssets();
    }
    void Update()
    {
        if (mLoadList.Count > 0)
        {
            for (int i = 0; i < mLoadList.Count; i++)
            {
                if (mLoadList[i] == null)
                {
                    mLoadList.RemoveAt(i);
                    continue;
                }
                if (!mLoadList[i].isLoad && mLoadList[i].request == null)
                {
                    mLoadList[i].isLoad = true;
                    LoadAsset(mLoadList[i]);                    
                }
                else if (mLoadList[i].isLoad && mLoadList[i].request != null)
                {
                    if (!mAssetPacksDic.ContainsKey(mLoadList[i].assetName))
                    {
                        mAssetPacksDic.Add(mLoadList[i].assetName, mLoadList[i]);

                        if (null != mLoadList[i].callBack)
                        {
                            if (null != mLoadList[i].request.asset)
                            {
                                mLoadList[i].callBack.Succeed(mLoadList[i].request.asset);
                            }
                            else
                            {
                                mLoadList[i].callBack.Failed();
                            }
                        }
                    }
                    else
                    {
                        mAssetPacksDic[mLoadList[i].assetName] = mLoadList[i];
                    }
                    mLoadList.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <param name="assetPack"></param>
    private void LoadAsset(AssetPack assetPack)
    {
        if (assetPack.isLoad)
        {
            return;
        }
        assetPack.request = Resources.LoadAsync(assetPack.assetName, assetPack.type);
    }

    #region 资源加载数据

    /// <summary>
    /// 加载包
    /// </summary>
    public class AssetPack
    {
        public AssetPack(string assetName, Type type, ILoadCallBack callBack, bool isKeepInMemory)
        {
            this.assetName = assetName;
            this.callBack = callBack;
            this.type = type;
            this.isKeepInMemory = isKeepInMemory;
        }

        /// <summary>
        /// 资源包
        /// </summary>
        public string assetName;

        /// <summary>
        /// 回调接口
        /// </summary>
        public ILoadCallBack callBack;

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type type;

        /// <summary>
        /// 是否常驻内存
        /// </summary>
        public bool isKeepInMemory;

        /// <summary>
        /// Resource加载结果
        /// </summary>
        public ResourceRequest request;

        /// <summary>
        /// 是否正在加载
        /// </summary>
        public bool isLoad;
    }

    #endregion


    #region 资源加载完成后的回调

    /// <summary>
    /// 资源加载完成后的回调
    /// </summary>
    public interface ILoadCallBack
    {
        void Succeed(Object asset);
        void Failed();
    }

    #endregion

}
