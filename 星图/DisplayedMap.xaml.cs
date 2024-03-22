using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 星图
{
    /// <summary>
    /// DisplayedMap.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayedMap : Window
    {
        public Star? rootStar;
        public Map? map;
        public double starSize = 60;
        public double minHeightDistance = 120;
        public int maxMapDepth = 5;

        public DisplayedMap()
        {
            InitializeComponent();
            //DrawMap();
        }

        public void DrawMap()
        {
            if (rootStar is null || DataContext is null|| DataContext is not Map)
            {
                return;
            }

            map = DataContext as Map;
         
            if (rootStar.Lanes.Count == 0)
            {
                DrawMapMain();
                return;
            }

            if (rootStar.Lanes.Count == 1)
            {
                rootStar.Coordinate = new Point(this.Width / 2, 100);
                List<double> branchPosition = [0];
                double branchRadian = Math.PI * 2 / 3;

                SetCoordinates(branchPosition, branchRadian);
                DrawMapMain();
                return;
            }

            if (rootStar.Lanes.Count == 2)
            {
                rootStar.Coordinate = new Point(this.Width / 2, this.Height / 2);
                List<double> branchPosition = [90, 270];
                double branchRadian = Math.PI * 2 / 3;

                SetCoordinates(branchPosition, branchRadian);
                DrawMapMain();
                return;
            }

            if (rootStar.Lanes.Count > 2)
            {
                rootStar.Coordinate = new Point(this.Width / 2, this.Height / 2);
                List<double> branchPosition = new List<double>(rootStar.Lanes.Count);
                double branchRadian = Math.PI * 2 / rootStar.Lanes.Count;
                double branchDegree = 360.0 / rootStar.Lanes.Count;
                for (int i = 0; i < rootStar.Lanes.Count; i++)
                {
                    branchPosition.Add(i * branchDegree);
                }

                SetCoordinates(branchPosition, branchRadian);
                DrawMapMain();
                return;
            }

            void SetCoordinates(List<double> branchPosition, double branchRadian)
            {
                if (rootStar.Lanes.Count != branchPosition.Count)
                {
                    throw new Exception("SetCoordinates: the number of neighbors should equal to the number of branch position");
                }

                map.Clean();

                List<List<Star>> parentFloors = new List<List<Star>>(rootStar.Lanes.Count);
                for (int i = 0; i < rootStar.Lanes.Count; i++)
                {
                    parentFloors.Add(new List<Star>());
                }

                double[] minHeight = new double[branchPosition.Count];
                for (int i = 0; i < branchPosition.Count; i++)
                {
                    minHeight[i] = minHeightDistance;
                }

                rootStar.Visited = true;
                for (int i = 0; i < rootStar.Lanes.Count; i++)
                {
                    rootStar.Lanes[i].OrganizeEndpoint(rootStar);
                    parentFloors[i].Add(rootStar.Lanes[i].Endpoint2_Star);
                    rootStar.Lanes[i].Endpoint2_Star.Visited = true;
                }

                for (int floor = 0; floor < maxMapDepth; floor++)
                {
                    for (int branch = 0; branch < branchPosition.Count; branch++)
                    {
                        List<Star> parentFloor = parentFloors[branch];
                        List<Star> childFloor = [];

                        if (parentFloor.Count == 0)
                        {
                            continue;
                        }

                        foreach (Star parentStar in parentFloor)
                        {
                            parentStar.Visited = true;
                            foreach (Lane l in parentStar.Lanes)
                            {
                                l.OrganizeEndpoint(parentStar);
                                Star childStar = l.Endpoint2_Star;
                                if (!childStar.Visited)
                                {
                                    childFloor.Add(childStar);
                                }
                            }
                        }

                        double requiredHalfWidth = parentFloor.Count * starSize / 2;
                        double requiredHeight = requiredHalfWidth / Math.Tan(branchRadian / 2) + starSize / 2;
                        double requiredInterval = starSize;
                        if (requiredHeight < minHeight[branch])
                        {
                            requiredHeight = minHeight[branch];
                            requiredHalfWidth = (requiredHeight - starSize / 2) * Math.Tan(branchRadian / 2);
                            requiredInterval = requiredHalfWidth * 2 / parentFloor.Count;
                        }

                        double startPosition;
                        if (parentFloor.Count % 2 == 0)
                        {
                            startPosition = requiredInterval / 2 - parentFloor.Count / 2 * requiredInterval;
                        }
                        else
                        {
                            startPosition = -(parentFloor.Count / 2 * requiredInterval);
                        }

                        System.Windows.Point[] coors = new System.Windows.Point[parentFloor.Count];
                        for (int i = 0; i < parentFloor.Count; i++)
                        {
                            coors[i] = new System.Windows.Point(startPosition + i * requiredInterval, requiredHeight);
                        }

                        // Rotation
                        Matrix rotationMatrix = Matrix.Identity;
                        rotationMatrix.Rotate(branchPosition[branch]);
                        rotationMatrix.Transform(coors);

                        // Add offset to center on root's coordination
                        for (int i = 0; i < parentFloor.Count; i++)
                        {
                            coors[i].X += rootStar.Coordinate.X;
                            coors[i].Y += rootStar.Coordinate.Y;
                        }

                        for (int i = 0; i < parentFloor.Count; i++)
                        {
                            parentFloor[i].Coordinate = coors[i];
                        }

                        parentFloors[branch] = childFloor;
                        minHeight[branch] = requiredHeight + minHeightDistance;
                    }

                    bool stop = true;
                    foreach (var parentFloor in parentFloors)
                    {
                        if (parentFloor.Count > 0)
                        {
                            stop = false;
                            break;
                        }
                    }
                    if (stop)
                    {
                        break;
                    }
                }
            }

            void DrawMapMain()
            {
                map.Clean();

                List<Star> parentFloor = [rootStar];
                while (parentFloor.Count > 0)
                {
                    List<Star> childFloor = [];
                    foreach (Star parent in parentFloor)
                    {
                        parent.Visited = true;
                        foreach (Lane l in parent.Lanes)
                        {
                            l.OrganizeEndpoint(parent);
                            Star child = l.Endpoint2_Star;

                            if (!child.Visited)
                            {
                                childFloor.Add(child);

                                Line line = new Line();
                                line.X1 = parent.Coordinate.X;
                                line.X2 = child.Coordinate.X;
                                line.Y1 = parent.Coordinate.Y;
                                line.Y2 = child.Coordinate.Y;

                                line.Style = DisplayedMapCanvas.TryFindResource($"{l.Type}") as Style;
                                DisplayedMapCanvas.Children.Add(line);
                            }
                        }

                        Button starButton = new Button();

                        starButton.Style = DisplayedMapCanvas.TryFindResource($"StarButton") as Style;
                        Image starImage = new();
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(@$"/src/{parent.StarType}.png", UriKind.Relative);
                        bitmapImage.EndInit();
                        starImage.Source = bitmapImage;
                        starButton.Content = starImage;
                        starButton.DataContext = parent;

                        Canvas.SetLeft(starButton, parent.Coordinate.X - starButton.Width / 2);
                        Canvas.SetTop(starButton, parent.Coordinate.Y - starButton.Height / 2);
                        DisplayedMapCanvas.Children.Add(starButton);
                    }
                    parentFloor = childFloor;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DataContext.ToString());
            MessageBox.Show(rootStar?.Name);

            DrawMap();
        }
    }
}
