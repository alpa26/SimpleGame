using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
	class CreateLevel
	{
		public static List<PictureBox> Boxes1 = new List<PictureBox>()
		{
			new PictureBox(){ Location =new Point(0, 150)},
			new PictureBox(){ Location =new Point(700, 150)},
			new PictureBox(){ Location =new Point(200, 250), Size = new Size(390, 20), Image = Properties.Resources.platform2 },
			new PictureBox(){ Location =new Point(0, 350)},
			new PictureBox(){ Location =new Point(700, 350)},
			new PictureBox(){ Location =new Point(350, 400)},
		};
		public static List<PictureBox> Boxes2 = new List<PictureBox>()
		{
			new PictureBox(){ Location =new Point(0, 150)},
			new PictureBox(){ Location =new Point(350, 150)},
			new PictureBox(){ Location =new Point(700, 150),Size = new Size(100, 20),Image = Properties.Resources.platform},
			new PictureBox(){ Location =new Point(200, 250)},
			new PictureBox(){ Location =new Point(500, 250)},
			new PictureBox(){ Location =new Point(0, 350)},
			new PictureBox(){ Location =new Point(700, 350)},
			new PictureBox(){ Location =new Point(350, 400)},
		};

		public static Level SinglePlay = new Level("SinglePlay",
			new Player(new Point(20, 300)),
			new Player(new Point(-100, 500)),
			CreateLevel.CreateBoxes(Properties.Resources.platform, Boxes2),
			Properties.Resources.Pustina);

		public static Level DesertMap = new Level("DesertMap",
			new Player(new Point(10, 170)),
			new Player(new Point(700, 170)),
			CreateLevel.CreateBoxes(Properties.Resources.platform, Boxes2),
			Properties.Resources.Pustina);

		public static Level CityMap = new Level("CityMap",
			new Player(new Point(10, 170)),
			new Player(new Point(700, 170)),
			CreateLevel.CreateBoxes(Properties.Resources.platform, Boxes1),
			Properties.Resources.City);

		public static List<PictureBox> CreateBoxes(Bitmap bit, List<PictureBox> boxes)
        {
			foreach(var box in boxes)
            {
				if (box != boxes[2])
                {
					box.Image = bit;
					box.Size = new Size(100, 20);
				}
			}
			return boxes;
		}
		public static IEnumerable<Level> CreateLevels()
		{
			yield return DesertMap;
			yield return CityMap;
			yield return SinglePlay;
		}
	}
}
