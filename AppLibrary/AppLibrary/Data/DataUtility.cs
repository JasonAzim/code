using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace AppLibrary.Data
{
    public static class DataUtility
    {
        // utility class does not make database connections. It only works on database objects

        public static bool ISPrimaryKey(DataTable myTable, string MatchColumnName)
        {
            bool result = false;

            // create the array for the columns.
            DataColumn[] colKeys = myTable.PrimaryKey;

            for (int i = 0; i < colKeys.Length; i++)
            {
                if (colKeys[i].ColumnName == MatchColumnName)
                {
                    result = true;
                }
            }

            return result;
        }

        public static DataTable Merge(DataTable dt1, DataTable dt2)
        {
            //Make sure we have some valid data to work with        
            if (dt1 == null || dt2 == null || dt1.Columns.Count < 1 || dt1.Columns.Count < 1)
            {
                return null;
            }
            //Find the common fields.        
            List<String> dt1ColumnsUsed = new List<String>();
            List<String> dt2ColumnsUsed = new List<String>();
            List<String> CommonColumns = new List<String>();
            foreach (DataColumn dc in dt1.Columns)
            {
                if (dt2.Columns.Contains(dc.ColumnName))
                {
                    CommonColumns.Add(dc.ColumnName);
                }
            }
            //Did we find a match?        
            if (CommonColumns.Count < 1)
            {
                return null;
            }
            //no        
            //Else Yes.        
            //Create a new DataTable        
            DataTable newdt = new DataTable();
            //Copy columns from Table 1        
            foreach (DataColumn dc in dt1.Columns)
            {
                DataColumn newdc = new DataColumn();
                newdc.ColumnName = dc.ColumnName;
                newdt.Columns.Add(newdc);
                dt1ColumnsUsed.Add(dc.ColumnName);
            }
            //Copy columns from Table 2        
            foreach (DataColumn dc in dt2.Columns)
            {
                if (!newdt.Columns.Contains(dc.ColumnName))
                {
                    DataColumn newdc = new DataColumn();
                    newdc.ColumnName = dc.ColumnName;
                    newdt.Columns.Add(newdc);
                    dt2ColumnsUsed.Add(dc.ColumnName);
                }
            }
            //Get data from both tables        
            foreach (DataRow dr in dt1.Rows)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    DataRow dr2 = dt2.Rows[i];
                    //if we have more than one.. check here now.
                    bool match = true;
                    foreach (string shared in CommonColumns)
                    {
                        if (dr[shared].ToString() != dr2[shared].ToString())
                        {
                            match = false;
                        }
                    }
                    if (match)
                    {
                        //We have a match..                        
                        DataRow newdr = newdt.NewRow();
                        //Load in data from dt1                        
                        foreach (string dt1col in dt1ColumnsUsed)
                        {
                            newdr[dt1col] = dr[dt1col].ToString();
                        }
                        //Load in data from dt2                        
                        foreach (string dt2col in dt2ColumnsUsed)
                        {
                            newdr[dt2col] = dr2[dt2col].ToString();
                        }
                        //add the new row to the table                        
                        newdt.Rows.Add(newdr);
                    }
                }
            }
            //return the table        
            return newdt;
        }

        public static ArrayList ConvertDataSetToList(DataSet InputDS, string InputMessage)
        {
            ArrayList list = new ArrayList();

            list.Add(InputMessage);

            foreach (DataRow da in InputDS.Tables[0].Rows)
            {
                // just read the first value and make it the list item value
                list.Add(da[0].ToString());
            }
            return list;
        }

        public static List<string> MergeList(List<string> ReturnList, List<string> AddList)
        {
            foreach (string merge in AddList)
            {
                ReturnList.Add(merge);
            }
            return ReturnList;
        }

    }
}
