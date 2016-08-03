using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothPhysics
{
    public partial class MainWindow : Form
    {
        Renderer mRenderer;
        MassSpring mMassSpring;
        World mWorld;
        Stopwatch mStopwatch;
        long mCurrentTime, mNewTime;
        static float PHYSICS_STEP = 1 / 60;
        float mPhysicsTime = 0f;

        public MainWindow()
        {
            InitializeComponent();
            mStopwatch = new Stopwatch();
            mStopwatch.Start();
            mCurrentTime = mStopwatch.ElapsedMilliseconds;
            mRenderer = new Renderer();

            mWorld = new World();
            //mMassSpring = new MassSpring(25, 20, new Vector2(50, 50));
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            mNewTime = mStopwatch.ElapsedMilliseconds;
            long delta = mNewTime - mCurrentTime;
            mCurrentTime = mNewTime;

            mPhysicsTime += delta * 0.001f;
            if(mPhysicsTime > PHYSICS_STEP)
            {
                mWorld.Update(mPhysicsTime);
                //mMassSpring.Update(mPhysicsTime);
                mPhysicsTime = 0f;
            }
            
            mRenderer.Draw(mWorld.mMassSpring.Grid, graphics);

            this.Invalidate();
        }
    }
}
