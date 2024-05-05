using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProNatur_Biomarkt_GmbH
{
    public partial class ProductsScreen : Form
    {
        private SqlConnection databaseConnections = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Denni\Documents\Pro-Natur Biomarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        public ProductsScreen()
        {
            InitializeComponent();
            ShowProducts();
        }
        

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if (textBoxProductName.Text == "" || textBoxProductBrand.Text == "" || textBoxProductPrice.Text == "" || comboBoxProductCategory.Text == "")
            {
                MessageBox.Show("Bitte fülle alle Werte aus");
                return;
            }
            string productName = textBoxProductName.Text;
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            databaseConnections.Open();
            string query = string.Format("insert into Products values ('{0}','{1}','{2}','{3}')", productName, productBrand, productCategory, productPrice );
            SqlCommand sqlCommand = new SqlCommand( query, databaseConnections );
            sqlCommand.ExecuteNonQuery();
            databaseConnections.Close();

            ClearAllFields();
            ShowProducts();
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            ShowProducts();

        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {

            ShowProducts();

        }

        private void ShowProducts()
        {
            //Start Punkt
            databaseConnections.Open();
            string query = "Select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnections);

            var datSet = new DataSet();
            sqlDataAdapter.Fill(datSet);

            productsDGV.DataSource = datSet.Tables[0];

            productsDGV.Columns[0].Visible = false;
            databaseConnections.Close();
        }
        private void ClearAllFields()
        {
            textBoxProductBrand.Text = "";
            textBoxProductName.Text = "";
            textBoxProductPrice.Text = "";
            comboBoxProductCategory.Text = "";
            comboBoxProductCategory.SelectedItem = null;
        }
    }
}
