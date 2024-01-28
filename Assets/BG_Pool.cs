using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class BG_Pool : MonoBehaviour
{
    public float imageHeight_Test;
    public List<BGTemplate> Templates;
    public AreaState areaState;
    public Transform spawnPoint_goingUp,spawnPoint_Dowm;
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
        GameObject bgObject= Instantiate(transform.GetChild(0),transform).gameObject;
        BGState bg=bgObject.GetComponent<BGState>();

        SpriteRenderer bgRenderer = bg.GetComponent<SpriteRenderer>();
        float imageHeight = Mathf.Abs(bgRenderer.bounds.max.y - bgRenderer.bounds.min.y);
        if (areaState.rollVector.y > 0)
        {//往上
            bg.transform.position = new Vector3(spawnPoint_goingUp.position.x, spawnPoint_goingUp.position.y - imageHeight, spawnPoint_goingUp.position.z);
        }
        else
        {//往下

            bg.transform.position = new Vector3(spawnPoint_Dowm.position.x, spawnPoint_Dowm.position.y + imageHeight, spawnPoint_Dowm.position.z);
        }
        int templateIndex = Mathf.RoundToInt(Random.Range(0, Templates.Count - 1));
        bg.template = Templates[templateIndex];
        bg.rollSpeed = areaState.rollSpeed;
        bg.rollVector = areaState.rollVector.y;
        bg.isMove = true;

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
        SpriteRenderer bgRenderer = bg.GetComponent<SpriteRenderer>();
        float imageHeight = Mathf.Abs(bgRenderer.bounds.max.y-bgRenderer.bounds.min.y);
        imageHeight_Test=imageHeight; ;
        bg.gameObject.SetActive(true);
        if (areaState.rollVector.y > 0) {//往上
            bg.transform.position = new Vector3(spawnPoint_goingUp.position.x, spawnPoint_goingUp.position.y- imageHeight, spawnPoint_goingUp.position.z) ;
        }
        else
        {//往下
            
            bg.transform.position = new Vector3(spawnPoint_Dowm.position.x, spawnPoint_Dowm.position.y + imageHeight, spawnPoint_Dowm.position.z);
        }
        int templateIndex=Mathf.RoundToInt(Random.Range(0, Templates.Count - 1));
        bg.template = Templates[templateIndex];
        bg.rollSpeed = areaState.rollSpeed;
        bg.rollVector = areaState.rollVector.y;
        bg.isMove=true;
        
    }
    private void Start()
    {
        areaState=transform.parent.gameObject.GetComponent<AreaState>();
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(BGState bg)
    {
        Destroy(bg.gameObject);
    }
}
