using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAOST
{
    public class DAO_ST
    {

        private static DAO_ST instance;

        public static DAO_ST Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_ST();
                return instance;
            }
            private set => instance = value;
        }
        private DAO_ST() { }
        
        public List<SP> GetAllSP()
        {
            List<SP> listSV = new List<SP>();
            string query = "select * from SanPham";
            Console.WriteLine(DataProvider.Instance.GetRecords(query).ToString());
            foreach (DataRow row in DataProvider.Instance.GetRecords(query).Rows)
            {
                listSV.Add(GetSP(row));
            }
            return listSV;
        }
        public List<SP> GetSPSearch(string TenSP)
        {
            List<SP> listSV = new List<SP>();
            string query = "select * from SanPham where TenSP ='" + TenSP + "'";
            foreach (DataRow row in DataProvider.Instance.GetRecords(query).Rows)
            {
                listSV.Add(GetSP(row));
            }
            return listSV;
        }
        public SP GetSP(DataRow i)
        {

            int maSP = i["MaSP"] != DBNull.Value ? (int)i["MaSP"] : 0;
            string TenSP = i["TenSP"] != DBNull.Value ? i["TenSP"].ToString() : string.Empty;
            DateTime? NNH = i["NNH"] != DBNull.Value ? (DateTime?)i["NNH"] : null;
            string NSX = i["NSX"] != DBNull.Value ? i["NSX"].ToString() : string.Empty;
            bool? TinhTrang = i["TinhTrang"] != DBNull.Value ? (bool?)i["TinhTrang"] : null;
            int MaMH = i["MaMH"] != DBNull.Value ? (int)i["MaMH"] : 0;

            return new SP(maSP, TenSP, NNH, NSX, TinhTrang, MaMH);
        }
        public List<MH> GetAllMH()
        {
            List<MH> listMH = new List<MH>();
            string query = "select * from MatHang";
            foreach (DataRow row in DataProvider.Instance.GetRecords(query).Rows)
            {
                listMH.Add(GetMH(row));
            }
            return listMH;
        }
        public MH GetMH(DataRow i)
        {
            return new MH
            (
                 (int)i["MaMH"],
                 i["TenMH"].ToString()
            );
        }
        public List<string> GetNameColumn()
        {
            List<string> listCL = new List<string>();
            string query = "select top 0 * from SanPham";
            foreach (DataColumn col in DataProvider.Instance.GetRecords(query).Columns)
            {
                string columnName = col.ColumnName;
                listCL.Add(columnName);
            }
            return listCL;
        }
        public void UpdateSP(SP s)
        {
            string query = "UPDATE SanPham SET TenSP='" + s.TenSP + "', NNH='" + s.NNH + "', NSX='" + s.NSX + "', TinhTrang='" + s.TinhTrang + "', MaMH=" + s.MaMH +  " WHERE MaSP=" + s.MaSP;
            DataProvider.Instance.ExecuteDB(query);
            Console.WriteLine("Ham nay da duoc goi");
            Console.WriteLine(s.TenSP);
        }
        public void DeleteSV(string msp)
        {
            string query = "DELETE FROM SanPham WHERE MaSP = " + msp;
            DataProvider.Instance.ExecuteDB(query);

        }
        public void AddSV(SP s)
        {
            string query = "INSERT INTO SanPham(MaSP,TenSP,NNH,NSX,TinhTrang,MaMH) VALUES(" + s.MaSP + ",'" + s.TenSP + "','" + s.NNH + "','" + s.NSX + "','" + s.TinhTrang + "'," + s.MaMH + ")";
            DataProvider.Instance.ExecuteDB(query);

        }
    }
}
