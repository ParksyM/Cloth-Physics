using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class SpringNode
    {
        public RigidBody rigidBody { get; private set; }
        public List<RigidBody> neighbours { get; set; }
        public List<SpringJoint> springJoints { get; private set; }
        //public List<RigidBody> diagonalNeighbours { get; set; }
        //public Vector2 currentForce { get; set; }

        public SpringNode()
        {
            neighbours = new List<RigidBody>();
            springJoints = new List<SpringJoint>();
            //diagonalNeighbours = new List<RigidBody>();
            //currentForce = Vector2.Zero;
        }

        public SpringNode(RigidBody body) : this()
        {
            rigidBody = body;
        }

        public void AddNeighbour(RigidBody body, float springKoeff, float stiffnessKoeff, float restLength)
        {
            neighbours.Add(body);
            springJoints.Add(new SpringJoint(rigidBody, body, springKoeff, stiffnessKoeff, restLength));
        }
    }
}
