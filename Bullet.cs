using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public class Bullet
    {
        public int dirX = 10;
        public PictureBox box;
        public Bullet()
        {
            box = new PictureBox();
            box.Size = new Size(20, 10);
            box.BackColor = Color.Black;
            box.Location = new Point(-10, -100);
        }

        public void MoveBullet()
        {
            box.Left += dirX;
        }
        // Проверяет, достигла ли пуля стены
        public bool InMapBoundaries()
        {
            if (box.Left <= 0)
                return false;
            if (box.Right >= 800 - 15)
                return false;
            else
                return true;
        }
    }
}
