using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseStock
{
    public partial class Data_base_stock : Form
    {
        public Data_base_stock()
        {
            InitializeComponent();
        }

        // the mysql databaseConnection
        MySqlConnection databaseConnection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='db_stock';username=root;password=");
        
        // form load
        private void Data_base_stock_Load(object sender, EventArgs e)
        {
            // call PopulateDataGridView function
            PopulateDataGridView("");
        }

        // function to populate the datagridview with products data
        public void PopulateDataGridView(string valueToSearch)
        {

            MySqlCommand command = new MySqlCommand("SELECT * FROM stockproducts WHERE CONCAT(ID, Name, Description, Price) LIKE '%" + valueToSearch + "%'", databaseConnection);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);

            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            dataGridView1.RowTemplate.Height = 150;

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.DataSource = dataTable;

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridView1.Columns[4];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns[4].Width = 250;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }


        // button browse image
        private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                productPictureBox.Image = Image.FromFile(opf.FileName);
            }

        }

        // datagridview click event
        // display the selected row into textboxes and the image into picturebox
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[4].Value;

            MemoryStream ms = new MemoryStream(img);

            productPictureBox.Image = Image.FromStream(ms);

            idTextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            descriptionTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            priceTextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }


        // button insert
        private void AddButton_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            productPictureBox.Image.Save(ms, productPictureBox.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand sqlCommand = new MySqlCommand("INSERT INTO stockproducts(ID, Name, Description, Price, Image) VALUES (@id,@name,@desc, @Price, @img)", databaseConnection);

            sqlCommand.Parameters.Add("@id", MySqlDbType.VarChar).Value = idTextBox.Text;
            sqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameTextBox.Text;
            sqlCommand.Parameters.Add("@desc", MySqlDbType.VarChar).Value = descriptionTextBox.Text;
            sqlCommand.Parameters.Add("@price", MySqlDbType.VarChar).Value = priceTextBox.Text;
            sqlCommand.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
            

            ExecuteQuery(sqlCommand, "Product added successfully");
        }


        // function to execute insert update delete
        public void ExecuteQuery(MySqlCommand mcomd, string myMsg)
        {
            databaseConnection.Open();
            if (mcomd.ExecuteNonQuery() == 1)
            {

                MessageBox.Show(myMsg);

            }
            else
            {

                MessageBox.Show("Unable to execute the request");

            }

            databaseConnection.Close();

            PopulateDataGridView("");
        }


        // button update
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            productPictureBox.Image.Save(ms, productPictureBox.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand command = new MySqlCommand("UPDATE stockproducts SET Name=@name, Description=@desc, Price=@Price, Image=@img WHERE ID = @id", databaseConnection);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = idTextBox.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameTextBox.Text;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = descriptionTextBox.Text;
            command.Parameters.Add("@Price", MySqlDbType.VarChar).Value = priceTextBox.Text;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
            

            ExecuteQuery(command, "item Updated");
        }


        // button delete
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM stockproducts WHERE ID = @id", databaseConnection);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = idTextBox.Text;

            ExecuteQuery(command, "Data Deleted");

            ResetInputFields();
        }

        // search value and display the returned result into datagridview
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            PopulateDataGridView(searchTextBox.Text);
        }

        // button search item by id
        private void SearchButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM stockproducts WHERE ID = @id", databaseConnection);
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = idTextBox.Text;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("No matching records found");
                ResetInputFields();
            }
            else
            {

                idTextBox.Text = table.Rows[0][0].ToString();
                nameTextBox.Text = table.Rows[0][1].ToString();
                descriptionTextBox.Text = table.Rows[0][2].ToString();
                priceTextBox.Text = table.Rows[0][3].ToString();

                byte[] img = (byte[])table.Rows[0][4];
                MemoryStream ms = new MemoryStream(img);
                productPictureBox.Image = Image.FromStream(ms);

            }
        }

        // button new
        private void Empty_The_fields_Button_Click(object sender, EventArgs e)
        {
            ResetInputFields();
        }

        // clear all fields
        public void ResetInputFields()
        {
            idTextBox.Text = "";
            nameTextBox.Text = "";
            descriptionTextBox.Text = "";
            priceTextBox.Text = "";

            productPictureBox.Image = null;

        }


        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Want to search for something?")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black; // Set text color to black, or choose another color
            }
        }

        private void searchTextBox_One(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Want to search for something?";
                tb.ForeColor = Color.Gray; // Set the placeholder text color to gray, or another light color
            }
        }

        private void Data_base_stock_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxDesc_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
