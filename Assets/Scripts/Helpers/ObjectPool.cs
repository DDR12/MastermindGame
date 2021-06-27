using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple object pooling behaviour to cache game objects to avoid instantiation spikes.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T m_ViewModel = null;
    private readonly List<T> pool = null;
    private readonly Transform mainViewParent = null;
    public ObjectPool(T viewModel, uint initialPoolSize = 8)
    {
        m_ViewModel = viewModel;
        m_ViewModel.gameObject.SetActive(false);
        mainViewParent = viewModel.transform.parent;
        pool = new List<T>(8) { m_ViewModel };
        
        for(uint i = 0; i < initialPoolSize; i++)
        {
            CreateNewItem();
        }
    }
    public T GetNewItem()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }
        return CreateNewItem();
    }
    public void DestroyAll() => pool.ForEach(o => 
    {
        if (o.gameObject.activeSelf)
            o.gameObject.SetActive(false);
    });

    private T CreateNewItem()
    {
        T instance = GameObject.Instantiate<T>(m_ViewModel, mainViewParent);
        instance.gameObject.SetActive(false);
        pool.Add(instance);
        return instance;
    }
}
