using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class World
    {
        public MassSpring mMassSpring { get; private set; }
        private int mClothWidth = 25;
        private int mClothHeight = 20;
        private Vector2 mClothOffset = new Vector2(160, 50);

        public World()
        {
            mMassSpring = new MassSpring(mClothWidth, mClothHeight, mClothOffset);
        }

        public void Update(float dt)
        {
            mMassSpring.Update(dt);
        }
    }
}
