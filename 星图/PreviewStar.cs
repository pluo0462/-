using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static 星图.Map;

namespace 星图
{
    internal class PreviewStar : ObservableCollection<Star>
    {
        public PreviewStar()
        {
            Star A = new Star("No.0 太阳系", "太阳系", StarType.ClassG);
            Star B = new Star("No.1 好望星", "好望星", StarType.ClassA);
            Star C = new Star("No.2 虚无", "虚无", StarType.ClassM);
            Star D = new Star("No.3 光辉之路", "光辉之路", StarType.ClassMRG);
            Star E = new Star("No.4 桑德兰", "桑德兰", StarType.ClassG);

            Add(A);
            Add(B);
            Add(C);
            Add(D);
            Add(E);
        }
    }
}
