using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SP
    {
        private int _MaSP;
        private string _TenSP;
        private DateTime? _NNH;
        private string _NSX;
        private bool? _TinhTrang;
        private int _MaMH;

        public int MaSP { get => _MaSP; set => _MaSP = value; }
        public string TenSP { get => _TenSP; set => _TenSP = value; }
        public DateTime? NNH { get => _NNH; set => _NNH = value; }
        public string NSX { get => _NSX; set => _NSX = value; }
        public bool? TinhTrang { get => _TinhTrang; set => _TinhTrang = value; }
        public int MaMH { get => _MaMH; set => _MaMH = value; }
        public SP(int MaSP,string tenSP,DateTime? NNH,string NSX,bool? TinhTrang,int MaMH) {
            this.MaSP = MaSP;
            this.TenSP = tenSP;
            this.NNH = NNH;
            this.NSX = NSX;
            this.TinhTrang= TinhTrang;
            this.MaMH = MaMH;
        
        }
    }
}
