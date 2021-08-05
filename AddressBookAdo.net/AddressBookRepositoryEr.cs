using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookAdo.net
{
    public class AddressBookRepositoryEr
    {
        //Connection String
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Method to retreive all data from er diagram
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RetreiveAllDataEr(AddressBookModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = @"SELECT ab.AddressBookId,ab.AddressBookName,p.PersonId,p.FirstName,p.LastName,p.Address,p.City,p.StateName,p.ZipCode,
                                p.PhoneNumber,p.EmailId,pt.PersonType FROM
                                AddressBook AS ab
                                INNER JOIN Person AS p ON ab.AddressBookId = p.AddressBookId
                                INNER JOIN PersonTypesMap as ptm On ptm.PersonId = p.PersonId
                                INNER JOIN PersonTypes AS pt ON pt.PersonTypeId = ptm.PersonTypeId; ";
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
        /// Method to retreive all data from er diagram
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RetreiveAllDataStateNameOrCityEr(AddressBookModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = @"SELECT ab.AddressBookId,ab.AddressBookName,p.PersonId,p.FirstName,p.LastName,p.Address,p.City,p.StateName,p.ZipCode,
                                p.PhoneNumber,p.EmailId,pt.PersonType,pt.PersonTypeId FROM
                                AddressBook AS ab 
                                INNER JOIN Person AS p ON ab.AddressBookId = p.AddressBookId AND (p.City='Chennai' OR p.StateName='Tn')
                                INNER JOIN PersonTypesMap as ptm On ptm.PersonId = p.PersonId
                                INNER JOIN PersonTypes AS pt ON pt.PersonTypeId = ptm.PersonTypeId; ";
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
                    output = "UnSuccess";
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
        /// Retrive data based on state name or city name er diagram
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SortBasedOnFirstName(AddressBookModel model)
        {
            string output = string.Empty;
            try
            {
                using (this.connection)
                {
                    string query = @"SELECT ab.AddressBookId,ab.AddressBookName,p.PersonId,p.FirstName,p.LastName,p.Address,p.City,p.StateName,p.ZipCode,
                                    p.PhoneNumber,p.EmailId,pt.PersonType,pt.PersonTypeId FROM
                                    AddressBook AS ab 
                                    INNER JOIN Person AS p ON ab.AddressBookId = p.AddressBookId
                                    INNER JOIN PersonTypesMap as ptm On ptm.PersonId = p.PersonId
                                    INNER JOIN PersonTypes AS pt ON pt.PersonTypeId = ptm.PersonTypeId ORDER BY p.FirstName ;";
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

            Console.WriteLine($"{model.addressBookId},{model.personId},{model.firstName},{model.lastName},{model.address},{model.city},{model.stateName},{model.zipCode},{model.phoneNumber},{model.emailId},{model.addressBookName},{model.abType}\n");
        }


    }
}
