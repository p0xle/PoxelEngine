using PoxelEngine.Components;
using PoxelEngine.Models;

namespace EngineTest.Demo_Game.Components
{
    public class CoinComponent : Component
    {
        public CoinComponent(GameObject gameObject) : base(gameObject)
        {
        }

        public int Coins { get; set; } = 0;

        private readonly string coinTag = "Coin";

        public override bool Init()
        {
            return true;
        }

        public override void Update()
        {
            var collisionComponent = this.GameObject?.GetComponent<CollisionComponent>();
            var coin = collisionComponent?.IsColliding(this.coinTag);
            if (coin != null)
            {
                coin.DestroySelf();
                this.Coins--;
            }
        }

        protected override void Dispose(bool disposing)
        {

        }
    }
}
