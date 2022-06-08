using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public class Level
    {
        public Bitmap image;
        public string Name;
        public Player player;
        public Player player2;
        public Enemy[] enemies;
        public List<PictureBox> panels;
        public Level(string numb, Player p, Player p2, List<PictureBox> pnls,Bitmap im)
        {
            image = im;
            Name = numb;
            player = p;
            player2 = p2;
            panels = pnls; 
        }
    }
}
