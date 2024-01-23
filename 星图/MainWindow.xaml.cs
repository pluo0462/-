using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography.Xml;
using System.Collections;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace 星图
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DenseDraw(Test());
        }

        internal Star Test()
        {
            Star A = new Star("A", "I am A", Map.StarType.Pulsar);
            Star B = new Star("B", "I am B", Map.StarType.BlackHole);
            Star C = new Star("C", "I am C", Map.StarType.BlackHole);
            Star D = new Star("D", "I am D", Map.StarType.BlackHole);
            Star E = new Star("E", "I am E", Map.StarType.ClassA);
            Star F = new Star("F", "I am F");
            Star G = new Star("G", "I am G");
            Star H = new Star("H", "I am H");
            Star K = new Star("K", "I am K");
            Star I = new Star("I", "I am I");
            Star J = new Star("J", "I am J");
            Star L = new Star("L", "I am L");
            Star M = new Star("M", "I am M");
            Star N = new Star("N", "I am N");
            Star O = new Star("O", "I am O");
            Star P = new Star("P", "I am P");


            Star.ConnectStar(A, B);
            Star.ConnectStar(A, C);
            Star.ConnectStar(C, D);
            Star.ConnectStar(C, E);
            Star.ConnectStar(C, N);
            Star.ConnectStar(C, O);
            Star.ConnectStar(C, P);
            Star.ConnectStar(A, F);
            Star.ConnectStar(E, G);
            Star.ConnectStar(D, H);
            Star.ConnectStar(D, K);
            Star.ConnectStar(B, I);
            Star.ConnectStar(A, J);
            Star.ConnectStar(A, L);
            Star.ConnectStar(A, M);

            I.Coordinate = new System.Windows.Point(1000, 500);

            return I;
        }

        internal void DenseDraw(Star root)
        {
            if (root == null)
            {
                return;
            }

            // Magic Number for styling. May be changed to input parameter later.
            double starSize = 60;
            double minHeightDistance = 2 * starSize;

            if (root.Neighbors.Count == 0)
            {
            }
            else if (root.Neighbors.Count == 1)
            {
                List<double> branchPosition = [0];
                double branchRadian = Math.PI * 2 / 3;

                CoorsBody(root, branchPosition, branchRadian, minHeightDistance);

                DrawBody(root, starSize);

                //DrawBranchLine(root, branchPosition, branchRadian);
            }
            else if (root.Neighbors.Count == 2)
            {
                List<double> branchPosition = [90, 270];
                double branchRadian = Math.PI * 2 / 3;

                CoorsBody(root, branchPosition, branchRadian, minHeightDistance);

                DrawBody(root, starSize);

                //DrawBranchLine(root, branchPosition, branchRadian);
            }
            else
            {
                // Standard Condition.
                List<double> branchPosition = new List<double>(root.Neighbors.Count);
                double branchRadian = Math.PI * 2 / root.Neighbors.Count;
                double branchDegree = 360.0 / root.Neighbors.Count;

                for (int i = 0; i < root.Neighbors.Count; i++)
                {
                    branchPosition.Add(i * branchDegree);
                }

                CoorsBody(root, branchPosition, branchRadian, minHeightDistance);

                DrawBody(root, starSize);

                //DrawBranchLine(root, branchPosition, branchRadian);
            }

            
            
            void CoorsBody(Star root, List<double> branchPosition, double branchRadian, double minHeightDistance)
            {
                if (root.Neighbors.Count != branchPosition.Count)
                {
                    throw new Exception("CoorsBody: the number of neighbors should equal to the number of branch position");
                }

                Map.Clean();

                List<List<Star>> parentFloors = new List<List<Star>>(root.Neighbors.Count);
                for (int i = 0; i < root.Neighbors.Count; i++)
                {
                    parentFloors.Add(new List<Star>());
                }

                double[] minHeight = new double[branchPosition.Count];
                for (int i = 0; i < branchPosition.Count; i++)
                {
                    minHeight[i] = minHeightDistance;
                }

                root.Visited = true;
                var iter = root.Neighbors.Keys.GetEnumerator();
                for (int i = 0; i < root.Neighbors.Count; i++)
                {
                    iter.MoveNext();
                    parentFloors[i].Add(iter.Current);
                    iter.Current.Visited = true;
                }

                // Preventing infinite loop or restrict the map size
                int maxRecursion = 100000;
                for (int floor = 0; floor < maxRecursion; floor++)
                {
                    for (int branch = 0; branch < root.Neighbors.Count; branch++)
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
                            foreach (Star childStar in parentStar.Neighbors.Keys)
                            {
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

                            //MessageBox.Show($"requiredHeight: {requiredHeight}\n requiredHalfWidth: {requiredHalfWidth}");
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
                            coors[i].X += root.Coordinate.X;
                            coors[i].Y += root.Coordinate.Y;
                        }

                        for (int i = 0; i < parentFloor.Count; i++)
                        {
                            parentFloor[i].Coordinate = coors[i];
                            //MessageBox.Show($"{parentFloor[i].Name} has coordination of ({parentFloor[i].Coordinate.X}, {parentFloor[i].Coordinate.Y})");
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

            void DrawBody(Star root, double starSize)
            {
                Map.Clean();

                List<Star> parentFloor = [root];
                while (parentFloor.Count > 0)
                {
                    List<Star> childFloor = [];
                    foreach(Star parent in parentFloor)
                    {
                        parent.Visited = true;
                        foreach (var pair in parent.Neighbors)
                        {
                            Star child = pair.Key;
                            Path path = pair.Value;
                            if (!child.Visited)
                            {
                                childFloor.Add(child);

                                Line line = new Line();
                                line.X1 = parent.Coordinate.X;
                                line.X2 = child.Coordinate.X;
                                line.Y1 = parent.Coordinate.Y;
                                line.Y2 = child.Coordinate.Y;

                                line.Style = DisplayedMap.TryFindResource($"{path.Type}") as Style;
                                DisplayedMap.Children.Add(line);
                            }
                        }

                        //Ellipse ellipse = new Ellipse();
                        //ellipse.Width = starSize; ellipse.Height = starSize;
                        //ellipse.Fill = Brushes.Gold;
                        //Canvas.SetLeft(ellipse, parent.Coordinate.X - ellipse.Width / 2);
                        //Canvas.SetTop(ellipse, parent.Coordinate.Y - ellipse.Height / 2);
                        //DisplayedMap.Children.Add(ellipse);

                        Button starButton = new Button();
                        //starButton.Style = DisplayedMap.TryFindResource($"{parent.StarType}") as Style;

                        starButton.Style = DisplayedMap.TryFindResource($"StarButton") as Style;
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
                        DisplayedMap.Children.Add(starButton);

                        //Button starButton = new Button();
                        //starButton.Width = starSize;
                        //starButton.Height = starSize;
                        //Image starImage = new();
                        //BitmapImage bitmapImage = new BitmapImage();
                        //bitmapImage.BeginInit();
                        //bitmapImage.UriSource = new Uri("..\\..\\src\\36px-Pc_black_hole.png", UriKind.Relative);
                        //bitmapImage.EndInit();
                        //bitmapImage.Freeze();
                        //starImage.Source = bitmapImage;
                        //starButton.Content = starImage;
                        //Canvas.SetLeft(starButton, parent.Coordinate.X - starButton.Width / 2);
                        //Canvas.SetTop(starButton, parent.Coordinate.Y - starButton.Height / 2);
                        //DisplayedMap.Children.Add(starButton);
                    }
                    parentFloor = childFloor;
                }
            }

            void DrawBranchLine(Star root, List<double> branchPosition, double branchRadian)
            {
                for (int i = 0; i < branchPosition.Count; i++)
                {
                    Line line = new Line();
                    line.X1 = root.Coordinate.X;
                    line.X2 = root.Coordinate.X;
                    line.Y1 = root.Coordinate.Y;
                    line.Y1 = root.Coordinate.Y + 1000;
                    line.Stroke = Brushes.White;
                    line.StrokeThickness = 1;
                    line.RenderTransform = new RotateTransform(branchPosition[i] + branchRadian / Math.PI * 180 / 2, root.Coordinate.X, root.Coordinate.Y);
                    DisplayedMap.Children.Add(line);

                    Line line2 = new Line();
                    line2.X1 = root.Coordinate.X;
                    line2.X2 = root.Coordinate.X;
                    line2.Y1 = root.Coordinate.Y;
                    line2.Y1 = root.Coordinate.Y + 1000;
                    line2.Stroke = Brushes.White;
                    line2.StrokeThickness = 1;
                    line2.RenderTransform = new RotateTransform(branchPosition[i] - branchRadian / Math.PI * 180 / 2, root.Coordinate.X, root.Coordinate.Y);          
                    DisplayedMap.Children.Add(line2);
                }
            }
        }

        //internal void DenseDrawEXP1(Star root)
        //{
        //    if (root == null)
        //    {
        //        return;
        //    }

        //    // Magic Number for styling. May be changed to input parameter later.
        //    double starSize = 20;
        //    double minHeightDistance = 2 * starSize;

        //    if (root.Neighbors.Count < 3)
        //    {
        //        // Single Star Map
        //        if (root.Neighbors.Count == 0)
        //        {
        //            return;
        //        }

        //        if (root.Neighbors.Count == 1)
        //        {
        //            Star neighbor = root.Neighbors.First();

        //            // Debug check for condition that logically invalid
        //            if (neighbor.Neighbors.Count == 0)
        //            {
        //                throw new Exception("DenseDraw: Number of neighbor star of Star neighbor shouldn't be 0");
        //            }

        //            // Draw two stars map
        //            if (neighbor.Neighbors.Count == 1) 
        //            {
        //                neighbor.Coordinate = new System.Windows.Point(root.Coordinate.X, root.Coordinate.Y + minHeightDistance);
        //                return;
        //            }

        //            if (neighbor.Neighbors.Count == 2)
        //            {
        //                Star neighbor2 = neighbor.Neighbors.First();

        //                // Debug check for condition that logically invalid
        //                if (neighbor2.Neighbors.Count == 0)
        //                {
        //                    throw new Exception("DenseDraw: Number of neighbor star of Star neighbor2 shouldn't be 0");
        //                }

        //                // Draw three stars Map
        //                if (neighbor2.Neighbors.Count == 1)
        //                {
        //                    neighbor.Coordinate = new System.Windows.Point(root.Coordinate.X, root.Coordinate.Y + minHeightDistance);
        //                    neighbor2.Coordinate = new System.Windows.Point(root.Coordinate.X, root.Coordinate.Y + 2 * minHeightDistance);
        //                    return;
        //                }

        //                // Draw three neighbors map. Two neighbor position are placed with neighbor of the only neighbor of the root, and its another neighbor.
        //                // Only one branch of the map will potentially have numerous neighbors. Thus, it is the only branch to be drawm further.
        //            }
        //            else
        //            {
        //                // Draw three neighbors map. Two neighbor position are placed with two neighbors of the only neighbor of the root.
        //                // Have to be taken care of the beginning condition because while stars at every neighbor positions may have numerous neighbors, the depth of one star is technically 1 and that of other two is 2. 
        //                var iter = neighbor.Neighbors.GetEnumerator();

        //            }
        //        }
        //        else 
        //        {
        //            var iter = root.Neighbors.GetEnumerator();
        //            Star neighbor1 = iter.Current;
        //            if (!iter.MoveNext()) 
        //            {
        //                // Debug check for condition that logically invalid
        //                throw new Exception("DenseDraw: Number of neighbor star of Star root should be 2, but enumerator fails to reach the second neighbor");
        //            }
        //            Star neighbor2 = iter.Current;

        //            // Debug check for condition that logically invalid
        //            if (neighbor1.Neighbors.Count == 0)
        //            {
        //                throw new Exception("DenseDraw: Number of neighbor star of Star neighbor1 shouldn't be 0");
        //            }
        //            if (neighbor2.Neighbors.Count == 0)
        //            {
        //                throw new Exception("DenseDraw: Number of neighbor star of Star neighbor2 shouldn't be 0");
        //            }

        //            // Draw three star Map
        //            if (neighbor1.Neighbors.Count == 1 && neighbor2.Neighbors.Count == 1)
        //            {
        //                return;
        //            }

        //            if (neighbor1.Neighbors.Count == 1 || neighbor2.Neighbors.Count == 1)
        //            {
        //                // Draw three neighbors map. one neighbor position is placed with one neighbor of the only neighbor that has further connection to other stars.
        //                // Only one branch of the map will potentially have numerous neighbors. Thus, it is the only branch to be drawm further.
        //            }
        //            else
        //            {
        //                // Draw four neighbors map. Two neighbor position are each placed with one neighbor of one neighbor of the root.
        //                // Have to be taken care of the beginning condition because while stars at every neighbor positions may have numerous neighbors, the depth of two neighbors is technically 1 and that of other two is 2. 
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Standard Condition.
        //        List<(List<Star>, List<Star>)> branches = new(root.Neighbors.Count);
        //        //List<List<Star>> parentFloors = new List<List<Star>>(root.Neighbors.Count);
        //        //List<List<Star>> childFloors = new List<List<Star>>(root.Neighbors.Count);

        //        root.Visited = true;
        //        //HashSet<Star> visited = [root];
        //        double radian = 2 * Math.PI / root.Neighbors.Count;
        //        double minHeight = minHeightDistance;

        //        var iter = root.Neighbors.GetEnumerator();
        //        for (int i = 0; i < root.Neighbors.Count; i++)
        //        {
        //            branches[i].Item1.Add(iter.Current);
        //            iter.Current.Visited = true;
        //            iter.MoveNext();
        //        }

        //        // Preventing infinite loop or restrict the map size
        //        int maxRecursion = 100000;
        //        for (int floor = 0; floor < maxRecursion; floor++)
        //        {
        //            for (int branch = 0; branch < root.Neighbors.Count; branch++)
        //            {
        //                var pair = branches[branch];
        //                List<Star> parentFloor = pair.Item1;
        //                List<Star> childFloor = pair.Item2;

        //                if (parentFloor.Count == 0)
        //                {
        //                    continue;
        //                }

        //                foreach (Star parentStar in parentFloor)
        //                {
        //                    parentStar.Visited = true;
        //                    foreach (Star childStar in parentStar.Neighbors)
        //                    {
        //                        if (!childStar.Visited)
        //                        {
        //                            childFloor.Add(childStar);
        //                        }
        //                    }
        //                }

        //                double requiredHalfWidth = parentFloor.Count * starSize / 2;
        //                double requiredHeight = requiredHalfWidth / Math.Tan(radian / 2) + starSize / 2;
        //                double requiredInterval = starSize;
        //                if (requiredHeight < minHeight)
        //                {
        //                    requiredHeight = minHeight;
        //                    requiredHalfWidth = (requiredHeight - starSize / 2) * Math.Tan(radian / 2);
        //                    requiredInterval = requiredHalfWidth * 2 / parentFloor.Count;
        //                }

        //                double startPosition;
        //                if (parentFloor.Count % 2 == 0)
        //                {
        //                    startPosition = requiredInterval / 2 - parentFloor.Count / 2 * requiredInterval;
        //                }
        //                else
        //                {
        //                    startPosition = -(parentFloor.Count / 2 * requiredInterval);
        //                }

        //                System.Windows.Point[] coors = new System.Windows.Point[parentFloor.Count];
        //                for (int i = 0; i < parentFloor.Count; i++)
        //                {
        //                    coors[i] = new System.Windows.Point(startPosition + i * requiredInterval, requiredHeight);
        //                }

        //                // Rotation
        //                Matrix rotationMatrix = Matrix.Identity;
        //                rotationMatrix.Rotate(radian / Math.PI * 180 * branch);
        //                rotationMatrix.Transform(coors);

        //                // Add offset to center on root's coordination
        //                for (int i = 0; i < parentFloor.Count; i++)
        //                {
        //                    coors[i].X += root.Coordinate.X;
        //                    coors[i].Y += root.Coordinate.Y;
        //                }

        //                for (int i = 0; i < parentFloor.Count; i++)
        //                {
        //                    parentFloor[i].Coordinate = coors[i];
        //                }

        //                //double startPosition = 0;
        //                //SortedSet<System.Windows.Point> coors = new SortedSet<System.Windows.Point>(new PointComp());
        //                //if (parentFloor.Count % 2 == 0)
        //                //{
        //                //    startPosition = requiredInterval / 2;
        //                //}
        //                //else
        //                //{
        //                //    coors.Add(new System.Windows.Point(0, requiredHeight));
        //                //    startPosition = requiredInterval;
        //                //}

        //                //if (parentFloor.Count % 2 == 0)
        //                //{
        //                //    startPosition = requiredInterval / 2;
        //                //    for (int i = 0; i < parentFloor.Count/2; i++)
        //                //    {
        //                //        parentFloor[2 * i].Coordinate = new System.Windows.Point(startPosition + i * requiredInterval, requiredHeight);
        //                //        parentFloor[2 * i + 1].Coordinate = new System.Windows.Point(-startPosition - i * requiredInterval, requiredHeight);
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    startPosition = requiredInterval;
        //                //    parentFloor.Last().Coordinate = new System.Windows.Point(0, requiredHeight);
        //                //    for (int i = 0; i < parentFloor.Count/2; i++)
        //                //    {
        //                //        parentFloor[2 * i].Coordinate = new System.Windows.Point(startPosition + i * requiredInterval, requiredHeight);
        //                //        parentFloor[2 * i + 1].Coordinate = new System.Windows.Point(-startPosition - i * requiredInterval, requiredHeight);
        //                //    }
        //                //}

        //                pair.Item1 = pair.Item2;
        //                pair.Item2 = [];
        //                minHeight = requiredHeight + minHeightDistance;
        //            }

        //            bool stop = true;
        //            foreach (var pair in branches)
        //            {
        //                if (pair.Item1.Count > 0)
        //                {
        //                    stop = false;
        //                    break;
        //                }
        //            }
        //            if (stop)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //}

        //internal void newDraw(Star root, double radian)
        //{
        //    /* 
        //     思路
        //    对于root的每一个neighbor 执行以下操作
        //    1. 算出每个经过neighbor可以抵达的星球在图中的坐标。
        //        为了方便，在第三第四象限作图。以此使得每个点的Y坐标等于其离原点的高度。每个点的X坐标则简单的取其离原点的宽度。
        //        完成作图后，对每个点进行旋转。可以用RotateTransform的Matrix的transform方法来处理每个点。
        //    2. 绘制polyline链接每个点，并在每个点布置一个button以代表星球。
        //        为了体现航道的多样性，path有多种可能的颜色。有以下解决方案
        //        1. 全用line，使用每个line的stroke设定颜色
        //     */
        //    List<Star> rootFloor = [];
        //    List<Star> childFloor = [];
        //    HashSet<Star> visited = [];

        //    (double, double) startCoor = (500, 500);
        //    Star origin = new Star("Origin", "I am the origin");
            

        //    rootFloor.Add(root);

        //    List<List<(double, double)>> coors = DenseCoors(origin, root, Math.PI / 4, 20);

        //    for (int floor = 0; rootFloor.Count > 0; floor++) 
        //    {
        //        int iter = 0;
        //        for (int i = 0; i < rootFloor.Count; i++)
        //        {
        //            Star rs = rootFloor[i];
        //            visited.Add(rs);
        //            foreach (Star cs in rs.Neighbors) 
        //            {
        //                if (!visited.Contains(cs))
        //                {
        //                    childFloor.Add(cs);
        //                }
        //                else
        //                {
        //                    continue;
        //                }

        //                Line line = new Line();
        //                line.X1 = coors[floor][i].Item1 + startCoor.Item1;
        //                line.Y1 = coors[floor][i].Item2 + startCoor.Item2;
        //                line.X2 = coors[floor + 1][iter].Item1 + startCoor.Item1;
        //                line.Y2 = coors[floor + 1][iter].Item2 + startCoor.Item2;
        //                line.Stroke = Brushes.Red;
        //                line.StrokeThickness = 3;
        //                DisplayedMap.Children.Add(line);
        //                iter++;
        //            }

        //            Ellipse ellipse = new Ellipse();
        //            ellipse.Width = 20;
        //            ellipse.Height = 20;
        //            ellipse.Fill = Brushes.Blue;
        //            Canvas.SetLeft(ellipse, coors[floor][i].Item1 + startCoor.Item1 - 10);
        //            Canvas.SetTop(ellipse, coors[floor][i].Item2 + startCoor.Item2 - 10);
        //            DisplayedMap.Children.Add(ellipse);
        //        }

        //        rootFloor = childFloor;
        //        childFloor = [];
        //    }
            


        //    List<List<(double, double)>> DenseCoors(Star origin, Star root, double radian, double starSize)
        //    {
        //        List<List<(double, double)>> coors = [];
        //        List<Star> processingFloor = [root];
        //        List<Star> waitingFloor = [];
        //        HashSet<Star> visited = [origin];

        //        double minHeight = 2 * starSize;

        //        while (processingFloor.Count > 0)
        //        {
        //            foreach (Star s in processingFloor)
        //            {
        //                visited.Add(s);
        //                foreach (Star sn in s.Neighbors)
        //                {
        //                    if (!visited.Contains(sn))
        //                    {
        //                        waitingFloor.Add(sn);
        //                    }
        //                }
        //            }

        //            double requiredHalfWidth = processingFloor.Count * starSize / 2;
        //            double requiredHeight = requiredHalfWidth / Math.Tan(radian / 2) + starSize / 2;
        //            double requiredInterval = starSize;
        //            if (requiredHeight < minHeight)
        //            {
        //                requiredHeight = minHeight;
        //                requiredHalfWidth = (requiredHeight - starSize / 2)* Math.Tan(radian / 2);
        //                requiredInterval = requiredHalfWidth * 2 / processingFloor.Count;
        //            }

        //            List<(double, double)> coor = [];

        //            double startPosition = 0;

        //            if (processingFloor.Count % 2 == 0)
        //            {
        //                startPosition = requiredInterval / 2;
        //            }
        //            else
        //            {
        //                coor.Add((0, requiredHeight));
        //                startPosition = requiredInterval;
        //            }

        //            for (double x = startPosition; x <= requiredHalfWidth; x += requiredInterval)
        //            {
        //                coor.Add((x, requiredHeight));
        //                coor.Add((-x, requiredHeight));
        //            }

        //            coors.Add(coor);

        //            processingFloor = waitingFloor;
        //            waitingFloor = [];
        //            minHeight = requiredHeight + 2 * starSize;
        //        }

        //        return coors;
        //    }

        //}

        //internal void DrawMapInit(Star root)
        //{
        //    int tier = 0;
        //    double mapWidth = DisplayedMap.Width;
        //    double mapHeight = DisplayedMap.Height;
        //    double rootX = mapWidth / 2;
        //    double rootY = mapHeight / 2;
        //    double pathLength = Map.LineLength[tier];

        //    Button rootStar = new() { Width = Map.ButtonSize[0], Height = Map.ButtonSize[0], Style = TryFindResource("StarButton") as Style, DataContext = root};
        //    rootStar.Click += new RoutedEventHandler(StarButton_Click);
        //    rootStar.SetBinding(Button.ContentProperty, new Binding("Name"));
            

        //    Canvas.SetLeft(rootStar, rootX - rootStar.Width / 2);
        //    Canvas.SetTop(rootStar, rootY - rootStar.Height / 2);
        //    Canvas.SetZIndex(rootStar, 1);
        //    DisplayedMap.Children.Add(rootStar);

        //    Queue<(double, double)> neighborCoors = new([(rootX + pathLength, rootY),(rootX, rootY +pathLength),(rootX - pathLength, rootY),(rootX, rootY - pathLength),]);

        //    //List<(double, double)> neighborCoor = [
        //    //    (rootX + pathLength, rootY),
        //    //    (rootX, rootY + pathLength),
        //    //    (rootX - pathLength, rootY),
        //    //    (rootX, rootY - pathLength),
        //    //];

        //    int dir = 0;
        //    foreach (Star neighbor in root.Neighbors)
        //    {
        //        //Star neighbor = root.Neighbors[i];
        //        Button neighborStar = new() { Height = Map.ButtonSize[tier], Width = Map.ButtonSize[tier], Style = TryFindResource("StarButton") as Style, DataContext = neighbor };
        //        neighborStar.Click += new RoutedEventHandler(StarButton_Click);
        //        neighborStar.SetBinding(Button.ContentProperty, new Binding("Name"));

        //        (double, double) neighborCoor = neighborCoors.Dequeue();

        //        Canvas.SetLeft(neighborStar, neighborCoor.Item1 - neighborStar.Width / 2);
        //        Canvas.SetTop(neighborStar, neighborCoor.Item2 - neighborStar.Height / 2);
        //        Canvas.SetZIndex(neighborStar,1);
        //        DisplayedMap.Children.Add(neighborStar);

        //        Line pathLine = new() { X1 = rootX, Y1 = rootY, X2 = neighborCoor.Item1, Y2 = neighborCoor.Item2, Style = DisplayedMap.TryFindResource("FTLLine") as Style };
        //        Canvas.SetZIndex(pathLine,0);
        //        DisplayedMap.Children.Add(pathLine);

        //        DrawMapSub(root, neighbor, neighborCoor.Item1, neighborCoor.Item2, tier + 1, dir);
        //        dir++;
        //    }
        //}

        //internal void DrawMapSub(Star parent, Star root, double rootX, double rootY, int tier, int dir)
        //{
        //    if (tier > 2)
        //    {
        //        return;
        //    }

        //    double PathLength = Map.LineLength[tier];
        //    List<(double, double)> neighborCoorsList;

        //    switch (dir)
        //    {
        //        case 0:
        //            neighborCoorsList = [
        //                (rootX + PathLength, rootY + PathLength),
        //                (rootX + PathLength, rootY),
        //                (rootX + PathLength, rootY -  PathLength),
        //            ];
        //            break;
        //        case 1:
        //            neighborCoorsList = [
        //                (rootX + PathLength, rootY + PathLength),
        //                (rootX, rootY + PathLength),
        //                (rootX - PathLength, rootY + PathLength),
        //            ];
        //            break;
        //        case 2:
        //            neighborCoorsList = [
        //                (rootX - PathLength, rootY + PathLength),
        //                (rootX - PathLength, rootY),
        //                (rootX - PathLength, rootY - PathLength),
        //            ];
        //            break;
        //        case 3:
        //            neighborCoorsList = [
        //                (rootX + PathLength, rootY - PathLength),
        //                (rootX, rootY - PathLength),
        //                (rootX - PathLength, rootY - PathLength),
        //            ];
        //            break;
        //        default:
        //            throw new Exception("Invalid Direction");
        //    }

        //    Queue<(double, double)> neighborCoors = new(neighborCoorsList);

        //    foreach(Star neighbor in root.Neighbors)
        //    {
        //        //Star neighbor = root.Neighbors[i];
        //        if (neighbor == parent) continue;

        //        Button neighborStar = new() { Height = Map.ButtonSize[tier], Width = Map.ButtonSize[tier], Style = TryFindResource("StarButton") as Style, DataContext = neighbor };
        //        neighborStar.Click += new RoutedEventHandler(StarButton_Click);
        //        neighborStar.SetBinding(Button.ContentProperty, new Binding("Name"));

        //        (double, double) neighborCoor = neighborCoors.Dequeue();

        //        Canvas.SetLeft(neighborStar, neighborCoor.Item1 - neighborStar.Width / 2);
        //        Canvas.SetTop(neighborStar, neighborCoor.Item2 - neighborStar.Height / 2);
        //        Canvas.SetZIndex(neighborStar, 1);
        //        DisplayedMap.Children.Add(neighborStar);

        //        Line pathLine = new() { X1 = rootX, Y1 = rootY, X2 = neighborCoor.Item1, Y2 = neighborCoor.Item2, Style = DisplayedMap.TryFindResource("FTLLine") as Style };
        //        Canvas.SetZIndex(pathLine, 0);
        //        DisplayedMap.Children.Add(pathLine);

        //        DrawMapSub(root, neighbor, neighborCoor.Item1, neighborCoor.Item2, tier + 1, dir);
        //    }            
        //}

        //internal void RotateExp()
        //{
        //    //LineGeometry a = new LineGeometry();
        //    //a.StartPoint = new Point(100, 100);
        //    //a.EndPoint = new Point(200, 100);
        //    //LineGeometry b = new LineGeometry();
        //    //b.StartPoint = new Point(100, 100);
        //    //b.EndPoint = new Point(100, 200);
        //    //GeometryGroup c = new GeometryGroup();
        //    //c.Children.Add(a);
        //    //c.Children.Add(b);
        //    //c.Transform = new RotateTransform(90, 100 ,100);

        //    ////Path path = new Path();
        //    ////path.Stroke = Brushes.Red;
        //    ////path.StrokeThickness = 5;
        //    ////path.Data = c;

        //    ////path.Loaded += new RoutedEventHandler(Path_Loaded);

        //    ////DisplayedMap.Children.Add(path);
            
        //    //Button t = new Button();
        //    //t.Height = 100;
        //    //t.Width = 100;
        //    //var e = new EllipseGeometry();
        //    //e.RadiusX = 75;
        //    //e.RadiusY = 75;
        //    //e.Center = new Point(100, 100);
        //    //t.Clip = e;
        //    //Canvas.SetTop(t, 100);
        //    //Canvas.SetLeft(t, 100);
        //    //DisplayedMap.Children.Add(t);


        //}
        
        private void StarButton_Click(object sender, EventArgs e)
        {
            var win = new StarWindow
            {
                DataContext = (sender as Button).DataContext
            };
            win.ShowDialog();
        }

    }


}