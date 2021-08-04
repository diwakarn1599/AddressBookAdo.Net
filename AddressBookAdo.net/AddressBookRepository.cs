using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookAdo.net
{
    public class AddressBookRepository
    {
        //Connection String
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Method to retreive all data from retreive all data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RetreiveAllData(AddressBookModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = "SELECT * FROM Address_Book_Table";
                SqlCommand command = new SqlCommand(query, connection);
                //Open Connection
                this.connection.Open();
                //Returns object of result set
                SqlDataReader result = command.ExecuteReader();
                //Check Result set has rows or not
                if (result.HasRows)
                {
                    //Parse untill  rows are null
                    while (result.Read())
                    {
                        //Print deatials that are retrived
                        PrintDetails(result, model);

                    }
                    output = "Success";
                }
                else
                {
                    //Console.WriteLine("No Records in the table");
                    output = "Success";
                }
                //close result set
                result.Close();
            }
            catch (Exception ex)
            {
                //handle exception
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }
            return output;

        }

        /// <summary>
        /// Update data using stored procedure
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateEmailIdUsingStoredProcedure(AddressBookModel model)
        {
            string output = string.Empty;
            try
            {
                using (this.connection)
                {
                    //sqlcommand object with stored procedure - dbo.UpdateDetails
                    SqlCommand command = new SqlCommand("dbo.UpdateDetails", connection);
                    //Setting command type
                    command.CommandType = CommandType.StoredProcedure;
                    //Adding values to stored procedures parameters
                    command.Parameters.AddWithValue("@name", model.firstName);
                    command.Parameters.AddWithValue("@emailId", model.emailId);
                    // Opening connection 
                    connection.Open();
                    //Executing using non query returns number of rows affected
                    int res = command.ExecuteNonQuery();

                    if (res >= 1)
                    {
                        output = $"Updated {res} rows";

                    }
                    else
                    {
                        output = "Not Updated";
                    }

                }
                return output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return output;
            }
            finally
            {
                //closing the connection
                connection.Close();
            }
        }

        /// <summary>
        /// Retrive data based on state name or city name
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RetreiveDataBasedOnStateNameOrCityName(AddressBookModel model)
        {
            string output = string.Empty;
            try
            {
                using (this.connection)
                {
                    string query = @"Select * from Address_Book_Table where City='chennai' OR StateName='Tn';";
                    SqlCommand command = new SqlCommand(query, connection);
                    //Open Connection
                    this.connection.Open();
                    //Returns object of result set
                    SqlDataReader result = command.ExecuteReader();

                    //checking result set has rows are not
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            //Print deatials that are retrived
                            PrintDetails(result, model);
                        }
                        //close the reader object
                        result.Close();
                    }
                    output = "Success";
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                output = "Unsuccessfull";
            }
            finally
            {
                //close the connection
                connection.Close();
            }
            return output;
        }

        /// <summary>
        /// Print details
        /// </summary>
        /// <param name="result"></param>
        /// <param name="model"></param>
        public void PrintDetails(SqlDataReader result, AddressBookModel model)
        {
            //reatreive adata and print details
            
            model.firstName = Convert.ToString(result["FirstName"]);
            model.lastName = Convert.ToString(result["LastName"]);
            model.address = Convert.ToString(result["address"]);
            model.city = Convert.ToString(result["City"]);
            model.stateName = Convert.ToString(result["StateName"]);
            model.zipCode = Convert.ToInt32(result["ZipCode"]);
            model.phoneNumber = Convert.ToInt64(result["Phonenum"]);
            model.emailId = Convert.ToString(result["EmailId"]);
            model.addressBookName = Convert.ToString(result["AdressBookName"]);
            model.abType = Convert.ToString(result["AbType"]);
            
            Console.WriteLine($"{model.firstName},{model.lastName},{model.address},{model.city},{model.stateName},{model.zipCode},{model.phoneNumber},{model.emailId},{model.addressBookName},{model.abType}\n");
        }
    }
}
