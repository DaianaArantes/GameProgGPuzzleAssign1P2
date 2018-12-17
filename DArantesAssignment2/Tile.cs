using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DArantesAssignment2.DesignForm;
using static DArantesAssignment2.PlayForm;

namespace DArantesAssignment2
{
    public class Tile : PictureBox
    {
        public int Row { set; get; }
        public int Colunm { set; get; }
        public ImageType ImageType { set; get; }
        public ImageColor ImageColor { set; get; }
    }
}
