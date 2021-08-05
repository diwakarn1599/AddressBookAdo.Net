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

        /// <summary>
        /// Add date added column
        /// </summary>
        /// <returns></returns>
        public string RetreiveDataBasedOnDateRange()
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
                    command.CommandText = @"SELECT ab.AddressBookId,ab.AddressBookName,p.PersonId,p.FirstName,p.LastName,p.Address,p.City,p.StateName,p.ZipCode,p.DateAdded,
                                p.PhoneNumber,p.EmailId,pt.PersonType FROM
                                AddressBook AS ab
                                INNER JOIN Person AS p ON ab.AddressBookId = p.AddressBookId AND DateAdded BETWEEN ('2018-03-01') AND GetDate()
                                INNER JOIN PersonTypesMap as ptm On ptm.PersonId = p.PersonId
                                INNER JOIN PersonTypes AS pt ON pt.PersonTypeId = ptm.PersonTypeId;";
                    //Execute command
                    SqlDataReader result = command.ExecuteReader();
                    //Check Result set has rows or not
                    if (result.HasRows)
                    {
                        //Parse untill  rows are null
                        while (result.Read())
                        {
                            //Print deatials that are retrived
                            PrintDetails(result);

                        }
                        output = "Success";
                    }
                    else
                    {
                        output = "Unsuccessfull";
                        transaction.Rollback();
                    }
                    //close the reader
                    result.Close();
                    //if all executes are success commit the transaction
                    transaction.Commit();
                    
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
                
            }
            return output;
        }

        /// <summary>
        /// Print details
        /// </summary>
        /// <param name="result"></param>
        /// <param name="model"></param>
        public void PrintDetails(SqlDataReader result)
        {
            AddressBookModel model = new AddressBookModel();
            //reatreive adata and print details
            model.addressBookId = Convert.ToInt32(result["AddressBookId"]);
            model.personId = Convert.ToInt32(result["PersonId"]);
            model.addressBookName = Convert.ToString(result["AddressBookName"]);
            model.firstName = Convert.ToString(result["FirstName"]);
            model.lastName = Convert.ToString(result["LastName"]);
            model.address = Convert.ToString(result["address"]);
            model.city = Convert.ToString(result["City"]);
            model.stateName = Convert.ToString(result["StateName"]);
            model.zipCode = Convert.ToInt32(result["ZipCode"]);
            model.phoneNumber = Convert.ToInt64(result["PhoneNumber"]);
            model.emailId = Convert.ToString(result["EmailId"]);
            model.abType = Convert.ToString(result["PersonType"]);
            model.DateAdded = (DateTime)result["DateAdded"];

            Console.WriteLine($"{model.addressBookId},{model.personId},{model.firstName},{model.lastName},{model.address},{model.city},{model.stateName},{model.zipCode},{model.phoneNumber},{model.emailId},{model.DateAdded},{model.addressBookName},{model.abType}\n");
        }

    }
}
