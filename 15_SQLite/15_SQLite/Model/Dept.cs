using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_SQLite.Model
{
    class Dept
    {
        private int deptNo;
        private string dnom;
        private string loc;

        public Dept(int deptNo, string dnom, string loc)
        {
            this.deptNo = deptNo;
            this.dnom = dnom;
            this.loc = loc;
        }

        public int DeptNo { get => deptNo; set => deptNo = value; }
        public string Nom { get => dnom; set => dnom = value; }
        public string Loc { get => loc; set => loc = value; }
    }
}
