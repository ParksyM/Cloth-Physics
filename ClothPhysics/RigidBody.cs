using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class RigidBody
    {
        public Vector2 Position { get; set; }
        public Vector2 LinearVelocity { get; private set; }
        public Vector2 LinearAcceleration { get; private set; }
        public Vector2 Force { get; set; }
        public float Mass { get; set; }
        public float InvMass { get; set; }
        public Vector2 CurrentForce { get; set; }
        public bool IsDynamic { get; private set; }

        public RigidBody() : this(Vector2.Zero, 200f, true)
        {
            //Mass = 200f;
            //InvMass = 1f / Mass;
        }

        public RigidBody(Vector2 position, float mass, bool isDynamic)
        {
            Position = position;
            Mass = mass;
            InvMass = 1 / Mass;
            IsDynamic = isDynamic;
            LinearVelocity = Vector2.Zero;
            CurrentForce = Vector2.Zero;
        }

        public void Step(float dt, Vector2 Gravity, float damping)
        {
            if (IsDynamic)
            {
                Vector2 force = Vector2.Zero;

                //F = mg
                force = Gravity * Mass;
                //Add forces due to spring joints
                force += CurrentForce;
                //Apply damping
                force -= LinearVelocity * damping;

                //Newtons 2nd law
                //LinearAcceleration = Gravity + CurrentForce - LinearVelocity * damping;
                LinearAcceleration = force * InvMass;
                LinearVelocity += LinearAcceleration * dt;
                Position += LinearVelocity * dt;

                CurrentForce = Vector2.Zero;
            }
        }

        public void Step(float dt, Vector2 Gravity, Vector2 WorldForce, float damping)
        {
            if (IsDynamic)
            {
                Vector2 force = WorldForce;

                //F = mg
                force += Gravity * Mass;
                force += CurrentForce;
                force -= LinearVelocity * damping;

                //Newtons 2nd law
                LinearAcceleration = force * InvMass;
                LinearVelocity += LinearAcceleration * dt;
                Position += LinearVelocity * dt;

                CurrentForce = Vector2.Zero;
            }
            
            //Reset the force of the object
            //Force = Vector2.Zero;

            //Euler Integration
            //LinearVelocity += (Gravity + LinearAcceleration) * dt;

            // Position += LinearVelocity * dt;
        }
    }
}
