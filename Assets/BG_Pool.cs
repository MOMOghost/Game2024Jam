using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ReturnBGToPool : MonoBehaviour 
{
    public BGState BG;
    public IObjectPool<BGState> pool;

    void Start()
    {
        BG = GetComponent<BGState>();
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        pool.Release(BG);
    }
}
public class BG_Pool : MonoBehaviour
{
    public enum PoolType {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int maxPoolSize = 10;

    IObjectPool<BGState> m_Pool;

    public IObjectPool<BGState> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                if (poolType == PoolType.Stack)
                    m_Pool = new ObjectPool<BGState>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    m_Pool = new LinkedPool<BGState>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return m_Pool;
        }
    }

    BGState CreatePooledItem()
    {
        var go = new GameObject("Pooled BGState");
        var bg = go.AddComponent<BGState>();
        // This is used to return ParticleSystems to the pool when they have stopped.
        var returnToPool = go.AddComponent<ReturnBGToPool>();
        returnToPool.pool = Pool;

        return bg;
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(BGState bg)
    {
        bg.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(BGState bg)
    {
        bg.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(BGState bg)
    {
        Destroy(bg.gameObject);
    }
}
