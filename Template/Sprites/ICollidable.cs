namespace RogueLike.Sprites
{
    public interface ICollidable
    {
        void UpdateHitbox();
        void OnCollide(Sprite sprite);
    }
}
