using RogueLike.Sprites;

namespace RogueLike.Interfaces
{
    public interface IDamageable
    {
        void UpdateHurtbox();
        void OnCollide(Hitbox hitbox);
    }
}
