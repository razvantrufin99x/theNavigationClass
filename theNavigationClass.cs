// theNavigationClass.cs



using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;

using System.Data.SqlClient;



namespace sqlServerConnectionTest

{

  





        public class theNavigationClass

        {





            public string databaseName = "";

            public string table = "";



            public List<string> databasesNames = new List<string>();

            public List<string> tablesNames = new List<string>();

            public List<string> fieldsNames = new List<string>();

            public List<record> recordsNames = new List<record>();



            //keep labels and textboxes

            public List<Label> labels = new List<Label>();

            public List<TextBox> textboxes = new List<TextBox>();



            //new navigations buttons first prev next last

            public List<Button> navigationButtons = new List<Button>();





            //count labels and textboxes generated

            public int countLabels = 0;

            public int countTextBoxes = 0;



            //starting points for labels

            public int leftLabelStartPoint = 13;

            public int topLabelStartPoint = 105;



            //starting points for textboxes

            public int leftTextBoxStartPoint = 241;

            public int topTextBoxStartPoint = 105;



            public Form f;



            public int currentRecordNumber = 0;

            public int lastRecordNumber = 0;

            public int currentNumberOfRecordsInTable = 0;



            //7 create for each field in table a textBox

            // we will use just 5 textBoxes for now

            public int numberOfFieldsInTable = 0;

            //this will keep the names of all the fields in tha table

            public List<string> namesOfFieldsInTable = new List<string>();



            //create a list with all records

            public List<record> inregistrariTabela = new List<record>();

            public List<string> tmpList;



            //connect 1

            SqlConnection co = new SqlConnection();



            //read datata and write from FunctiiDBF



            SqlCommand cmd2;

            SqlDataReader reader2;// = cmd2.ExecuteReader();

            string currentDBName;



            string currentTableName;



            int totalColumnInTheCurrentTable = 0;



            public string textBox1 = "";

            public string txtNumberOfTotalRecords = "";

            public string lblDBName = "";

            public string lblTabelName = "";

            public string lblcurrentTableName = "";

            public string textBox2 = "";

            public string listBox1 = "";

            public string Text = "";

            public string comboBox1 = "";



            public string theCurrentDatabaseName = "";

            public string theCurrentTableName = "";



            public string txtCurrentRecordNumber = "";

            public string txtListAllRecords = "";



            public int countNavigationButtons = 0;



            //contructors



            public theNavigationClass() { }

            public theNavigationClass(Form pf)

            {

                f = pf;

            }





            public theNavigationClass(Form pf, string databasename, string tablename, string runstring)

            {

                f = pf;

                theCurrentDatabaseName = databasename;

                theCurrentTableName = tablename;

                textBox1 = theCurrentDatabaseName;

                comboBox1 = theCurrentTableName;

                currentTableName = theCurrentTableName;

                if (runstring == "RUNALL")

                {

                    this.getCurrentDBName();

                    this.findAllTablesInDB();

                    this.findAllFieldsNamesInTable();

                    this.finndAllRecordsInTable();

                    this.addTextBoxesAndLabelsIntoNavigator();

                    this.selectTheCurrentTableNameFromCurrentDB();

                    this.addAllNavigationsButtons();

                }





            }



            public theNavigationClass(Form pf, string databasename, string tablename)

            {

                f = pf;

                theCurrentDatabaseName = databasename;

                theCurrentTableName = tablename;

                textBox1 = theCurrentDatabaseName;

                comboBox1 = theCurrentTableName;

                currentTableName = theCurrentTableName;

            }



            public void addNewLabel(string s)

            {

                labels.Add(new Label());

                countLabels++;

                f.Controls.Add(labels[countLabels - 1]);

                labels[countLabels - 1].Visible = true;

                labels[countLabels - 1].Left = leftLabelStartPoint;

                labels[countLabels - 1].Top = topLabelStartPoint + countLabels * 35;

                labels[countLabels - 1].Text = s;





            }





            public void addNewTextBox(string s)

            {

                textboxes.Add(new TextBox());

                countTextBoxes++;

                f.Controls.Add(textboxes[countTextBoxes - 1]);

                textboxes[countTextBoxes - 1].Visible = true;

                textboxes[countTextBoxes - 1].Left = leftTextBoxStartPoint;

                textboxes[countTextBoxes - 1].Top = topTextBoxStartPoint + countTextBoxes * 35;

                textboxes[countTextBoxes - 1].Text = s;





            }



            //new add navigation buttons

            public void addNewNavigationButtons(string s)

            {

                navigationButtons.Add(new Button());

                countNavigationButtons++;

                f.Controls.Add(navigationButtons[countNavigationButtons - 1]);

                navigationButtons[countNavigationButtons - 1].Visible = true;

                navigationButtons[countNavigationButtons - 1].Left = countNavigationButtons * 100;

                navigationButtons[countNavigationButtons - 1].Top = 500;

                navigationButtons[countNavigationButtons - 1].Text = s;

                if (s == "first" || s == "|<")

                {

                    navigationButtons[countNavigationButtons - 1].Click += new System.EventHandler(this.first_Click);

                }

                else if (s == "prev" || s == "<")

                {

                    navigationButtons[countNavigationButtons - 1].Click += new System.EventHandler(this.prev_Click);

                }

                else if (s == "next" || s == ">")

                {

                    navigationButtons[countNavigationButtons - 1].Click += new System.EventHandler(this.next_Click);

                }

                else if (s == "last" || s == ">|")

                {

                    navigationButtons[countNavigationButtons - 1].Click += new System.EventHandler(this.last_Click);

                }



            }



            public void addAllNavigationsButtons()

            {

                addNewNavigationButtons("first");

                addNewNavigationButtons("prev");

                addNewNavigationButtons("next");

                addNewNavigationButtons("last");



            }





            private void first_Click(object sender, EventArgs e)

            {

                this.gotoFirstRecord();

            }



            private void prev_Click(object sender, EventArgs e)

            {

                this.gotoPreviewsRecord();

            }



            private void next_Click(object sender, EventArgs e)

            {

                this.gotoNextRecord();

            }



            private void last_Click(object sender, EventArgs e)

            {

                this.gotoLastRecord();

            }



            //this will keep a record

            public class record

            {

                public record() { }

                public record(int n, ref List<string> x) { }

                public record(ref List<string> strinRec)

                {

                    this.row = strinRec;

                }

                public List<string> row = new List<string>();



            }





            public void Table2Record(ref List<string> x)

            {

                record tmprec = new record(ref x);



                inregistrariTabela.Add(tmprec);





            }









            public void findAllTheRecordsInTable()

            {



                SqlCommand cmd3;

                SqlDataReader reader3;

            co.Close();

                cmd3 = new SqlCommand("SELECT * FROM " + currentTableName, co);

            co.Open();

                reader3 = cmd3.ExecuteReader();

                tmpList = new List<string>();



                while (reader3.Read())

                {

                    tmpList = new List<string>(); //this is the new row who make it work just fine 

                    for (int i = 0; i < totalColumnInTheCurrentTable; i++)

                    {

                        tmpList.Add(reader3.GetValue(i).ToString());



                    }

                    Table2Record(ref tmpList);



                }

            }











            public void printCurrentRecord()

            {







            }





            public void printAllTheRecordFromInregistrariTable()

            {

                for (int i = 0; i < inregistrariTabela.Count; i++)

                {

                    for (int j = 0; j < totalColumnInTheCurrentTable; j++)

                    {

                        //txtListAllRecords.Text += inregistrariTabela[i].row[j].ToString() + " : ";

                        txtListAllRecords += inregistrariTabela[i].row[j].ToString() + " : ";



                    }

                }



            }





            public void updateCurrentRecordNumberTextBox()

            {

                //txtCurrentRecordNumber.Text = currentRecordNumber.ToString();

                txtCurrentRecordNumber = currentRecordNumber.ToString();

            }





            private void navigareCartiGenerator_Load(object sender, EventArgs e)

            {

                /*

                 * textBox2.Visible = false;

                currentDBName = this.textBox1.Text;

                currentTableName = "carti";

                // points 1 to 3

                //conect 2

                co.ConnectionString = "Data Source=" + currentDBName + ";";

                //open

                co.Open();

                //execute reader2

                cmd2 = new SqlCeCommand("SELECT * FROM " + currentTableName, co);

                reader2 = cmd2.ExecuteReader();

                reader2.Read();

                 * */

            }



            public void selectToSelectedServer()

            {



            }

            public void connectToSelectedDatabase()

            {



            }



            public int GetNumberOfRecords2()

            {

                int count = -1;

                try

                {

                    //co.Open();

                    co.Close();

                string strcong = "SELECT COUNT(*) FROM " + currentTableName;

                    SqlCommand countall = new SqlCommand(strcong, co);

                    co.Open();

                    count = (int)countall.ExecuteScalar();

                }

                finally

                {

                    if (co!= null){co.Close();}

                }

                return count;

            }



            public void gotoRecord(int x)

            {

                currentRecordNumber = x;



            }

            public void loadRecordIntoForm()

            {

                for (int i = 0; i < currentNumberOfRecordsInTable; i++)

                {

                    try

                    {

                        labels[i].Text = fieldsNames[i].ToString();

                        textboxes[i].Text = inregistrariTabela[currentRecordNumber].row[i].ToString();

                    }

                    catch { }

                }



            }



            public void rec()

            {

                //textBox1.Text += " \r\n" + GetNumberOfRecords2().ToString();

                textBox1 += " \r\n" + GetNumberOfRecords2().ToString();

                currentNumberOfRecordsInTable = GetNumberOfRecords2();

                //txtNumberOfTotalRecords.Text = currentNumberOfRecordsInTable.ToString();

                txtNumberOfTotalRecords = currentNumberOfRecordsInTable.ToString();



                currentRecordNumber = 0;

                updateCurrentRecordNumberTextBox();





                //lblDBName.Text += currentDBName;

                //lblTabelName.Text += currentTableName;



                lblDBName += currentDBName;

                lblTabelName += currentTableName;





                findAllTheRecordsInTable();

                printAllTheRecordFromInregistrariTable();

            }



            public void addRecordsLabelsAndTextBoxesOnForm(string l, string t)

            {

                addNewLabel(l);

                addNewTextBox(t);

            }





            public void gotoFirstRecord()

            {

                currentRecordNumber = 0;



                //goto record 0

                //reader2.Read();

                updateCurrentRecordNumberTextBox();

                gotoRecord(currentRecordNumber);

                loadRecordIntoForm();

            }



            public void gotoPreviewsRecord()

            {

                if (currentRecordNumber > 0)

                {

                    //goto record currentRecordNumer-1;

                    //reader2.Read();

                    printCurrentRecord();

                    currentRecordNumber--;

                    updateCurrentRecordNumberTextBox();

                    gotoRecord(currentRecordNumber);

                    loadRecordIntoForm();

                }



            }

            public void gotoNextRecord()

            {

                if (currentRecordNumber < currentNumberOfRecordsInTable - 1)

                {

                    //goto record currentRecordNumer+1;

                    //reader2.NextResult();

                    //reader2.Read();

                    printCurrentRecord();

                    currentRecordNumber++;

                    updateCurrentRecordNumberTextBox();

                    gotoRecord(currentRecordNumber);

                    loadRecordIntoForm();



                }

            }



            public void gotoLastRecord()

            {

                currentRecordNumber = currentNumberOfRecordsInTable - 1;

                //goto record last

                //reader2.Read();

                updateCurrentRecordNumberTextBox();

                gotoRecord(currentRecordNumber);

                loadRecordIntoForm();

            }



            public void findAllRecordsAndPrintAllRecordToList()

            {

                findAllTheRecordsInTable();

                printAllTheRecordFromInregistrariTable();

            }



            public void getCurrentDBName()

            {

                //get current db name

                //currentDBName = textBox1.Text;

                currentDBName = textBox1;

          



            //connect to current db

            // points 1 to 3

            //conect 2





            //co.ConnectionString = "Data Source=" + currentDBName + ";";

            co.ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + currentDBName + ";Integrated Security = True";

            //C:\\___\\srcmai2020B\\sqlServerConnectionTest\\sqlServerConnectionTest\\dbMDFTestDatabase.mdf





            //open

            co.Open();

            }



            public void findAllTablesInDB()

            {

                //get db schema



                //GET ALL COLUMNS NAMES FROM DATABASE

                //SELECT * FROM INFORMATION_SCHEMA.COLUMNS



                //GET ALL INDEXES OF DATABASE

                //SELECT * FROM INFORMATION_SCHEMA.INDEXES



                //GET ALL INDEXES AND COLUMNS OF THE DB

                //SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMNS_USAGE



                //get all datatypes of the db

                //SELECT * FROM INFORMATION_SCHEMA.PROVIDER_TYPES



                //GET AL TABLES FROM DB

                //SELECT * FROM INFORMATION_SCHEMA.TABLES



                //GET ALL CONSTRAINTS FRO MDB

                //SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS



                //GET ALL FOREIGN KEYS OF THE DB

                //SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS







                //list all tables names



                //SELECT * FROM INFORMATION_SCHEMA.TABLES where table_name LIKE '%INV%' 

                //sys.tables





                SqlCommand FINDALLTABLES = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES", co);





                SqlDataReader reader4;

                reader4 = FINDALLTABLES.ExecuteReader();











                while (reader4.Read())

                {

                    //comboBox1.Items.Add(reader4["TABLE_NAME"].ToString());

                    comboBox1 += (reader4["TABLE_NAME"].ToString());

                }





                //find all field names





                //list all records

            }











            public void findAllFieldsNamesInTable()

            {

            co.Close();

                SqlCommand FINDALLTABLESCOLUMNS = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS", co);





            co.Open();

                SqlDataReader reader5;

                reader5 = FINDALLTABLESCOLUMNS.ExecuteReader();



            







            while (reader5.Read())

                {

                    if (reader5["TABLE_NAME"].ToString() == currentTableName)

                    {

                        //this.listBox1.Items.Add(reader5["COLUMN_NAME"].ToString());

                        this.listBox1 += reader5["COLUMN_NAME"].ToString();

                        totalColumnInTheCurrentTable++;

                        fieldsNames.Add(reader5["COLUMN_NAME"].ToString());

                    }

                }

            }







            public void finndAllRecordsInTable()

            {

                rec();

                SqlCommand cmd6;

                SqlDataReader reader6;

                //Text = this.currentTableName + " ";

                Text = this.currentTableName + " ";

                //Text += "SELECT * FROM " + currentTableName;

                Text += "SELECT * FROM " + currentTableName;

            co.Close();



                cmd6 = new SqlCommand("SELECT * FROM " + currentTableName, co);



            co.Open();

                reader6 = cmd6.ExecuteReader();







                while (reader6.Read())

                {

                    for (int i = 0; i < totalColumnInTheCurrentTable; i++)

                    {

                        try

                        {

                            //textBox2.Text += reader6.GetValue(i).ToString() + "     ";

                            textBox2 += reader6.GetValue(i).ToString() + "     ";



                        }

                        catch { }

                    }

                    // textBox2.Text += "\r\n";

                    textBox2 += "\r\n";

                }

            }







            public void addTextBoxesAndLabelsIntoNavigator()

            {

                // button10.Enabled = false;

                // textBox2.Visible = false;

                for (int i = 0; i < totalColumnInTheCurrentTable; i++)

                {

                try

                {

                    addRecordsLabelsAndTextBoxesOnForm(fieldsNames[i].ToString(), inregistrariTabela[currentRecordNumber].row[i].ToString());

                }

                catch { }

                }

            }





            public void selectTheCurrentTableNameFromCurrentDB()

            {

                //currentTableName = comboBox1.Text;

                //lblcurrentTableName.Text = currentTableName;

                currentTableName = comboBox1;

                lblcurrentTableName = currentTableName;

            }

        }

    }

