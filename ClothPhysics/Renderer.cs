using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothPhysics
{
    class Renderer
    {
        private Pen mPen;
        private SolidBrush mBrush;
        private float mNodeRadius;
        private float mNodeDiameter;

        public Renderer()
        {
            mPen = new Pen(Color.Black);
            mPen.Width = 1f;
            mBrush = new SolidBrush(Color.Black);

            mNodeRadius = 2f;
            mNodeDiameter = mNodeRadius * 2;
        }

        public void Draw(List<SpringNode> nodes, Graphics g)
        {
            foreach(SpringNode n in nodes)
            {
                Vector2 position = n.rigidBody.Position;

                g.DrawEllipse(mPen, 
                    position.X - mNodeRadius, 
                    position.Y - mNodeRadius, 
                    mNodeDiameter, 
                    mNodeDiameter
                );

                //g.FillEllipse(mBrush,
                //    position.X - NodeRadius,
                //    position.Y - NodeRadius, 
                //    NodeDiameter, 
                //    NodeDiameter
                //);

                foreach (RigidBody body in n.neighbours)
                {
                    g.DrawLine(mPen,
                        new Point((int)position.X, (int)position.Y),
                        new Point((int)body.Position.X, (int)body.Position.Y)
                    );
                }

                //foreach(RigidBody body in n.diagonalNeighbours)
                //{
                //    g.DrawLine(mPen,
                //        new Point((int)position.X, (int)position.Y),
                //        new Point((int)body.Position.X, (int)body.Position.Y)
                //    );
                //}
            }
        }
    }
}
