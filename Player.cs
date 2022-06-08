using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public class Player 
    {
        public Physics myPhysics;
        public int dirX;
        public int dirY;
        public bool isMoving;
        public int score = 0;
        public int speed = 6;
        public Player(Point point)
        {
            myPhysics = new Physics(point);
        }

        public void Move()
        {
            myPhysics.box.Left += dirX;
        }

        public bool ChecksSideBorders(PictureBox obj)
        {
            return (myPhysics.box.Left < obj.Right &&  obj.Right < myPhysics.box.Right ||
                myPhysics.box.Left < obj.Left && obj.Left < myPhysics.box.Right);
        }

        // Взаимодействие с другим объектом
        public bool Interaction(PictureBox obj)
        {
            if (ChecksSideBorders(obj) &&
                ( myPhysics.box.Top < obj.Bottom && obj.Bottom < myPhysics.box.Bottom))
                return true;
            else if (ChecksSideBorders(obj) &&
                ( myPhysics.box.Top < obj.Top && obj.Bottom < myPhysics.box.Bottom))
                return true;
            else if (ChecksSideBorders(obj) &&
                ( myPhysics.box.Top<obj.Top && obj.Top < myPhysics.box.Bottom))
                return true;
            else
                return false;
        }
        
        public void MovePlayer()
        {
            if (myPhysics.box.Left > 0 && myPhysics.box.Right < 800)
                Move();
            if (myPhysics.box.Left <= 0)
                myPhysics.box.Left += 1;
            if (myPhysics.box.Right >= 800-15)
                myPhysics.box.Left -= 1;
        }

        public void fasterSpeed()
        {
            speed = 10;
            Thread.Sleep(5000);
            speed = 6;
        }

        public void slowerSpeed()
        {
            speed = 4;
            Thread.Sleep(5000);
            speed = 6;
        }
    }
}
