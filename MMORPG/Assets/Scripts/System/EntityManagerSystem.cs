using System;
using QFramework;
using System.Collections.Generic;
using MMORPG.Common.Proto.Entity;
using MMORPG.Event;
using MMORPG.Game;
using Serilog;
using UnityEngine;

namespace MMORPG.System
{
    public interface IEntityManagerSystem : ISystem
    {
        public EntityView SpawnEntity(
            EntityView prefab,
            int entityId,
            int unitId,
            EntityType type,
            Vector3 position,
            Quaternion rotation);

        public Dictionary<int, EntityView> EntityDict { get; }

        public void LeaveEntity(int entityId);
    }


    public class EntityManagerSystem : AbstractSystem, IEntityManagerSystem
    {
        public Dictionary<int, EntityView> EntityDict { get; } = new();

        private IDataManagerSystem _dataManager;

        public void LeaveEntity(int entityId)
        {
            if (EntityDict.TryGetValue(entityId, out var entity))
            {
                var suc = EntityDict.Remove(entity.EntityId);
                Debug.Assert(suc);
                this.SendEvent(new EntityLeaveEvent(entity));
                // Log.Information($"实体退出地图: id:{entityId}, type:{entity.EntityType}");
                // 主要为了延迟下一帧调用, 以便可以先处理EntityLeaveEvent再Destroy
                GameObject.Destroy(entity.gameObject);
            }
            else
            {
                Log.Error($"实体:{entityId}还没加入地图, 但是有退出地图的响应发来");
            }
        }

        public EntityView SpawnEntity(
            EntityView prefab,
            int entityId,
            int unitId,
            EntityType type,
            Vector3 position,
            Quaternion rotation)
        {
            if (EntityDict.TryGetValue(entityId, out var entity))
            {
                Log.Error($"实体'{entityId}'已存在。");
            }
            else
            {
                entity = GameObject.Instantiate(prefab, position, rotation);
                var unitDefine = _dataManager.GetUnitDefine(unitId);
                entity.Initialize(entityId, unitDefine);
                entity.gameObject.name = $"{unitDefine.Name}_{entityId}_{unitDefine.Kind}";
                EntityDict[entity.EntityId] = entity;
                this.SendEvent(new EntityEnterEvent(entity));
            }

            entity.transform.SetPositionAndRotation(position, rotation);
            return entity;
        }

        protected override void OnInit()
        {
            this.RegisterEvent<ExitedMapEvent>(OnExitedMap);

            _dataManager = this.GetSystem<IDataManagerSystem>();
        }

        private void OnExitedMap(ExitedMapEvent e)
        {
            // foreach (var entity in EntityDict)
            // {
            //     if (entity.Value.gameObject != null)
            //     {
            //         GameObject.Destroy(entity.Value.gameObject);
            //     }
            // }
            EntityDict.Clear();
        }

        protected override void OnDeinit()
        {
            EntityDict.Clear();
        }
    }
}
