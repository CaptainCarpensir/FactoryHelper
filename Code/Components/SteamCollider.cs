﻿using FactoryHelper.Entities;
using Monocle;
using System;

namespace FactoryHelper.Components {
    [Tracked(false)]
    public class SteamCollider : Component {
        public Action<SteamWall> OnCollide;
        public Collider Collider;
        public bool OneShot;

        public SteamCollider(Action<SteamWall> onCollide, bool oneShot = true) 
            : base(false, false) {
            OnCollide = onCollide;
            Collider = null;
            OneShot = oneShot;
        }

        public bool ShouldDoChecks { get; private set; } = true;

        public void Check(SteamWall steamWall) {
            if (OnCollide != null) {
                Collider collider = Entity.Collider;
                if (Collider != null) {
                    Entity.Collider = Collider;
                }

                if (steamWall.CollideCheck(Entity)) {
                    OnCollide(steamWall);
                    if (OneShot) {
                        ShouldDoChecks = false;
                    }
                }

                Entity.Collider = collider;
            }
        }
    }
}
