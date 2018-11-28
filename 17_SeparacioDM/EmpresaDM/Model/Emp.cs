namespace EmpresaDM.Model
{
    public class Emp
    {
        private int num;
        private string nom;

        public int Num { get => num; set => num = value; }
        public string Nom { get => nom; set => nom = value; }

        public Emp(int num, string nom)
        {
            Num = num;
            Nom = nom;
        }

        public override bool Equals(object obj)
        {
            var emp = obj as Emp;
            return emp != null &&
                   Num == emp.Num;
        }
    }
}