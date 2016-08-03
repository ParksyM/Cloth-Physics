using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class SpringJoint
    {
        RigidBody mBodyA, mBodyB;
        //RigidBody mBodyB;
        float mSpringKoeff;
        float mRestLength;
        float mStiffnessKoeff;

        public SpringJoint(RigidBody bodyA, RigidBody bodyB, float springKoeff, float stiffnessKoeff) : this(bodyA, bodyB, springKoeff, stiffnessKoeff, Vector2.Distance(bodyA.Position, bodyB.Position))
        { }

        public SpringJoint(RigidBody bodyA, RigidBody bodyB, float springKoeff, float stiffnessKoeff,  float restLength)
        {
            mBodyA = bodyA;
            mBodyB = bodyB;
            mSpringKoeff = springKoeff;
            mStiffnessKoeff = stiffnessKoeff;
            mRestLength = restLength;
        }

        public void ApplyForce()
        {
            //Hookes law
            //F = -k(l-l0)
            Vector2 direction = mBodyB.Position - mBodyA.Position;
            float force = -mSpringKoeff * (direction.Length() - mRestLength);
            mBodyB.CurrentForce += force * direction.Normalise();

            //return -SpringKoeff * (length - RestLength);
        }


        /// <summary>
        /// Attempt to limit stretching factor in order to better simulate real world cloth
        /// Based on research paper available at: 
        /// https://graphics.stanford.edu/courses/cs468-02-winter/Papers/Rigidcloth.pdf
        /// </summary>
        public void ApplyStiffness()
        {
            float stiffLength = mRestLength * (1 + mStiffnessKoeff);

            float lengthDiff = Vector2.Distance(mBodyA.Position, mBodyB.Position) - stiffLength;
            if(lengthDiff > 0)
            {
                Vector2 direction = (mBodyB.Position - mBodyA.Position).Normalise();

                if(!mBodyA.IsDynamic)
                {
                    if (mBodyB.IsDynamic)
                    {
                        //Move B towards A
                        mBodyB.Position -= direction * lengthDiff;
                    }
                    //else neither body is dynamic, this shouldn't happen
                }
                else
                {
                    //mBodyA is dynamic
                    if (!mBodyB.IsDynamic)
                    {
                        //Move A towards B
                        mBodyA.Position += direction * lengthDiff;
                    }
                    else
                    {
                        //Both are dynamic, move each towards the other by half the amount
                        mBodyA.Position += direction * (lengthDiff / 2f);
                        mBodyB.Position -= direction * (lengthDiff / 2f);
                    }
                }
            }
        }
    }
}
