using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

namespace Assets.Features.CreateSquad
{
    public class CreateEntityOnStart : MonoBehaviour
    {
        public Entity Entity;
        public void Start()
        {
            var configurer = GetComponent(typeof (IGameObjectConfigurer)) as IGameObjectConfigurer;
            Entity = Pools.pool.CreateEntity();
            configurer.OnAttachEntity(Entity);
        }
    }
}
