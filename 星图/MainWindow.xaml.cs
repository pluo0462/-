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

            //DenseDraw(Test());
        }

        //internal Star Test()
        //{
        //    Star A = new Star("A", "I am A", Map.StarType.Pulsar);
        //    Star B = new Star("B", "I am B", Map.StarType.BlackHole);
        //    Star C = new Star("C", "I am C", Map.StarType.BlackHole);
        //    Star D = new Star("D", "I am D", Map.StarType.BlackHole);
        //    Star E = new Star("E", "I am E", Map.StarType.ClassA);
        //    Star F = new Star("F", "I am F");
        //    Star G = new Star("G", "I am G");
        //    Star H = new Star("H", "I am H");
        //    Star K = new Star("K", "I am K");
        //    Star I = new Star("I", "I am I");
        //    Star J = new Star("J", "I am J");
        //    Star L = new Star("L", "I am L");
        //    Star M = new Star("M", "I am M");
        //    Star N = new Star("N", "I am N");
        //    Star O = new Star("O", "I am O");
        //    Star P = new Star("P", "I am P");


        //    Star.ConnectStar(A, B);
        //    Star.ConnectStar(A, C);
        //    Star.ConnectStar(C, D);
        //    Star.ConnectStar(C, E);
        //    Star.ConnectStar(C, N);
        //    Star.ConnectStar(C, O);
        //    Star.ConnectStar(C, P);
        //    Star.ConnectStar(A, F);
        //    Star.ConnectStar(E, G);
        //    Star.ConnectStar(D, H);
        //    Star.ConnectStar(D, K);
        //    Star.ConnectStar(B, I);
        //    Star.ConnectStar(A, J);
        //    Star.ConnectStar(A, L);
        //    Star.ConnectStar(A, M);

        //    I.Coordinate = new System.Windows.Point(1000, 500);

        //    Map.WriteMap();

        //    return I;
        //}

        //internal void DenseDraw(Star root)
        //{
        //    if (root == null)
        //    {
        //        return;
        //    }

        //    // Magic Number for styling. May be changed to input parameter later.
        //    double starSize = 60;
        //    double minHeightDistance = 2 * starSize;

        //    if (root.Neighbors.Count == 0)
        //    {
        //    }
        //    else if (root.Neighbors.Count == 1)
        //    {
        //        List<double> branchPosition = [0];
        //        double branchRadian = Math.PI * 2 / 3;

        //        CoorsBody(root, branchPosition, branchRadian, minHeightDistance);

        //        DrawBody(root, starSize);

        //        //DrawBranchLine(root, branchPosition, branchRadian);
        //    }
        //    else if (root.Neighbors.Count == 2)
        //    {
        //        List<double> branchPosition = [90, 270];
        //        double branchRadian = Math.PI * 2 / 3;

        //        CoorsBody(root, branchPosition, branchRadian, minHeightDistance);

        //        DrawBody(root, starSize);

        //        //DrawBranchLine(root, branchPosition, branchRadian);
        //    }
        //    else
        //    {
        //        // Standard Condition.
        //        List<double> branchPosition = new List<double>(root.Neighbors.Count);
        //        double branchRadian = Math.PI * 2 / root.Neighbors.Count;
        //        double branchDegree = 360.0 / root.Neighbors.Count;

        //        for (int i = 0; i < root.Neighbors.Count; i++)
        //        {
        //            branchPosition.Add(i * branchDegree);
        //        }

        //        CoorsBody(root, branchPosition, branchRadian, minHeightDistance);

        //        DrawBody(root, starSize);

        //        //DrawBranchLine(root, branchPosition, branchRadian);
        //    }

            
            
        //    void CoorsBody(Star root, List<double> branchPosition, double branchRadian, double minHeightDistance)
        //    {
        //        if (root.Neighbors.Count != branchPosition.Count)
        //        {
        //            throw new Exception("CoorsBody: the number of neighbors should equal to the number of branch position");
        //        }

        //        Map.Clean();

        //        List<List<Star>> parentFloors = new List<List<Star>>(root.Neighbors.Count);
        //        for (int i = 0; i < root.Neighbors.Count; i++)
        //        {
        //            parentFloors.Add(new List<Star>());
        //        }

        //        double[] minHeight = new double[branchPosition.Count];
        //        for (int i = 0; i < branchPosition.Count; i++)
        //        {
        //            minHeight[i] = minHeightDistance;
        //        }

        //        root.Visited = true;
        //        var iter = root.Neighbors.Keys.GetEnumerator();
        //        for (int i = 0; i < root.Neighbors.Count; i++)
        //        {
        //            iter.MoveNext();
        //            parentFloors[i].Add(iter.Current);
        //            iter.Current.Visited = true;
        //        }

        //        // Preventing infinite loop or restrict the map size
        //        int maxRecursion = 100000;
        //        for (int floor = 0; floor < maxRecursion; floor++)
        //        {
        //            for (int branch = 0; branch < root.Neighbors.Count; branch++)
        //            {
        //                List<Star> parentFloor = parentFloors[branch];
        //                List<Star> childFloor = [];

        //                if (parentFloor.Count == 0)
        //                {
        //                    continue;
        //                }

        //                foreach (Star parentStar in parentFloor)
        //                {
        //                    parentStar.Visited = true;
        //                    foreach (Star childStar in parentStar.Neighbors.Keys)
        //                    {
        //                        if (!childStar.Visited)
        //                        {
        //                            childFloor.Add(childStar);
        //                        }
        //                    }
        //                }

        //                double requiredHalfWidth = parentFloor.Count * starSize / 2;
        //                double requiredHeight = requiredHalfWidth / Math.Tan(branchRadian / 2) + starSize / 2;
        //                double requiredInterval = starSize;
        //                if (requiredHeight < minHeight[branch])
        //                {
        //                    requiredHeight = minHeight[branch];
        //                    requiredHalfWidth = (requiredHeight - starSize / 2) * Math.Tan(branchRadian / 2);
        //                    requiredInterval = requiredHalfWidth * 2 / parentFloor.Count;

        //                    //MessageBox.Show($"requiredHeight: {requiredHeight}\n requiredHalfWidth: {requiredHalfWidth}");
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
        //                rotationMatrix.Rotate(branchPosition[branch]);
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
        //                    //MessageBox.Show($"{parentFloor[i].Name} has coordination of ({parentFloor[i].Coordinate.X}, {parentFloor[i].Coordinate.Y})");
        //                }

        //                parentFloors[branch] = childFloor;
        //                minHeight[branch] = requiredHeight + minHeightDistance;     
        //            }

        //            bool stop = true;
        //            foreach (var parentFloor in parentFloors)
        //            {
        //                if (parentFloor.Count > 0)
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

        //    void DrawBody(Star root, double starSize)
        //    {
        //        Map.Clean();

        //        List<Star> parentFloor = [root];
        //        while (parentFloor.Count > 0)
        //        {
        //            List<Star> childFloor = [];
        //            foreach(Star parent in parentFloor)
        //            {
        //                parent.Visited = true;
        //                foreach (var pair in parent.Neighbors)
        //                {
        //                    Star child = pair.Key;
        //                    Lane path = pair.Value;
        //                    if (!child.Visited)
        //                    {
        //                        childFloor.Add(child);

        //                        Line line = new Line();
        //                        line.X1 = parent.Coordinate.X;
        //                        line.X2 = child.Coordinate.X;
        //                        line.Y1 = parent.Coordinate.Y;
        //                        line.Y2 = child.Coordinate.Y;

        //                        line.Style = DisplayedMap.TryFindResource($"{path.Type}") as Style;
        //                        DisplayedMap.Children.Add(line);
        //                    }
        //                }

        //                Button starButton = new Button();

        //                starButton.Style = DisplayedMap.TryFindResource($"StarButton") as Style;
        //                Image starImage = new();
        //                BitmapImage bitmapImage = new BitmapImage();
        //                bitmapImage.BeginInit();
        //                bitmapImage.UriSource = new Uri(@$"/src/{parent.StarType}.png", UriKind.Relative);
        //                bitmapImage.EndInit();
        //                starImage.Source = bitmapImage;
        //                starButton.Content = starImage;
        //                starButton.DataContext = parent;

        //                Canvas.SetLeft(starButton, parent.Coordinate.X - starButton.Width / 2);
        //                Canvas.SetTop(starButton, parent.Coordinate.Y - starButton.Height / 2);
        //                DisplayedMap.Children.Add(starButton);
        //            }
        //            parentFloor = childFloor;
        //        }
        //    }

        //    void DrawBranchLine(Star root, List<double> branchPosition, double branchRadian)
        //    {
        //        for (int i = 0; i < branchPosition.Count; i++)
        //        {
        //            Line line = new Line();
        //            line.X1 = root.Coordinate.X;
        //            line.X2 = root.Coordinate.X;
        //            line.Y1 = root.Coordinate.Y;
        //            line.Y1 = root.Coordinate.Y + 1000;
        //            line.Stroke = Brushes.White;
        //            line.StrokeThickness = 1;
        //            line.RenderTransform = new RotateTransform(branchPosition[i] + branchRadian / Math.PI * 180 / 2, root.Coordinate.X, root.Coordinate.Y);
        //            DisplayedMap.Children.Add(line);

        //            Line line2 = new Line();
        //            line2.X1 = root.Coordinate.X;
        //            line2.X2 = root.Coordinate.X;
        //            line2.Y1 = root.Coordinate.Y;
        //            line2.Y1 = root.Coordinate.Y + 1000;
        //            line2.Stroke = Brushes.White;
        //            line2.StrokeThickness = 1;
        //            line2.RenderTransform = new RotateTransform(branchPosition[i] - branchRadian / Math.PI * 180 / 2, root.Coordinate.X, root.Coordinate.Y);          
        //            DisplayedMap.Children.Add(line2);
        //        }
        //    }
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