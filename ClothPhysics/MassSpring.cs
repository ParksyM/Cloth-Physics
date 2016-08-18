using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class MassSpring
    {
        public List<SpringNode> Grid;
        //float[] Forces;
        float SpringKoeff = 10000f;
        float StiffnessKoeff = 0.1f;
        float SpawnLength = 20f;
        float RestLength = 20f;
        float RestLengthDiag;// = (float)Math.Sqrt(RestLength*RestLength)
        float NodeMass = 10f;
        float SpringDamping = 20f;

        //float grav = 98f;
        //float gravScale = 0.5f;

        Vector2 Gravity = new Vector2(0, 98f);
        //The force applied to the cloth by the world. Used to represent wind.
        Vector2 WorldForce = new Vector2();
        Random Random;

        float WorldForceTime = 1f;
        float WorldForceDeltaTime = 0f;

        //TODO: Tidy up static node constraints
        //consider changing nodes after entire grid is generated

        bool LockTop = true;
        bool LockBottom = false;
        bool LockLeft = false;
        bool LockRight = false;

        bool LockTopLeft = true;
        bool LockTopRight = true;
        bool LockBottomLeft = false;
        bool LockBottomRight = false;

        bool DoDiagonal = false;

        bool ApplyExperimentalStiffness = false;

        public MassSpring(int nodeWidth, int nodeHeight, Vector2 gridOffset)
        {
            Random = new Random();
            //Gravity = new Vector2(0, grav * gravScale);
            RestLengthDiag = (float)Math.Sqrt(2 * RestLength * RestLength);
            //RestLengthDiag = 70.7106781187f;
            //RestLengthDiag = 75f;
            Grid = new List<SpringNode>(nodeWidth * nodeHeight);
            SpringNode newNode;

            bool dynamic;

            //Generate grid
            for(int y = 0; y < nodeHeight; y++)
            {
                for(int x = 0; x < nodeWidth; x++)
                {
                    dynamic = true;

                    if (y == 0)
                    {
                        if(LockTop || LockTopLeft && x == 0 || LockTopRight && x == nodeWidth - 1)
                        {
                            dynamic = false;
                        }
                    }

                    if (y == nodeHeight - 1)
                    {
                        if(LockBottom || LockBottomLeft && x == 0 || LockBottomRight && x == nodeWidth - 1)
                        {
                            dynamic = false;
                        }
                    }

                    if (LockLeft && x == 0 || LockRight && x == nodeWidth - 1)
                    {
                        dynamic = false;
                    }

                    newNode = new SpringNode(new RigidBody(gridOffset + new Vector2(SpawnLength * x, SpawnLength * y), NodeMass, dynamic));
                    //newNode.neighbours = new List<RigidBody>();
                    Grid.Add(newNode);
                }
            }

            int index = 0;
            //Add neighbour nodes
            for(int y = 0; y < nodeHeight; y++)
            {
                for(int x = 0; x < nodeWidth; x++)
                {
                    index = y * nodeWidth + x;

                    //Left
                    if(x > 0)
                    {
                        Grid[index].AddNeighbour(Grid[index - 1].rigidBody, SpringKoeff, StiffnessKoeff, RestLength);
                        //Grid[index].neighbours.Add(Grid[index - 1].rigidBody);
                    }
                    //Right
                    if(x < nodeWidth - 1)
                    {
                        Grid[index].AddNeighbour(Grid[index + 1].rigidBody, SpringKoeff, StiffnessKoeff, RestLength);
                        //Grid[index].neighbours.Add(Grid[index + 1].rigidBody);
                    }
                    //Up
                    if(y > 0)
                    {
                        Grid[index].AddNeighbour(Grid[index - nodeWidth].rigidBody, SpringKoeff, StiffnessKoeff, RestLength);
                        //Grid[index].neighbours.Add(Grid[index - nodeWidth].rigidBody);
                    }
                    //Down
                    if(y < nodeHeight - 1)
                    {
                        Grid[index].AddNeighbour(Grid[index + nodeWidth].rigidBody, SpringKoeff, StiffnessKoeff, RestLength);
                        //Grid[index].neighbours.Add(Grid[index + nodeWidth].rigidBody);
                    }
                }
            }
        }

        public void Update(float dt)
        {
            WorldForceDeltaTime += dt;
            if(WorldForceDeltaTime > WorldForceTime)
            {
                WorldForce = new Vector2((float)Random.NextDouble() * 1000f, 0);
                WorldForceDeltaTime = 0f;
            }

            foreach (SpringNode node in Grid)
            {
                foreach (SpringJoint joint in node.springJoints)
                {
                    joint.ApplyForce();
                }
            }

            foreach (SpringNode node in Grid)
            {
                //node.rigidBody.Step(dt, Gravity, node.currentForce + LeftForce, SpringDamping);
                node.rigidBody.Step(dt, Gravity, WorldForce, SpringDamping);
            }

            /// <summary>
            /// Attempt to limit stretching factor in order to better simulate real world cloth
            /// Based on research paper available at: 
            /// https://graphics.stanford.edu/courses/cs468-02-winter/Papers/Rigidcloth.pdf
            /// </summary>
            if (ApplyExperimentalStiffness)
            {
                foreach (SpringNode node in Grid)
                {
                    foreach (SpringJoint joint in node.springJoints)
                    {
                        joint.ApplyStiffness();
                    }
                }
            }
        }
    }
}
