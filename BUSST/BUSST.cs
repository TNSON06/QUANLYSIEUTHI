using DTO;
using DAOST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSST
{
    public class BUS_ST
    {
        private static BUS_ST instance;

        public static BUS_ST Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ST();
                return instance;
            }

        }
        private BUS_ST() { }
        public List<SP> GetListSP(string tenSP)
        {
            List<SP> listSV = new List<SP>();
            if (tenSP == null)
            {
               
                listSV = DAO_ST.Instance.GetAllSP();
            }
            if (tenSP != null)
            {
                listSV = DAO_ST.Instance.GetSPSearch(tenSP);
            }
            return listSV;
        }
        //cho MH
        public List<CBBItem> GetListCBBMH()
        {
            List<CBBItem> listCBB = new List<CBBItem>();
            foreach (MH mh in DAO_ST.Instance.GetAllMH())
            {
                listCBB.Add(new CBBItem
                {
                    Text = mh.TenMH,
                    value = mh.MaMH
                }) ;
            }
            return listCBB;
        }
        //cho NSX
        public List<CBBItem> GetListCBBNSX()
        {
            List<CBBItem> listCBBNSX = new List<CBBItem>();
            foreach (SP sp in DAO_ST.Instance.GetAllSP())
            {
                listCBBNSX.Add(new CBBItem
                {
                    Text = sp.NSX,
                    value = sp.MaSP
                });
            }
            return listCBBNSX;
        }
        //cho Sort
        public List<string> GetListCBBSort()
        {
            List<string> listSort = new List<string>();
            foreach (string cln in DAO_ST.Instance.GetNameColumn())
            {
                listSort.Add(cln);
            }
            return listSort;

        }
        public void ExcuteDB(SP s)
        {
            if (BUS_ST.Instance.GetSVByMSP(s.MaSP.ToString()) != null && s.MaSP == BUS_ST.Instance.GetSVByMSP(s.MaSP.ToString()).MaSP)
            {
                Console.WriteLine("da vao day");
                DAO_ST.Instance.UpdateSP(s);
            }
            else
            {
                DAO_ST.Instance.AddSV(s);

            }
        }
        public SP GetSVByMSP(string m)
        {
            SP s = null;
            foreach (SP sp in DAO_ST.Instance.GetAllSP())
            {
                if (sp.MaSP.ToString() == m)
                {
                    s = sp; ;
                    break;
                }

            }
            return s;
        }
        public void DelSV(List<string> LMSP)
        {
            foreach (string msp in LMSP)
            {
                DAO_ST.Instance.DeleteSV(msp);
                Console.WriteLine(msp);
            }
        }
    }
}
