using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public class SinglyLinkedList<T>
    {
        public readonly T Value;
        public readonly SinglyLinkedList<T> Previous;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
        }

    }
    public class Enemy
    {
        public PictureBox box;
        public List<Point> way;

        public Enemy(Point point)
        {
            box = new PictureBox();
            box.Location = point;
            box.Size = new Size(60, 30);
            box.Image = Properties.Resources.Orel;
        }

        // Двигается, с учетом препятствий
        public void DoMove(Player player, List<PictureBox> boxes)
        {
            way = DoWay(player, boxes);
            if(way.Count!=0)
            {
                var dPoint = new Point(way[0].X - box.Location.X, way[0].Y - box.Location.Y);
                if (dPoint == new Point(0, 10)) box.Top += 5;
                else if (dPoint == new Point(0, -10)) box.Top -= 5;
                else if (dPoint == new Point(10, 0))
                {
                    box.Left += 5;
                    box.Image = Properties.Resources.Orel1;
                }
                else if (dPoint == new Point(-10, 0))
                {
                    box.Left -= 5;
                    box.Image = Properties.Resources.Orel;
                }
            }
        }

        // Формирует путь в List
        public  List<Point> DoWay(Player player, List<PictureBox> boxes)
        {
            var point = FindPaths(box.Location, player.myPhysics.box, boxes);
            if (point == null) return new List<Point>();
            var way = new List<Point>();
            while(point.Previous != null)
            {
                way.Add(point.Value);
                point = point.Previous;
            }
            way.Reverse();
            return way;
        }

        // Нетривиальный алгоритм, ищет игрока
        public  SinglyLinkedList<Point> FindPaths(Point start, PictureBox player, List<PictureBox> boxes)
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>();
            queue.Enqueue(new SinglyLinkedList<Point>(start, null));
            visited.Add(start);
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (Contains(player,point.Value))
                    return point;
                for (var dx = -10; dx <= 10; dx+=10)
                    for (var dy = -10; dy <= 10; dy+=10)
                    {
                        if (dx != 0 && dy != 0) continue;
                        var nearbyPoint = new Point { X = point.Value.X + dx, Y = point.Value.Y + dy };
                        if (visited.Contains(nearbyPoint) || ContainBoxes(boxes, nearbyPoint)) continue;
                        visited.Add(nearbyPoint);
                        queue.Enqueue(new SinglyLinkedList<Point>(nearbyPoint, point));
                    }
            }
            return null;
        }


        public bool Contains(PictureBox obstacle, Point point)
        {
            if((obstacle.Left-box.Size.Width < point.X && point.X < obstacle.Right) &&
                (obstacle.Top- box.Size.Height < point.Y && point.Y < obstacle.Bottom))
                return true;
            return false;
        }

        // Проверяет препятствия (платформы)
        public bool ContainBoxes(List<PictureBox> boxes, Point point)
        {
            foreach(var platform in boxes)
            {
                if (Contains(platform, point))
                    return true;
            }
            return false;
        }

        public bool ChecksSideBorders(PictureBox obj)
        {
            return (box.Left < obj.Right && obj.Right < box.Right ||
                box.Left < obj.Left && obj.Left < box.Right);
        }

        public bool Interaction(PictureBox obj)
        {
            if (ChecksSideBorders(obj) &&
                (box.Top < obj.Bottom && obj.Bottom < box.Bottom))
                return true;
            else if (ChecksSideBorders(obj) &&
                (box.Top < obj.Top && obj.Bottom < box.Bottom))
                return true;
            else if (ChecksSideBorders(obj) &&
                (box.Top < obj.Top && obj.Top < box.Bottom))
                return true;
            else
                return false;
        }
        // Новая позиция в зависимости от позиции игрока
        public void MakeNewLocation(Point player)
        {
            if(0<player.Y && player.Y<150)
                box.Location= new Point(new Random().Next(100, 700), new Random().Next(270, 400));
            else if (150 < player.Y && player.Y < 350)
                box.Location = new Point(new Random().Next(0, 700), new Random().Next(0, 130));
            else if (350 < player.Y && player.Y < 500)
                box.Location = new Point(new Random().Next(0, 700), new Random().Next(170, 250));
        }
    }
}
