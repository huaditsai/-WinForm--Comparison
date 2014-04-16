using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison
{
    class CreateIndex
    {
        SaveData saveData = new SaveData();

        public CreateIndex(string path)
        {
            // 讀取所有資料
            //string connectionString = "Dsn=HiveNew";
            //OdbcConnection DbConnection = new OdbcConnection(connectionString);
            //DbConnection.Open();

            //OdbcCommand com = new OdbcCommand(@"select * from socialdata", DbConnection);
            //DataTable table = new DataTable();

            //bool firstRun = true;
            //OdbcDataReader reader = com.ExecuteReader();
            //while (reader.Read())
            //{
            //    try
            //    {
            //        object[] row = new object[reader.FieldCount];

            //        for (int i = 0; i < reader.FieldCount; i++)
            //        {
            //            if (firstRun)
            //                table.Columns.Add(reader.GetString(i), reader[i].GetType());

            //            if (!reader.IsDBNull(i))
            //                row[i] = reader.GetValue(i);
            //        }
            //        firstRun = false;

            //        table.Rows.Add(row);
            //    }
            //    catch (Exception)
            //    { }
            //}
            //DbConnection.Close();


            StreamReader file = new StreamReader(path);
            DataTable table = new DataTable();
            DataColumn column = null;

            // 把列假如到table中
            column = new DataColumn("Id");
            table.Columns.Add(column);
            column = new DataColumn("Brand");
            table.Columns.Add(column);
            column = new DataColumn("Model");
            table.Columns.Add(column);
            column = new DataColumn("Model2");
            table.Columns.Add(column);
            column = new DataColumn("Model3");
            table.Columns.Add(column);
            column = new DataColumn("Color");
            table.Columns.Add(column);

            DataRow row;

            int counter = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("  ", "");
                line = line.Replace("\t", ",");
                string[] values = line.Split(',');

                try
                {
                    row = table.NewRow();
                    row["Id"] = values[0];
                    row["Brand"] = values[1];
                    row["Model"] = values[2].Trim();
                    row["Model2"] = values[3].Trim();
                    row["Model3"] = values[4].Trim();
                    //row["Color"] = values[5];
                    table.Rows.Add(row);

                    //saveData.Save("Data.csv", values[0] + "," + values[1] + "," + values[2].Trim() + "," + values[3] + "," + values[4] );
                    //System.IO.File.SetAttributes(@".\PocFile\export\Data.csv", FileAttributes.Normal);
                }
                catch
                {
                    saveData.Save("ExList.txt", line);
                }

                counter++;
            }
            file.Close();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string indexPath = @".\PocFile\";//Index 存放路徑
            FSDirectory dir = FSDirectory.Open(new DirectoryInfo(indexPath));

            IndexWriter indexWriter = new IndexWriter(dir, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), true, IndexWriter.MaxFieldLength.UNLIMITED);//IndexWriter,true改false為新增

            foreach (DataRow datarow in table.Rows)// 還原且加入需做 index 的欄位
            {
                object[] array = datarow.ItemArray;
                Document doc = new Document();
                // 把每一個欄位都建立索引
                try
                {
                    Field f1 = new Field("Id", datarow[0].ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    Field f2 = new Field("Brand", datarow[1].ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    Field f3 = new Field("Model", datarow[2].ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    Field f4 = new Field("Model2", datarow[3].ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    Field f5 = new Field("Model3", datarow[4].ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    //Field f6 = new Field("Color", datarow[5].ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    
                    //Field f_create_time = new Field("create_time", DateTime.Parse(datarow[5].ToString()).ToString("yyyyMMdd"), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                    doc.Add(f1); doc.Add(f2); doc.Add(f3); doc.Add(f4); doc.Add(f5); //doc.Add(f6);
                    indexWriter.AddDocument(doc);
                }
                catch { }
            }
            indexWriter.Optimize();
            indexWriter.Commit();
            indexWriter.Dispose();

        }
        
    }
}
