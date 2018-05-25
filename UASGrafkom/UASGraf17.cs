using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace UASGrafkom
{
    public class UASGraf17 : GameWindow
    {
        Matrix4 view, projection, model;
        List<TriangleClass> triList;
        

        public UASGraf17() : base(600,600)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            createTriangle(4);
            GL.Enable(EnableCap.DepthTest);
        }

        public void createTriangle(int n)
        {
            if (n > 0)
            {
                triList = new List<TriangleClass>();
                float angleplus = (float)(Math.PI * 2 / (float)n);
                float redshade = 255 / n;
                for (int i = 0; i < n; i++)
                {
                    TriangleClass temp = new TriangleClass(Color.FromArgb((int)(redshade * i), 0, 0));
                    temp.Angle = angleplus * i;
                    triList.Add(temp);
                }
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Key.A))
            {
               foreach(TriangleClass c in triList)
                {
                    if (c.Ismoving == false)
                    {
                        c.TargetAngle = (float)(Math.PI * 2 / (float)triList.Count);
                    }
                }
            }
            if (kstate.IsKeyDown(Key.D))
            {
                foreach (TriangleClass c in triList)
                {
                    if (c.Ismoving == false)
                    {
                        c.TargetAngle = -(float)(Math.PI * 2 / (float)triList.Count);
                    }
                }
            }
            if (kstate.IsKeyDown(Key.W))
            {
                createTriangle(triList.Count + 1);
            }
            if (kstate.IsKeyDown(Key.S))
            {
                createTriangle(triList.Count - 1);
            }

            foreach (TriangleClass c in triList)
            {
                c.Update();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)this.Width / this.Height, 1, 100);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            view = Matrix4.LookAt(new Vector3(0, 5, 20), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            foreach(TriangleClass c in triList)
            {
                c.Draw(view);
            }
            

            GL.Flush();
            SwapBuffers();
        }



    }
}
