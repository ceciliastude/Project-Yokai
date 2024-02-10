using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Yokai.Content.Code
{
    //Camera that will follow the player around but stop when the player reaches close to the maps border.
    public class Camera
    {
        private Vector2 position;
        private Viewport viewport;
        private float zoom = 1.0f;
        private int mapWidth;
        private int mapHeight;
        public Matrix Transform { get; private set; }

        public void Initialize(Viewport viewport, int mapWidth, int mapHeight)
        {
            this.viewport = viewport;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
        }

        public void Follow(Vector2 target)
        {
            var dx = (viewport.Width / 2) - target.X;
            dx = MathHelper.Clamp(dx, -mapWidth + viewport.Width, 0);

            var dy = (viewport.Height / 2) - target.Y;
            dy = MathHelper.Clamp(dy, -mapHeight + viewport.Height, 0);

            position.X = dx;
            position.Y = dy;

            Transform = Matrix.CreateTranslation(new Vector3(position, 0)) *
                        Matrix.CreateScale(zoom);
        }
    }
}
