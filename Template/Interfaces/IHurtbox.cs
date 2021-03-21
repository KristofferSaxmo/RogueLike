﻿using RogueLike.Sprites;

namespace RogueLike.Interfaces
{
    public interface IHurtbox
    {
        void UpdateHurtbox();
        void OnCollide(Sprite sprite);
    }
}