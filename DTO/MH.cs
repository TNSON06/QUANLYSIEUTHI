using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MH
    {
        int _MaMH;
        string _TenMH;

        public int MaMH { get => _MaMH; set => _MaMH = value; }
        public string TenMH { get => _TenMH; set => _TenMH = value; }
        public MH(int MaMH,string TenMH) { 
            this.MaMH = MaMH;
            this.TenMH = TenMH;
        }
    }
}
