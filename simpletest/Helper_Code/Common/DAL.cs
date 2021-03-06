﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpletest.Model.BusinessLogic.Helper_Code.Common;

namespace simpletest.Helper_Code.Common
{
    class DAL
    {
        public static int executeQuery(string query)
        {
            // Initialization.
            int rowCount = 0;
            string strConn = HomeBusinessLogic.getcon();
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                // Settings.
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 2 * 3600; //// Setting timeeout for longer queries to 12 hours.

                // Open.
                sqlConnection.Open();

                // Result.
                rowCount = sqlCommand.ExecuteNonQuery();

                // Close.
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                // Close.
                sqlConnection.Close();

                throw ex;
            }

            return rowCount;
        }
    }
}
