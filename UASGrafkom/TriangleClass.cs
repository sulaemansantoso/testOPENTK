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
    public class TriangleClass
    {
        Matrix4 model;
        Color color;
        float angle = 0;
        float distance = 5;
        bool ismoving = false;
        float targetAngle = 0;
        float itterate = 0;

        public Matrix4 Model { get => model; set => model = value; }
        public float Angle { get => angle; set => angle = value; }
        public float TargetAngle { get => targetAngle; set => targetAngle = value; }
        public bool Ismoving { get => ismoving; set => ismoving = value; }

        public TriangleClass(Color color)
        {
            Model = Matrix4.Identity;
            Model = Matrix4.Mult(Matrix4.CreateTranslation(0, 0, distance), Model);

            Model = Matrix4.Mult(Matrix4.CreateRotationY(Angle), Model);
           
            this.color = color;
        }

        public void Update()
        {
            if (TargetAngle> 0)
            {
                Ismoving = true;
                itterate = -0.1f;
            }
            if (targetAngle < 0)
            {
                Ismoving = true;
                itterate = 0.1f;
            }

            if (Ismoving == true) {
                angle += itterate;
                TargetAngle += itterate; 
                if ( Math.Abs( TargetAngle) <= itterate*2)
                {
                    targetAngle = 0;
                    Ismoving = false;
                }
            }
        }

        public void Draw(Matrix4 view)
        {
            Model = Matrix4.Identity;
            Model = Matrix4.Mult(Matrix4.CreateRotationY(Angle), Model);
            Model = Matrix4.Mult(Matrix4.CreateTranslation(0, 0, distance), Model);

            Matrix4 modelView = Matrix4.Mult(Model, view);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);
            createTriangle(); 
        }

        public void createTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);
            GL.Vertex3(-1, -1, 0);
            GL.Vertex3(0, 1, 0);
            GL.Vertex3(1, -1, 0);
            GL.End();
        }

    }
}
