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

        public World()
        {
            mMassSpring = new MassSpring(25, 20, new Vector2(50, 50));
        }

        public void Update(float dt)
        {
            mMassSpring.Update(dt);
        }
    }
}
