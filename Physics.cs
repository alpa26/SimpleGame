using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public class Physics
    {
        public PictureBox box;
        double gravity;
        double a;
        public Physics(Point point)
        {
            box = new PictureBox();
            box.Location = point;
            box.Size = new Size(50, 85);
            gravity = 0;
            a = 0.5;
        }
        
        public void ApplyPhysics(List<PictureBox> boxes)
        {
            CalculatePhysics(boxes);
        }

        public void CalculatePhysics(List<PictureBox> boxes)
        {
            if (CheckObstacle(boxes) || a < 0)
            {
                if (a != 0.5f)
                    a += 0.025f;
                box.Top += (int)gravity;
                gravity += a;
            }
        }
        // Проверяет наличие препятствий 
        public bool CheckObstacle(List<PictureBox> boxes)
        {
            if (boxes != null)
                foreach (var box1 in boxes)
                {
                if (box.Right > box1.Left && box.Left < box1.Right
                    && !(box.Top > box1.Bottom - box.Height))
                    return box.Bottom <= box1.Top;
                }
            return (box.Bottom <= 500-50);
        }

        public bool CheckSurface(List<PictureBox> boxes)
        {
            if(boxes!= null)
                foreach (var box1 in boxes)
                {
                if (box.Right > box1.Left && box.Left < box1.Right
                    && !(box.Top > box1.Bottom - box.Height))
                    return box.Bottom >= box1.Top;
                }
            return (box.Bottom >= 500-50);
        }

        public void Jumping(List<PictureBox> boxes)
        {
            if(CheckSurface(boxes))
            {
                gravity = 0;
                a = -0.5f;
            }
        }
    }
}
