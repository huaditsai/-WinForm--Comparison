using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison
{
    public class SaveData
    {
        string path = @"./PocFile/export/";
        public void Save(string fileName, string text)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            if (!System.IO.File.Exists(path + fileName))
            {
                FileStream fs = System.IO.File.Create(path + fileName);
                fs.Close();
            }
            using (StreamWriter w = System.IO.File.AppendText(path + fileName))
            {
                System.IO.File.SetAttributes(path + fileName, FileAttributes.Hidden);//設定檔案為隱藏
                w.WriteLine(text, Encoding.Default);
            }
        }
    }
}
