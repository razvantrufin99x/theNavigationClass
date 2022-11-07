// Form1.cs



namespace sqlServerConnectionTest

{

    public partial class Form1 : Form

    {

        public Form1()

        {

            InitializeComponent();

        }

        

       

        public theNavigationClass nc;



        private void Form1_Load(object sender, EventArgs e)

        {



           



        }



        private void button1_Click(object sender, EventArgs e)

        {

            nc = new theNavigationClass(this, "C:\\___\\srcmai2020B\\sqlServerConnectionTest\\sqlServerConnectionTest\\dbMDFTestDatabase.mdf", "clienti", "RUNALL");

        }

    }

}





