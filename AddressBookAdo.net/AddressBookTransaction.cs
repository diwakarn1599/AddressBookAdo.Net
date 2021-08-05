using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookAdo.net
{
    public class AddressBookTransaction
    {
        //Connection String
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Add date added column
        /// </summary>
        /// <returns></returns>
        public string AlterTableAddDateAdded()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @"ALTER TABLE Person ADD DateAdded Date";
                    //Execute command
                    command.ExecuteNonQuery();
                    
                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = "Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }

        /// <summary>
        /// Update date added column
        /// </summary>
        /// <returns></returns>
        public string UpdateDateAddedCoulmn()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @"UPDATE Person SET DateAdded='2016-05-06' WHERE PersonId=1";
                    //Execute command
                    command.ExecuteNonQuery();
                    //set command text to command object
                    command.CommandText = @"UPDATE Person SET DateAdded='2019-03-01' WHERE PersonId=2";
                    //Execute command
                    command.ExecuteNonQuery();
                    //set command text to command object
                    command.CommandText = @"UPDATE Person SET DateAdded='2020-08-01' WHERE PersonId=3";
                    //Execute command
                    command.ExecuteNonQuery();

                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = "Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }

    }
}
