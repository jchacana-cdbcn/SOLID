﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MarsRoverKata
{
    public class Mars 
    {
        public Size Bounds { get; private set; }
        public Point CenterOfThePlanet { get; private set; }

        private readonly List<IObstacle> _obstacles;
        public IReadOnlyList<IObstacle> Obstacles
        {
            get { return _obstacles; }
        }

        public Mars()
            : this(new Size(25, 25))
        { }

        public Mars(Size bounds)
        {
            Bounds = bounds;
            CenterOfThePlanet = new Point(Bounds.Width / 2, Bounds.Height / 2);
            _obstacles = new List<IObstacle>();
        }

        public void UpdateAliens()
        {
            var aliens = Obstacles.OfType<Alien>().ToList();
            foreach (var alien in aliens)
            {
                alien.DoStuff();
            }
        }

        public void AddObstacle(Point location)
        {
            _obstacles.Add(new Obstacle(location));
        }

        public void AddObstacle(IObstacle obstacle)
        {
            _obstacles.Add(obstacle);
        }

        public void RemoveObstacle(IObstacle obstacle)
        {
            _obstacles.Remove(obstacle);
        }

        public Point CalculateFinalPosition(Point from, Point desired)
        {
            Point newDestination = desired;
            newDestination = CalculatePositionY(desired, newDestination);
            newDestination = CalculatePositionX(desired, newDestination);

            if (!IsValidPosition(newDestination))
            {
                return from;
            }

            return newDestination;
        }

        private Point CalculatePositionX(Point desired, Point newDestination)
        {
            if (desired.X > Bounds.Width)
            {
                newDestination = new Point(0, desired.Y);
            }
            if (desired.X < 0)
            {
                newDestination = new Point(Bounds.Width, desired.Y);
            }
            return newDestination;
        }

        private Point CalculatePositionY(Point desired, Point newDestination)
        {
            if (desired.Y > Bounds.Height)
            {
                newDestination = new Point(desired.X, 0);
            }
            if (desired.Y < 0)
            {
                newDestination = new Point(desired.X, Bounds.Height);
            }
            return newDestination;
        }

        public bool IsValidPosition(Point point)
        {
            bool anyInstance = _obstacles.Any(x => x.Location.Equals(point));

            return !anyInstance;
        }
    }
}
