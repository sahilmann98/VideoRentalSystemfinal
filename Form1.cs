using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace VideoRentalSystem
{
    public partial class Form1 : Form
    {
        /*
         * SQL Connection constructor to start new connection 
         */
        SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\OEM\\Desktop\\VideoRentalSystem\\database.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();

            showDbContent("Customer");
            showDbContent("Movie");
            showDbContent("Rental");
        }

        /*
         * Function to load 
         * database contents from 
         * a table
         * Argument: Table Name
         */
        private void showDbContent(string tableName)
        {
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Format("SELECT * FROM {0}", tableName), sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            switch (tableName)
            {
                case "Customer":
                    customerDataGridView.DataSource = dataTable;
                    break;

                case "Movie":
                    movieGridView.DataSource = dataTable;
                    break;

                case "Rental":
                    rentalGridView.DataSource = dataTable;
                    break;
            }

            sqlConnection.Close();
        }

        /*
         * Add a new customer
         */
        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "INSERT INTO Customer VALUES({0},'{1}','{2}','{3}','{4}');";

            try
            {
                int id = Int32.Parse(cID.Text);
                string fname = cfname.Text;
                string lname = clname.Text;
                string address = caddress.Text;
                string phone = cphone.Text;
                SqlCommand sqlCommand = new SqlCommand(string.Format(query, id, fname, lname, address, phone), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! New customer added to Database");
            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + e.ToString());
            }
            sqlConnection.Close();
            showDbContent("Customer");
        }

        /*
         * Delete an existing
         * customer from Database
         */
        private void BtnDeleteCustomer_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "DELETE FROM Customer WHERE customerID={0};";

            try
            {
                int id = Int32.Parse(cID.Text);
                SqlCommand sqlCommand = new SqlCommand(string.Format(query, id), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! Customer has been removed from the database");
            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + err.ToString());
            }
            sqlConnection.Close();
            showDbContent("Customer");
        }

        /*
         * Insert a new movie to
         * the database
         */
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "INSERT INTO Movie VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}');";

            try
            {

                //Get input from text fields
                int id = Int32.Parse(mID.Text);
                string rating = mrating.Text;
                string title = mtitle.Text;
                string year = myear.Text;
                string cost = mcost.Text;
                string copies = mcopies.Text;
                string genre = mgenre.Text;
                SqlCommand sqlCommand = new SqlCommand(string.Format(query, id, rating, title, year, cost, copies, genre), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! New Movie added to Database");

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + e.ToString());
            }

            sqlConnection.Close();
            showDbContent("Movie");
        }

        /*
         * Delete a movie
         * from database
         */
        private void BtnDeleteMovie_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "DELETE FROM Movie WHERE movieID={0};";

            try
            {
                int id = Int32.Parse(mID.Text);
                SqlCommand sqlCommand = new SqlCommand(string.Format(query, id), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! Movie has been removed from the database");
            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + err.ToString());
            }
            sqlConnection.Close();
            showDbContent("Movie");
        }

        private void BtnUpdateCustomer_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "UPDATE Customer SET firstName='{0}', lastName='{1}', Address='{2}', Phone='{3}' WHERE customerID={4};";

            try
            {
           
                int id = Int32.Parse(cID.Text);
                string fname = cfname.Text;
                string lname = clname.Text;
                string address = caddress.Text;
                string phone = cphone.Text;
                SqlCommand sqlCommand = new SqlCommand(string.Format(query, fname, lname, address, phone, id), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! Customer details updated.");

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + err.ToString());
            }
            //Close connection
            sqlConnection.Close();
            showDbContent("Customer");
        }

        /*
         * Update an existing
         * movie in the database
         */
        private void BtnUpdateMovie_Click(object sender, EventArgs e)
        {

            // Open a new connection
            sqlConnection.Open();
            string query = "UPDATE Movie SET Rating='{0}', Title='{1}', Year='{2}', Cost='{3}', Copies='{4}', Genre='{5}' WHERE movieID={6};";

            try
            {
                int id = Int32.Parse(mID.Text);
                string rating = mrating.Text;
                string title = mtitle.Text;
                string year = myear.Text;
                string cost = mcost.Text;
                string copies = mcopies.Text;
                string genre = mgenre.Text;
                SqlCommand sqlCommand = new SqlCommand(string.Format(query, rating, title, year, cost, copies, genre, id), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! Movie details updated.");

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + err.ToString());
            }

            sqlConnection.Close();
            showDbContent("Movie");
        }

        /*
         * Initialize a rent
         * using Foreign Keys
         */
        private void BtnRent_Click(object sender, EventArgs e)
        {

            // Open a new connection
            sqlConnection.Open();


            try
            {
                //Inserting details of the rental
                string query = "INSERT INTO Rental VALUES({0},{1},{2},'{3}',{4});";

                int id = Int32.Parse(rentID.Text);
                int mid = Int32.Parse(rentmID.Text);
                int cid = Int32.Parse(rentcID.Text);

                DateTime dateTime = rentDate.Value;
                String date = dateTime.ToString("yyyy-MM-dd");

                query = string.Format(query, id, mid, cid, date, "NULL");
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + e.ToString());
            }

            sqlConnection.Close();
            showDbContent("Rental");
        }

        /*
         * Return a rented movie
         */
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "UPDATE Rental SET dateReturned='{0}' WHERE rentID={1};";

            try
            {
                int id = Int32.Parse(returnID.Text);
                DateTime dateTime = rentDate.Value;
                String date = dateTime.ToString("yyyy-MM-dd");

                SqlCommand sqlCommand = new SqlCommand(string.Format(query, date, id), sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                MessageBox.Show("Success! Movie has been returned");

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + e.ToString());
            }

            sqlConnection.Close();
            showDbContent("Rental");
        }

        /*
         * Returns the ID of customer
         * with most rented movies
         */
        private void BtnMostCustomer_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "SELECT customerIDFK, COUNT(customerIDFK) AS value_occurence FROM Rental GROUP BY customerIDFK ORDER BY value_occurence DESC;";

            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                MessageBox.Show("The customer who has rented most movies has customerID = "+dataTable.Rows[0][0].ToString());

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + err.ToString());
            }

            sqlConnection.Close();
        }

        /*
         * returns id of 
         * the most popular
         * (rented) movie
         */
        private void BtnMostMovie_Click(object sender, EventArgs e)
        {
            // Open a new connection
            sqlConnection.Open();
            string query = "SELECT movieIDFK, COUNT(movieIDFK) AS value_occurence FROM Rental GROUP BY movieIDFK ORDER BY value_occurence DESC;";

            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                MessageBox.Show("The most rented movie has movieID = " + dataTable.Rows[0][0].ToString());

            }
            catch (Exception err)
            {
                MessageBox.Show("Sorry! Please try again.\nError: " + err.ToString());
            }

            sqlConnection.Close();
        }
    }
}
