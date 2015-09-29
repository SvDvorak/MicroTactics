using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Features.CreateSquad
{
    public class CreateEntityOnStart : MonoBehaviour
    {
        public void Start()
        {
            var configurer = GetComponent(typeof (IGameObjectConfigurer)) as IGameObjectConfigurer;
            configurer.OnAttachEntity(Pools.pool.CreateEntity());
        }
    }
}
