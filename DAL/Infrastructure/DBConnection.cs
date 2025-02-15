using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class DBConnection
    {
        public static OdbcConnection getConnectionString()
        {

            string conn = "DRIVER={MySQL ODBC 9.0 Unicode Driver}; SERVER=20.205.170.137; DATABASE=fireguard_dev; UID=shakibe;PASSWORD=#Fireguard2025; OPTION=3; port=3306; stmt=SET NAMES 'utf8';";
            OdbcConnection smsCosnnection = new OdbcConnection(conn);
            return smsCosnnection;
        }

        public static DataSet ExecuteQuery(OdbcCommand cmd)
        {
            // Import Global Sqlconnection in local variable
            using (OdbcConnection conn = getConnectionString())
            {
                try
                {
                    // Set OdbcConnection
                    cmd.Connection = conn;

                    // Open the Sqlconnection

                    conn.Open();
                    cmd.CommandTimeout = 120;
                    // This property create instance of Dataset
                    DataSet dsExecute = new DataSet();

                    // This property create instance of OdbcDataAdapter
                    OdbcDataAdapter daExecute = new OdbcDataAdapter();

                    //Sqlcommand as a select command
                    daExecute.SelectCommand = cmd;

                    //Accept the change
                    daExecute.AcceptChangesDuringFill = false;

                    //Fill Data from DataAdapter to DataSet
                    daExecute.Fill(dsExecute, "Data");

                    DataTable dt = dsExecute.Tables[0];

                    // Close the OdbcConnection
                    conn.Close();

                    //Retunr the dataset
                    return dsExecute;
                }
                catch (Exception ex)
                {
                    //Handle the exception
                    // Close the OdbcConnection
                    conn.Close();
                    throw ex;
                }
            }
        }

        public static long ExecuteNonQueryAndScalar(OdbcCommand cmd)
        {
            using (OdbcConnection conn = getConnectionString())
            {
                OdbcTransaction tx = null;

                // Set the Connection to the new OdbcConnection.
                cmd.Connection = conn;

                OdbcCommand cmd1 = new OdbcCommand("SELECT @@IDENTITY; ");
                cmd1.Connection = conn;

                // Open the connection and execute the transaction.
                try
                {
                    conn.Open();
                    // Start a local transaction
                    tx = conn.BeginTransaction();

                    // Assign transaction object for a pending local transaction.
                    //cmd.Connection = connection;
                    cmd.Transaction = tx;
                    cmd.ExecuteNonQuery();

                    cmd1.Transaction = tx;
                    long id = Convert.ToInt64(cmd1.ExecuteScalar());

                    // Commit the transaction.
                    tx.Commit();
                    // Close the OdbcConnection
                    conn.Close();
                    return id;
                }
                catch (Exception ex)
                {
                    try
                    {
                        // Attempt to roll back the transaction.
                        tx.Rollback();
                        // Close the OdbcConnection
                        conn.Close();
                        throw ex;
                    }
                    catch (Exception ex1)
                    {
                        // Close the OdbcConnection
                        conn.Close();
                        // Do nothing here; transaction is not active.
                        throw ex1;
                    }
                }
                // The connection is automatically closed when the
                // code exits the using block.
            }
        }
    }
}
