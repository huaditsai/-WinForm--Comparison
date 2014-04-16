using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comparison
{
    public partial class Form1 : Form
    {
        SaveData saveData = new SaveData();
        private static object syncHandle = new object();

        List<string> brands = new List<string>(); //產品廠牌
        Stopwatch sw = new Stopwatch();

        Timer timer = new Timer();//左下角的時鐘

        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;

            if (!System.IO.Directory.Exists("./PocFile/"))
                System.IO.Directory.CreateDirectory("./PocFile/");

            string line;
            StreamReader brandfile = new StreamReader(@".\PocFile\Brand.txt");
            while ((line = brandfile.ReadLine()) != null)
            {
                if (line.Length != 0)
                    brands.Add(line);
            }
            brandfile.Close();

            timer.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
            timer.Interval = (1000) * (1);              // Timer will tick evert second
            timer.Enabled = true;                       // Enable the timer
        }

        void timer_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        public void GetBrand(string inPath, string saveFileName)//廠牌同義字,分開廠牌
        {
            string line;
            StreamReader file = new StreamReader(inPath, System.Text.Encoding.Default);
            while ((line = file.ReadLine()) != null)
            {
                line = line.ToLower();

                line = line.Replace("法拉利", "").Replace("升級版", "").Replace("+行動電源", "").Replace("豪華限定版", "").Replace("  ", "").Replace("\t ", "\t");
                line = line.Replace("gb", "g");

                string comp1 = line.Split('\t')[1].Split(' ')[0];//product name
                string comp2 = line.Split('\t')[1].Split('-')[0];//product name
                string mo1 = line.Replace(" - ", " ").Replace("-", " ").Replace("iii", "3").Replace("ii", "2").Replace(" iii", "3").Replace(" ii", "2").Replace("  iii", "3").Replace("  ii", "2"); // delete '-'

                try
                {
                    if (comp1 == "htc")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "motorola")
                        saveData.Save(saveFileName, mo1.Replace(comp1, "motorola\t"));
                    else if (comp1 == "se" || comp1 == "sony")
                        saveData.Save(saveFileName, mo1.Replace("ericsson", "").Replace(comp1, "sony\t"));
                    else if (comp1 == "lg")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "samsung" || comp1 == "sam")
                        saveData.Save(saveFileName, mo1.Replace(comp1, "samsung\t"));
                    else if (comp1 == "iphone")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "nokia")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "bb")
                        saveData.Save(saveFileName, mo1.Replace(comp1, "blackberry\t"));
                    else if (comp1 == "acer")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "twm")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "長江")
                        saveData.Save(saveFileName, mo1.Replace(comp1, "ChangJiang" + "\t"));
                    else if (comp1 == "新加坡 CARD")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else if (comp1 == "聯想 Lenovo")
                        saveData.Save(saveFileName, mo1.Replace(comp1, comp1 + "\t"));
                    else
                    {
                        if (comp2 == "htc")
                            saveData.Save(saveFileName, mo1.Replace(comp2, comp2 + "\t"));
                        else if (comp2 == "mo")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "motorola\t"));
                        else if (comp2 == "se")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "sony\t"));
                        else if (comp2 == "lg")
                            saveData.Save(saveFileName, mo1.Replace(comp2, comp2 + "\t"));
                        else if (comp2 == "sam")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "samsung\t"));
                        else if (comp2 == "no")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "nokia\t"));
                        else if (comp2 == "bb")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "blackberry\t"));
                        else if (comp2 == "as")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "acer\t"));
                        else if (comp2 == "zte")
                            saveData.Save(saveFileName, mo1.Replace(comp2, comp2 + "\t"));
                        else if (comp2 == "ph")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "philips\t"));
                        else if (comp2 == "toshiba")
                            saveData.Save(saveFileName, mo1.Replace(comp2, comp2 + "\t"));
                        else if (comp2 == "長江")
                            saveData.Save(saveFileName, mo1.Replace(comp2, "" + "\t"));
                        else
                            saveData.Save("Other.txt", mo1);
                    }
                }
                catch { }

            }

            file.Close();
        }

        public void GetColor(string inPath, string colorPath, string saveFileName) //產生顏色
        {
            string line;

            StreamReader colorfile = new StreamReader(colorPath);
            List<string> colors = new List<string>();
            while ((line = colorfile.ReadLine()) != null)
            {
                if (line.Length != 0)
                    colors.Add(line);
            }
            colorfile.Close();

            StreamReader file = new StreamReader(inPath);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("  ", "").Replace("\t ", "\t").Replace("(福利品)", "");

                bool isColor = false;
                foreach (string color in colors)
                {
                    if (line.Contains(color) && line.Split('(').Length > 1 && !line.Contains("blackberry"))
                    {
                        saveData.Save(saveFileName, line.Split('(')[0].Trim() + "\t" + color);
                        isColor = true;
                        break;
                    }
                    else if (line.Contains(color) && !line.Contains("blackberry"))
                    {
                        saveData.Save(saveFileName, line.Replace(color, "").Trim() + "\t" + color);
                        isColor = true;
                        break;
                    }
                }

                if (!isColor && line.Split('(').Length > 1)
                    saveData.Save(saveFileName, line.Split('(')[0].Trim() + "\tN");
                else if (!isColor)
                {
                    saveData.Save(saveFileName, line.Split('(')[0].Trim() + "\tN");
                }

            }

            file.Close();
        }

        private void DistinctModel(string inPath, string saveRepeatFileName, string saveFileName)//去除型號相同的(不管顏色)
        {
            string line;
            StreamReader file = new StreamReader(inPath);
            List<string> models = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length != 0)
                    models.Add(line.ToLower());
            }
            file.Close();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string m in models)
            {
                if (!dic.ContainsValue(m.Substring(m.IndexOf(m.Split('\t')[1]))))//比較廠牌型號
                {
                    dic.Add(m.Split('\t')[0], m.Substring(m.IndexOf(m.Split('\t')[1])));//ID,值
                    saveData.Save(saveFileName, m);
                }
                else
                    foreach (var d in dic)//印出對應表
                    {
                        if (d.Value == m.Substring(m.IndexOf(m.Split('\t')[1])))
                            saveData.Save(saveRepeatFileName, d.Key + "\t" + m.Split('\t')[0]);
                    }
            }
            //return (String[])DistinctArray.ToArray(typeof(string));
        }

        public void SplitModel(string inPath, string saveFileName) //分離model
        {
            string line;

            StreamReader file = new StreamReader(inPath);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("  ", "").Replace("\t ", "\t");
                string model = line.Split('\t')[2];
                string[] model1 = model.Split(' ');

                //saveData.Save(saveFileName, line.Replace(line.Split('\t')[2], line.Split('\t')[2] + "\t" + line.Split('\t')[2] + "\t" + line.Split('\t')[2]));

                //if (model1.Length > 3)//3段以上則拆出頭
                //    saveData.Save(saveFileName, line.Replace(model, model1[0] + "\t" + model1[1] + " " + model1[2] + "\t" + model.Replace(model1[0], "").Replace(model1[1] + " " + model1[2], "").Trim()));
                //else 
                if (model1.Length > 2)//兩段以上則拆出頭
                    saveData.Save(saveFileName, line.Replace(model, model1[0] + "\t" + model1[1] + "\t" + model.Replace(model1[0], "").Replace(model1[1], "").Trim()));
                //saveData.Save(saveFileName, line.Replace(model, model + "\t" + model1[0] + "\t" + model.Replace(model1[0], "").Trim()));
                else if (model1.Length == 2)//只有兩段的話就拆開
                    saveData.Save(saveFileName, line.Replace(model, model + "\t" + model1[0] + "\t" + model1[1]));
                else if (model1.Length == 1)//model1 = model2 = model3
                    saveData.Save(saveFileName, line.Split('\t')[0] + "\t" + line.Split('\t')[1] + "\t" + line.Split('\t')[2] + "\t" + line.Split('\t')[2] + "\t" + line.Split('\t')[2] + "\t" + line.Split('\t')[3]);

            }
            file.Close();
        }

        //public void SortModel(string inPath, string saveFileName)
        //{
        //    string line;
        //    StreamReader file = new StreamReader(inPath);
        //    List<string> models = new List<string>();
        //    while ((line = file.ReadLine()) != null)
        //    {
        //        if (line.Length != 0)
        //            models.Add(line);
        //    }
        //    file.Close();

        //    models.Sort((x, y) => { return -x.Split('\t')[2].Split(' ').Length.CompareTo(y.Split('\t')[2].Split(' ').Length); });
        //    foreach (string m in models)
        //        saveData.Save(saveFileName, m);
        //}

        public void Get3CProduct(string htmlFilePath, string exPath, string saveFileName)//得到所有的手機產品
        {
            var Ex = from rline in File.ReadLines(exPath)
                     where rline.Length != 0
                     select rline.ToLower();

            List<string> exx = new List<string>();
            foreach (var ee in Ex)
            {
                exx.Add(ee);
            }

            var Fi = from line in File.ReadLines(htmlFilePath)//以yahoo的keyword, 及排除的ExProduct做篩選
                     where (
                             !exx.Any(s => line.Split('\t')[4].ToLower().Contains(s.ToLower()))
                             || line.Split('\t')[2].ToLower().Contains("送")
                            )
                         && line.Split('\t')[4].ToLower().Contains("手機")
                         && (
                             line.Split('\t')[4].ToLower().Contains("一般型手機")//brands.Any(s => line.Split('\t')[2].ToLower().Contains(s))
                             || line.Split('\t')[4].ToLower().Contains("智慧型手機")
                             || line.Split('\t')[4].ToLower().Contains("apple")
                             || line.Split('\t')[4].ToLower().Contains("htc")
                             || line.Split('\t')[4].ToLower().Contains("samsung")
                             || line.Split('\t')[4].ToLower().Contains("sony")
                            )
                     select line;

            int count = 0;
            Parallel.ForEach(Fi, f =>
            {
                lock (syncHandle)
                {
                    saveData.Save(saveFileName, string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                        f.Split('\t')[0], f.Split('\t')[1], f.Split('\t')[2], f.Split('\t')[3], f.Substring(f.IndexOf("http")))); //All 3C product
                    count++;
                }

            });
            saveData.Save("Number.txt", saveFileName + "\t" + count);
        }

        public string GetKeyword(string original)//處理Keyword
        {
            string desc = original.Split('\t')[2].ToLower().Replace("新加坡", " ").Replace("g-plus", "gplus").Replace("!", " ").Replace("’", " ").Replace("-", " ")
                    .Replace("【", " ").Replace("】", " ").Replace("iii", "3").Replace("ii", "2").Replace(" iii", "3").Replace(" ii", "2").Replace("  iii", "3")
                    .Replace("  ii", "2").Replace("gb", "g").Split('(')[0];

            //將中英混和的文字用空格分開
            ArrayList array = new ArrayList();
            string str = "";
            foreach (var d in desc)
            {
                array.Add(d.ToString());
            }
            for (int i = 0; i < array.Count - 1; i++)
            {
                str += array[i].ToString();

                if ((Regex.IsMatch(array[i].ToString(), "^[\u4e00-\u9fa5]") && Regex.IsMatch(array[i + 1].ToString(), "^[A-Za-z0-9]"))//中文+英文
                    || (Regex.IsMatch(array[i].ToString(), "^[A-Za-z0-9]") && Regex.IsMatch(array[i + 1].ToString(), "^[\u4e00-\u9fa5]")))//英文+中文
                {
                    str += " ";
                }
            }
            return str;
        }

        public void Compare(string productFilePath, int num, string saveFileName, string saveFileName2)//比對
        {
            int count = 0;
            Search s = new Search();

            var Fi = from rline in File.ReadLines(productFilePath)
                     select new { Line = rline };

            Parallel.ForEach(Fi, f =>
            {
                string desc = GetKeyword(f.Line);

                bool isMatch = false;

                foreach (var ss in s.Searcher(num, desc, "n")) //比對
                {
                    isMatch = true;
                    lock (syncHandle)
                    {
                        saveData.Save(saveFileName, string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                                ss,
                                f.Line.Split('\t')[2].Replace(",", " "),//description
                                f.Line.Split('\t')[1],//price
                                f.Line.Split('\t')[3],//user_id
                                f.Line.Split('\t')[0],//gd_id
                                f.Line.Substring(f.Line.IndexOf("http"))));//url
                    }
                    count++;
                }
                if (!isMatch)//若沒對應到則印出原本文字
                {
                    lock (syncHandle)
                        saveData.Save(saveFileName2, f.Line);
                }
            });

            saveData.Save("Number.txt", saveFileName + "\t" + count);
        }

        //private void DeleteRepeat(string inPath, int pos, string saveFileName)//去除不知道為什麼重複的(修好 已無用)
        //{
        //    var Fi = from rline in File.ReadLines(inPath)
        //             select rline;

        //    List<string> dic = new List<string>();
        //    foreach (string f in Fi)
        //    {
        //        if (!dic.Contains(f.Split('\t')[pos]))//比較g_id
        //        {
        //            dic.Add(f.Split('\t')[pos]);//ID,值
        //            saveData.Save(saveFileName, f);
        //        }
        //    }
        //}

        private void CopyRepeat(string inPath, string correspondPath, string saveFileName)//將有重複的對應上
        {
            var Fi = from rline in File.ReadLines(correspondPath)
                     select rline;

            string line;
            StreamReader file = new StreamReader(inPath);
            while ((line = file.ReadLine()) != null)
            {
                saveData.Save(saveFileName, line);
                foreach (var f in Fi)
                {
                    if (f.Split('\t')[0] == line.Split('\t')[0])
                    {
                        saveData.Save(saveFileName, line.Replace(line.Split('\t')[0] + "\t", f.Split('\t')[1] + "\t"));
                    }
                }
            }
            file.Close();

        }

        public void AddSource(string sourcePath, string inPath, string saveFileName)//將原來Source的文字對應上
        {
            var Fi = from rline in File.ReadLines(inPath)
                     select new { Line = rline };

            string line;
            StreamReader file = new StreamReader(sourcePath);
            saveData.Save(saveFileName, "序號\tTWM EC 商品名稱\t對應URL 商品名稱\t對應URL 商品價錢\t對應URL 使用者Id\t對應URL 商品序號\t對應URL");
            while ((line = file.ReadLine()) != null)
            {
                bool isput = false;//是否放入對應料
                foreach (var f in Fi)
                {
                    if (f.Line.Split('\t')[0] == line.Split('\t')[0])//當完成檔ID 與 原始List的ID相同時
                    {
                        isput = true;

                        string str = f.Line;
                        //string str = f.Line.Replace(f.Line.Split(',')[0] + ",", line + "\t").Replace(',','\t');//不知道為什麼會對不準
                        foreach (string uid in str.Split('\t')[7].Split(' '))//把每個時用者分到不同行
                            saveData.Save(saveFileName, line + '\t' + str.Split('\t')[5] + '\t' + str.Split('\t')[6] + '\t' + uid + '\t' + str.Split('\t')[8] + '\t' + str.Split('\t')[9]);
                    }
                }
                System.Threading.Thread.Sleep(10);

                if (!isput)
                    saveData.Save(saveFileName, line);
            }
            file.Close();
        }

        private void Convert(string inPath)
        {
            string[] dirs = Directory.GetFiles(inPath, "*.csv");
            string savePasth = inPath + @"\TwmCompare_" + DateTime.Now.ToString("MMdd") + @"\";
            if (!System.IO.Directory.Exists(savePasth))
            {
                System.IO.Directory.CreateDirectory(savePasth);
            }

            foreach (string dir in dirs)
            {
                var Fi = from line in File.ReadAllLines(dir)
                         select line;
                File.WriteAllLines(savePasth + Path.GetFileName(dir), Fi, Encoding.Unicode);
            }
        }

        private void SplitOtherFileUserId(string inPath, string saveFileName)//把Other檔案的userid分離成不同欄
        {
            var Fi = from line in File.ReadLines(inPath)
                     select line;
            saveData.Save(saveFileName, "商品id\t價錢\t商品名稱\tUser ID\tUrl");
            foreach (string f in Fi)
                foreach (string uid in f.Split('\t')[3].Split(' '))
                    saveData.Save(saveFileName, f.Split('\t')[0] + '\t' + f.Split('\t')[1] + '\t' + f.Split('\t')[2] + '\t' + uid + '\t' + f.Split('\t')[4]);
        }

        private void DelFiles(string path, string fileType)//只留下最後的結果
        {
            string[] dirs = Directory.GetFiles(path, "*." + fileType);
            foreach (string dir in dirs)
            {
                File.Delete(dir);
            }
        }




        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            Search s = new Search();

            string str = GetKeyword(txtInput1.Text.ToLower());

            foreach (string ss in s.Searcher(3, str, "n"))
                txtResult.Text += ss;
            txtResult.Text += "\r\n";
            foreach (string ss in s.Searcher(2, str, "n"))
                txtResult.Text += ss;
            txtResult.Text += "\r\n";
            foreach (string ss in s.Searcher(1, str, "n"))
                txtResult.Text += ss;
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            backgroundWorker1.RunWorkerAsync();

            sw.Stop();
            //txtResult.Text = sw.Elapsed.ToString();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            sw.Start();

            backgroundWorker1.ReportProgress(1, "Data Clearning...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            //GetBrand(@".\PocFile\ECList.txt", "1.poc");//轉換並取得廠牌
            //GetColor(@".\PocFile\export\1.poc", @".\PocFile\color.txt", "2.poc");//取得顏色
            DistinctModel(@".\PocFile\Data.txt", "CorrespondingTable.poc", "Final.poc");//將重複的刪除(視為一個，以ID小的當代表)
            //SortModel(@".\PocFile\export\2.txt", "3.txt");
            //SplitModel(@".\PocFile\export\3.poc", "Final.poc");//將model分為三塊

            backgroundWorker1.ReportProgress(1, "Get Phones...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            Get3CProduct(@".\PocFile\AnalysisReport.csv", @".\PocFile\ExProduct.txt", "product.poc");//取得手機產品
            
            backgroundWorker1.ReportProgress(1, "Create Index...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            new CreateIndex(@".\PocFile\export\Final.poc");//建立索引

            backgroundWorker1.ReportProgress(1, "Comparing...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            Compare(@".\PocFile\export\product.poc", 3, "M3.poc", "O1.poc");//符合model的3個
            Compare(@".\PocFile\export\O1.poc", 2, "M2.poc", "O2.poc");//符合model的2個
            Compare(@".\PocFile\export\O2.poc", 1, "M1.poc", "Other.poc");//符合model的1個   

            System.Threading.Thread.Sleep(1000);

            SplitOtherFileUserId(@".\PocFile\export\Other.poc", "Other.csv");
            SplitOtherFileUserId(@".\PocFile\export\product.poc", "product.csv");

            CopyRepeat(@".\PocFile\export\M3.poc", @".\PocFile\export\CorrespondingTable.poc", "RR.poc");//將 重複 的部份對應回

            backgroundWorker1.ReportProgress(1, "Export Result File...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            AddSource(@".\PocFile\NewList.txt", @".\PocFile\export\M3.poc", "M3.csv");//將 原始 資料對應回
            AddSource(@".\PocFile\NewList.txt", @".\PocFile\export\M2.poc", "M2.csv");//將 原始 資料對應回
            AddSource(@".\PocFile\NewList.txt", @".\PocFile\export\M1.poc", "M1.csv");//將 原始 資料對應回

            System.IO.File.SetAttributes(@".\PocFile\export\product.csv", FileAttributes.Normal); //將隱藏檔案顯示
            System.IO.File.SetAttributes(@".\PocFile\export\M3.csv", FileAttributes.Normal);//將隱藏檔案顯示
            System.IO.File.SetAttributes(@".\PocFile\export\M2.csv", FileAttributes.Normal);//將隱藏檔案顯示
            System.IO.File.SetAttributes(@".\PocFile\export\M1.csv", FileAttributes.Normal);//將隱藏檔案顯示
            System.IO.File.SetAttributes(@".\PocFile\export\Other.csv", FileAttributes.Normal);//將隱藏檔案顯示

            AddSource(@".\PocFile\NewList.txt", @".\PocFile\export\RR.poc", "Result.csv");//將原始資料對應回
            System.IO.File.SetAttributes(@".\PocFile\export\Result.csv", FileAttributes.Normal);//將隱藏檔案顯示

            Convert(@".\PocFile\export\");//轉為Unicode檔

            backgroundWorker1.ReportProgress(1, "Complete");
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            DelFiles(@".\PocFile\export\", "poc");
            DelFiles(@".\PocFile\export\", "csv");

            sw.Stop();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtResult.Text += DateTime.Now.ToString("HH : mm : ss") + "\t" + e.UserState.ToString() + "\r\n";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtResult.Text += "處理時間 : " + sw.Elapsed.ToString();
        }

    }
}
