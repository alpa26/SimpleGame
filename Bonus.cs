using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public class Bonus
    {
        public PictureBox speed;
        public PictureBox slower;

        public Bonus()
        {
            speed = new PictureBox()
            {
                Size = new Size(30, 30),
                Location = new Point(new Random().Next(20, 700), 
                new Random().Next(20, 400)),
                Image = Properties.Resources.speed
            };
            slower = new PictureBox() 
            { 
                Size = new Size(30, 30),
                Location = new Point(-100, -100),
                Image = Properties.Resources.slower 
            };
        }
    }
}
