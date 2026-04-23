using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Gameplay.Arts
{
    public class ArtService : IArtService, IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle<Sprite>> _cache = new();

        public async UniTask<Sprite> GetSpriteAsync(AssetReferenceSprite reference)
        {
            string key = reference.AssetGUID;

            if (_cache.TryGetValue(key, out var existingHandle))
                return await existingHandle.ToUniTask();

            var handle = reference.LoadAssetAsync();
            _cache.Add(key, handle);

            try
            {
                return await handle.ToUniTask();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _cache.Remove(key);
                return null;
            }
        }

        public Sprite GetCachedSprite(string assetGuid)
        {
            if (_cache.TryGetValue(assetGuid, out var handle) && handle.IsValid())
                return handle.Result;
            return null;
        }

        public void ReleaseAll()
        {
            foreach (var handle in _cache.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }

            _cache.Clear();
        }

        public void Dispose() => ReleaseAll();
    }
}