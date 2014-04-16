using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comparison
{
    class Search
    {
        public List<string> Searcher(int num, string keyword1, string keyword2)
        {
            List<string> result = new List<string>();

            // 讀取索引
            string indexPath = @".\PocFile\";
            DirectoryInfo dirInfo = new DirectoryInfo(indexPath);
            FSDirectory dir = FSDirectory.Open(dirInfo);
            IndexSearcher search = new IndexSearcher(dir, true);


            // 針對 欄位進行搜尋
            QueryParser parser0 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,
                "Brand", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));

            QueryParser parser1 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,
                "Model", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));

            QueryParser parser1_2 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,
                "Model2", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));

            QueryParser parser1_3 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,
                "Model3", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));

            //parser.DefaultOperator = QueryParser.AND_OPERATOR;

            QueryParser parser2 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,
                "Color", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));

            try
            {
                Query query0 = parser0.Parse(keyword1);// 搜尋的關鍵字

                Query query1 = parser1.Parse(keyword1);// 搜尋的關鍵字
                Query query1_2 = parser1_2.Parse(keyword1);// 搜尋的關鍵字
                Query query1_3 = parser1_3.Parse(keyword1);// 搜尋的關鍵字

                //Query query2 = parser2.Parse(keyword2); //new TermQuery(new Term("Color", keyword2));// 搜尋的關鍵字

                //Query query1 = new FuzzyQuery(new Term("Model", keyword1), 0.8F); //模糊搜尋
                //Query query2 = new TermRangeQuery("create_time",
                //    (Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) - time).ToString(), //開始時間
                //    DateTime.Now.ToString("yyyyMMdd"), true, true); //結束時間,包含頭,尾

                BooleanQuery query = new BooleanQuery();

                query.Add(query0, Occur.MUST);

                if (num == 3)
                {
                    query.Add(query1, Occur.MUST);
                    query.Add(query1_2, Occur.MUST);
                    query.Add(query1_3, Occur.MUST);
                }
                if (num == 2)
                {
                    query.Add(query1, Occur.MUST);
                    query.Add(query1_2, Occur.MUST);
                    query.Add(query1_3, Occur.SHOULD);
                }
                if (num == 1)
                {
                    query.Add(query1, Occur.MUST);
                    query.Add(query1_2, Occur.SHOULD);
                    query.Add(query1_3, Occur.SHOULD);
                }

                //query.Add(query2, Occur.SHOULD);

                ScoreDoc[] hits = search.Search(query, null, search.MaxDoc).ScoreDocs;// 開始搜尋

                //for (int i = 0; i < hits.Length; i++)
                //{
                //    result.Add(search.Doc(hits[i].Doc).Get("Id") +
                //        "," + search.Doc(hits[i].Doc).Get("Brand") +
                //        "," + search.Doc(hits[i].Doc).Get("Model") +
                //        "," + search.Doc(hits[0].Doc).Get("Model2") +
                //        "," + search.Doc(hits[0].Doc).Get("Model3") +
                //        "," + search.Doc(hits[i].Doc).Get("Color")
                //        );
                //}

                //result.Add(search.Doc(hits[0].Doc).Get("Id"));

                result.Add(search.Doc(hits[0].Doc).Get("Id") +
                       "\t" + search.Doc(hits[0].Doc).Get("Brand") +
                       "\t" + search.Doc(hits[0].Doc).Get("Model") +
                       "\t" + search.Doc(hits[0].Doc).Get("Model2") +
                       "\t" + search.Doc(hits[0].Doc).Get("Model3")
                       );
            }
            catch { }

            return result;
        }

    }
}
