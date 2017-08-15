using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NehouseLibrary
{
    public class nethouse
    {
        string otv;
        int addCount = 0;
        Dictionary<string, string> ampersands = new Dictionary<string, string>();

        #region WEB - запросы

        /// <summary>
        /// Метод возвращает ответ на запрос в кодировке 1251
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string getRequestEncod(string url)
        {
            otv = "";
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy = null;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding(1251));
            otv = ressr.ReadToEnd();
            res.Close();
            return otv;
        }

        public string getRequest(string url)
        {
            otv = "";
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream());
            otv = ressr.ReadToEnd();
            res.GetResponseStream().Close();
            req.GetResponse().Close();

            return otv;
        }

        public string getRequest(CookieContainer cookie, string url)
        {
            otv = "";
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy = null;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.CookieContainer = cookie;
            res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream());
            otv = ressr.ReadToEnd();
            res.GetResponseStream().Close();
            req.GetResponse().Close();
            res.Close();
            return otv;
        }

        public string getRequest(CookieContainer cookie, string url, string stringQuery)
        {
            otv = "";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms1 = Encoding.ASCII.GetBytes(stringQuery);
            req.ContentLength = ms1.Length;
            Stream stre1 = req.GetRequestStream();
            stre1.Write(ms1, 0, ms1.Length);
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            otv = ressrImg.ReadToEnd();
            resimg.Close();
            return otv;
        }

        public string PostRequest(CookieContainer cookie, string url)
        {
            otv = "";
            HttpWebResponse res = null;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            res = (HttpWebResponse)req.GetResponse();
            StreamReader ressr = new StreamReader(res.GetResponseStream());
            otv = ressr.ReadToEnd();

            return otv;
        }

        public CookieContainer webCookie(string url)
        {
            CookieContainer cooc = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cooc;
            Stream stre = req.GetRequestStream();
            stre.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            return cooc;
        }

        /// <summary>
        /// Метод возвращает кукисы после авторизации на сайте nethouse.ru
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CookieContainer CookieNethouse(string login, string password)
        {
            otv = "";
            CookieContainer cookie = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://nethouse.ru/signin");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms = Encoding.ASCII.GetBytes("login=" + login + "&password=" + password + "&quick_expire=0&submit=%D0%92%D0%BE%D0%B9%D1%82%D0%B8");
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            res.Close();
            return cookie;
        }

        public string PostRequest(CookieContainer cookie, string url, string requestStr)
        {
            otv = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            requestStr = requestStr.Replace("false", "0").Replace("true", "1").Replace("+", "%2B");

            byte[] ms = Encoding.GetEncoding("utf-8").GetBytes(requestStr);
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res1 = (HttpWebResponse)req.GetResponse();
            StreamReader ressr1 = new StreamReader(res1.GetResponseStream());
            otv = ressr1.ReadToEnd();
            res1.Close();
            ressr1.Close();

            return otv;
        }

        #endregion

        #region Загрузка CSV файла на сайт

        public void UploadCSVNethouse(CookieContainer cookie, string nameFile)
        {
            string trueOtv = null;
            do
            {
                string otvimg = DownloadNaSite(cookie, nameFile);
                string check = "{\"success\":true,\"imports\":{\"state\":1,\"errorCode\":0,\"errorLine\":0}}";
                do
                {
                    System.Threading.Thread.Sleep(2000);
                    otvimg = ChekedLoading(cookie);
                }
                while (otvimg == check);

                trueOtv = new Regex("(?<=\":{\"state\":).*?(?=,\")").Match(otvimg).ToString();
                string error = new Regex("(?<=errorCode\":).*?(?=,\")").Match(otvimg).ToString();

                if (error == "13")
                    ErrUpload13(otvimg, nameFile);

                if (error == "37")
                    ErrUpload37(otvimg, nameFile);

                if (error == "27")
                    ErrUpload27(otvimg, nameFile);

                if (error == "10")
                    ErrUpload13(otvimg, nameFile);
            }
            while (trueOtv != "2");
        }

        private string DownloadNaSite(CookieContainer cookie, string nameFile)
        {
            string epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString().Replace(",", "");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/export-import/import-from-csv?fileapi" + epoch);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=---------------------------12709277337355";
            req.CookieContainer = cookie;
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            byte[] csv = File.ReadAllBytes(nameFile);
            byte[] end = Encoding.ASCII.GetBytes("\r\n-----------------------------12709277337355\r\nContent-Disposition: form-data; name=\"_catalog_file\"\r\n\r\n" + nameFile + "\r\n-----------------------------12709277337355--\r\n");
            byte[] ms1 = Encoding.ASCII.GetBytes("-----------------------------12709277337355\r\nContent-Disposition: form-data; name=\"catalog_file\"; filename=\"" + nameFile + "\"\r\nContent-Type: text/csv\r\n\r\n");
            req.ContentLength = ms1.Length + csv.Length + end.Length;
            Stream stre1 = req.GetRequestStream();
            stre1.Write(ms1, 0, ms1.Length);
            stre1.Write(csv, 0, csv.Length);
            stre1.Write(end, 0, end.Length);
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            string otvimg = ressrImg.ReadToEnd();
            return otvimg;
        }

        private void ErrUpload10(string otv, string nameFile)
        {
            try
            {
                string errstr = new Regex("(?<=errorLine\":).*?(?=,\")").Match(otv).ToString();
                string[] naSite = File.ReadAllLines(nameFile, Encoding.GetEncoding(1251));
                string[] newList = new string[naSite.Length - 1];
                int i = 0;
                string delString = naSite[Convert.ToInt32(errstr) - 1].ToString();
                foreach (string str in naSite)
                {
                    if (str != delString)
                        newList[i] = str;
                    i++;
                }
                File.WriteAllLines(nameFile, newList, Encoding.GetEncoding(1251));
            }
            catch
            {

            }

        }

        private void ErrUpload27(string otv, string nameFile)
        {
            string errstr = new Regex("(?<=errorLine\":).*?(?=,\")").Match(otv).ToString();
            string[] naSite = File.ReadAllLines(nameFile, Encoding.GetEncoding(1251));
            int u = Convert.ToInt32(errstr) - 1;
            string[] s = naSite[u].ToString().Split(';');
        }

        private void ErrUpload13(string otv, string nameFile)
        {
            string errstr = new Regex("(?<=errorLine\":).*?(?=,\")").Match(otv).ToString();
            string[] naSite = File.ReadAllLines(nameFile, Encoding.GetEncoding(1251));
            int u = Convert.ToInt32(errstr) - 1;
            string[] strslug3 = naSite[u].ToString().Split(';');
            string strslug = strslug3[strslug3.Length - 5];
            int slug = strslug.Length;
            int countAdd = ReturnCountAdd();
            int countDel = countAdd.ToString().Length;
            if (strslug.Contains("\""))
            {
                countDel = countDel + 2;
            }
            string strslug2 = strslug.Remove(slug - countDel);
            strslug2 += countAdd;
            strslug2 = strslug2.Replace("”", "").Replace("~", "").Replace("#", "").Replace("?", "");
            if (strslug2.Contains("\""))
            {
                strslug2 = strslug2 + "\"";
                countDel = countDel - 2;
            }
            naSite[u] = naSite[u].Replace(strslug, strslug2);
            File.WriteAllLines(nameFile, naSite, Encoding.GetEncoding(1251));
        }

        private void ErrUpload37(string otv, string nameFile)
        {
            string errstr = new Regex("(?<=errorLine\":).*?(?=,\")").Match(otv).ToString();
            string[] naSite = File.ReadAllLines(nameFile, Encoding.GetEncoding(1251));
            int u = Convert.ToInt32(errstr) - 1;
            string[] strslug3 = naSite[u].Split(';');
            int slugint = strslug3.Length - 5;
            string strslug = strslug3[slugint].ToString();
            int slug = strslug.Length;
            int countAdd = ReturnCountAdd();
            int countDel = countAdd.ToString().Length;
            string strslug2 = strslug.Remove(slug - countDel);
            strslug2 += countAdd;
            strslug2 = strslug2.Replace(" -", "-").Replace("?", "").Replace("%", "");
            naSite[u] = naSite[u].Replace(strslug, strslug2);
            File.WriteAllLines(nameFile, naSite, Encoding.GetEncoding(1251));
        }

        private int ReturnCountAdd()
        {
            if (addCount == 99)
                addCount = 0;
            addCount++;
            return addCount;
        }

        private string ChekedLoading(CookieContainer cookie)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/export-import/check-import");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentLength = 0;
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            Stream stre1 = req.GetRequestStream();
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            string otvimg = ressrImg.ReadToEnd();
            return otvimg;
        }

        #endregion

        #region Работа с изображениями на сайте

        public void UploadImage(CookieContainer cookieBike18, string urlProduct)
        {
            string otv = null;

            if (!urlProduct.Contains("nethouse"))
            {
                urlProduct = urlProduct.Replace(".ru", ".nethouse.ru");
            }

            otv = PostRequest(cookieBike18, urlProduct);
            String article = new Regex("(?<=Артикул:)[\\w\\W]*?(?=</div>)").Match(otv).Value.Trim();
            if (article.Length > 128 || article.Contains(" "))
            {
                MatchCollection articles = new Regex("(?<=Артикул:)[\\w\\W]*?(?=</div>)").Matches(otv);
                if (articles.Count >= 2)
                    article = articles[1].ToString().Trim();
                else
                {

                }
            }

            MatchCollection prId = new Regex("(?<=data-id=\").*?(?=\")").Matches(otv);
            string productId = prId[0].ToString();

            Image newImg = Image.FromFile("Pic\\" + article + ".jpg");
            double widthImg = newImg.Width;
            double heigthImg = newImg.Height;

            if (widthImg > heigthImg)
            {
                double dblx = widthImg * 0.9;
                if (dblx < heigthImg)
                    heigthImg = heigthImg * 0.9;
                else
                    widthImg = widthImg * 0.9;
            }
            else
            {
                double dblx = heigthImg * 0.9;
                if (dblx < widthImg)
                    widthImg = widthImg * 0.9;
                else
                    heigthImg = heigthImg * 0.9;
            }

            string epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString().Replace(",", "");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/putimg?fileapi" + epoch);
            req.Accept = "*/*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=----WebKitFormBoundaryDxXeyjY3R0nRHgrP";
            req.CookieContainer = cookieBike18;
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            byte[] pic = File.ReadAllBytes("Pic\\" + article + ".jpg");
            byte[] end = Encoding.ASCII.GetBytes("\r\n------WebKitFormBoundaryDxXeyjY3R0nRHgrP\r\nContent-Disposition: form-data; name=\"_file\"\r\n\r\n" + article + ".jpg\r\n------WebKitFormBoundaryDxXeyjY3R0nRHgrP--\r\n");
            byte[] ms1 = Encoding.ASCII.GetBytes("------WebKitFormBoundaryDxXeyjY3R0nRHgrP\r\nContent-Disposition: form-data; name=\"file\"; filename=\"4680329013422.jpg\"\r\nContent-Type: image/jpeg\r\n\r\n");
            req.ContentLength = ms1.Length + pic.Length + end.Length;
            Stream stre1 = req.GetRequestStream();
            stre1.Write(ms1, 0, ms1.Length);
            stre1.Write(pic, 0, pic.Length);
            stre1.Write(end, 0, end.Length);
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            string otvimg = ressrImg.ReadToEnd();
            resimg.Close();
            string urlSaveImg = new Regex("(?<=url\":\").*?(?=\")").Match(otvimg).Value.Replace("\\/", "%2F");

            req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/catalog/save-image");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookieBike18;
            byte[] saveImg = Encoding.ASCII.GetBytes("url=" + urlSaveImg + "&id=0&type=4&objectId=" + productId + "&imgCrop[x]=0&imgCrop[y]=0&imgCrop[width]=" + widthImg + "&imgCrop[height]=" + heigthImg + "&imageId=0&iObjectId=" + productId + "&iImageType=4&replacePhoto=0");
            req.ContentLength = saveImg.Length;
            Stream srSave = req.GetRequestStream();
            srSave.Write(saveImg, 0, saveImg.Length);
            srSave.Close();
            HttpWebResponse resSave = (HttpWebResponse)req.GetResponse();
            StreamReader ressrSave = new StreamReader(resSave.GetResponseStream());
            ressrImg.Close();

            string otvSave = ressrSave.ReadToEnd();
        }

        public string DownloadImages(CookieContainer cookie, string artProd)
        {
            string epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString().Replace(",", "");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/putimg?fileapi" + epoch);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "multipart/form-data; boundary=---------------------------12709277337355";
            req.CookieContainer = cookie;
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            byte[] pic = File.ReadAllBytes("Pic\\" + artProd + ".jpg");
            byte[] end = Encoding.ASCII.GetBytes("\r\n-----------------------------12709277337355\r\nContent-Disposition: form-data; name=\"_file\"\r\n\r\n" + artProd + ".jpg\r\n-----------------------------12709277337355--\r\n");
            byte[] ms1 = Encoding.ASCII.GetBytes("-----------------------------12709277337355\r\nContent-Disposition: form-data; name=\"file\"; filename=\"" + artProd + ".jpg\"\r\nContent-Type: image/jpeg\r\n\r\n");
            req.ContentLength = ms1.Length + pic.Length + end.Length;
            Stream stre1 = req.GetRequestStream();
            stre1.Write(ms1, 0, ms1.Length);
            stre1.Write(pic, 0, pic.Length);
            stre1.Write(end, 0, end.Length);
            stre1.Close();
            HttpWebResponse resimg = (HttpWebResponse)req.GetResponse();
            StreamReader ressrImg = new StreamReader(resimg.GetResponseStream());
            string otvimg = ressrImg.ReadToEnd();
            return otvimg;
        }

        public string SaveImages(CookieContainer cookie, string urlSaveImg, int prodId, double widthImg, double heigthImg)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/catalog/save-image");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] saveImg = Encoding.ASCII.GetBytes("url=" + urlSaveImg + "&id=0&type=4&objectId=" + prodId + "&imgCrop[x]=0&imgCrop[y]=0&imgCrop[width]=" + widthImg + "&imgCrop[height]=" + heigthImg + "&imageId=0&iObjectId=" + prodId + "&iImageType=4&replacePhoto=0");
            req.ContentLength = saveImg.Length;
            Stream srSave = req.GetRequestStream();
            srSave.Write(saveImg, 0, saveImg.Length);
            srSave.Close();
            HttpWebResponse resSave = (HttpWebResponse)req.GetResponse();
            StreamReader ressrSave = new StreamReader(resSave.GetResponseStream());
            string otvSave = ressrSave.ReadToEnd();
            return otvSave;
        }

        #endregion

        #region Замена html-символов на текстовые

        private void AddAmpersands()
        {
            ampersands.Add("&bull;", "·");
            ampersands.Add("&#32;", " ");
            ampersands.Add("&#33;", "!");
            ampersands.Add("&#34;", "\"");
            ampersands.Add("&quot;", "\"");
            ampersands.Add("&#35;", "#");
            ampersands.Add("&#36;", "$");
            ampersands.Add("&#37;", "%");
            ampersands.Add("&#38;", "&");
            ampersands.Add("&amp;", "&");
            ampersands.Add("&#39;", "'");
            ampersands.Add("&#40", "(");
            ampersands.Add("&#41;", ")");
            ampersands.Add("&#42;", "*");
            ampersands.Add("&#43;", "+");
            ampersands.Add("&#44;", ",");
            ampersands.Add("&#45;", "-");
            ampersands.Add("&#46;", ".");
            ampersands.Add("&#47;", "/");
            ampersands.Add("&#48;", "0");
            ampersands.Add("&#49;", "1");
            ampersands.Add("&#50;", "2");
            ampersands.Add("&#51;", "3");
            ampersands.Add("&#52;", "4");
            ampersands.Add("&#53;", "5");
            ampersands.Add("&#54;", "6");
            ampersands.Add("&#55;", "7");
            ampersands.Add("&#56;", "8");
            ampersands.Add("&#57;", "9");
            ampersands.Add("&#58;", ":");
            ampersands.Add("&#59;", ";");
            ampersands.Add("&#60;", "<");
            ampersands.Add("&lt;", "<");
            ampersands.Add("&#61;", "=");
            ampersands.Add("&#62;", ">");
            ampersands.Add("&gt;", ">");
            ampersands.Add("&#63;", "?");
            ampersands.Add("&#64;", "@");
            ampersands.Add("&#65;", "A");
            ampersands.Add("&#66;", "B");
            ampersands.Add("&#67;", "C");
            ampersands.Add("&#68;", "D");
            ampersands.Add("&#69;", "E");
            ampersands.Add("&#70;", "F");
            ampersands.Add("&#71;", "G");
            ampersands.Add("&#72;", "H");
            ampersands.Add("&#73;", "I");
            ampersands.Add("&#74;", "J");
            ampersands.Add("&#75;", "K");
            ampersands.Add("&#76;", "L");
            ampersands.Add("&#77;", "-");
            ampersands.Add("&#78;", "N");
            ampersands.Add("&#79;", "O");
            ampersands.Add("&#80;", "P");
            ampersands.Add("&#81;", "Q");
            ampersands.Add("&#82;", "R");
            ampersands.Add("&#83;", "S");
            ampersands.Add("&#84;", "T");
            ampersands.Add("&#85;", "U");
            ampersands.Add("&#86;", "V");
            ampersands.Add("&#87;", "W");
            ampersands.Add("&#88;", "X");
            ampersands.Add("&#89;", "Y");
            ampersands.Add("&#90;", "Z");
            ampersands.Add("&#91;", "[");
            ampersands.Add("&#92;", "\\");
            ampersands.Add("&#93;", "]");
            ampersands.Add("&#94;", "^");
            ampersands.Add("&#95;", "_");
            ampersands.Add("&#96;", "`");
            ampersands.Add("&#97;", "a");
            ampersands.Add("&#98;", "b");
            ampersands.Add("&#99;", "c");
            ampersands.Add("&#100;", "d");
            ampersands.Add("&#101;", "e");
            ampersands.Add("&#102;", "f");
            ampersands.Add("&#103;", "g");
            ampersands.Add("&#104;", "-");
            ampersands.Add("&#105;", "i");
            ampersands.Add("&#106;", "j");
            ampersands.Add("&#107;", "k");
            ampersands.Add("&#108;", "l");
            ampersands.Add("&#109;", "m");
            ampersands.Add("&#110;", "n");
            ampersands.Add("&#111;", "o");
            ampersands.Add("&#112;", "p");
            ampersands.Add("&#113;", "q");
            ampersands.Add("&#114;", "r");
            ampersands.Add("&#115;", "s");
            ampersands.Add("&#116;", "t");
            ampersands.Add("&#117;", "u");
            ampersands.Add("&#118;", "v");
            ampersands.Add("&#119;", "w");
            ampersands.Add("&#120;", "x");
            ampersands.Add("&#121;", "y");
            ampersands.Add("&#122;", "z");
            ampersands.Add("&#123;", "{");
            ampersands.Add("&#124;", "|");
            ampersands.Add("&#125;", "}");
            ampersands.Add("&#126;", "~");
            ampersands.Add("&#161;", "¡");
            ampersands.Add("&iexcl;", "¡");
            ampersands.Add("&#160;", " ");
            ampersands.Add("&nbsp;", " ");
            ampersands.Add("&#162;", "¢");
            ampersands.Add("&cent;", "¢");
            ampersands.Add("&#163;", "£");
            ampersands.Add("&pound;", "£");
            ampersands.Add("&#164;", "¤");
            ampersands.Add("&curren;", "¤");
            ampersands.Add("&#165;", "¥");
            ampersands.Add("&yen;", "¥");
            ampersands.Add("&#166;", "¦");
            ampersands.Add("&brvbar;", "¦");
            ampersands.Add("&#167;", "§");
            ampersands.Add("&sect;", "§");
            ampersands.Add("&#168;", "¨");
            ampersands.Add("&uml;", "¨");
            ampersands.Add("&#169;", "©");
            ampersands.Add("&copy;", "©");
            ampersands.Add("&#170;", "ª");
            ampersands.Add("&ordf;", "ª");
            ampersands.Add("&#171;", "«");
            ampersands.Add("&laquo;", "«");
            ampersands.Add("&#172;", "¬");
            ampersands.Add("&not;", "¬");
            ampersands.Add("&#173;", "­ ");
            ampersands.Add("&shy;", " ");
            ampersands.Add("&#174;", "®");
            ampersands.Add("&reg;", "®");
            ampersands.Add("&#175;", "¯");
            ampersands.Add("&macr;", "¯");
            ampersands.Add("&#176;", "°");
            ampersands.Add("&deg;", "°");
            ampersands.Add("&#177;", "±");
            ampersands.Add("&plusmn;", "±");
            ampersands.Add("&#178;", "²");
            ampersands.Add("&sup2;", "²");
            ampersands.Add("&#179;", "³");
            ampersands.Add("&sup3;", "³");
            ampersands.Add("&#180;", "´");
            ampersands.Add("&acute;", "´");
            ampersands.Add("&#181;", "µ");
            ampersands.Add("&micro;", "µ");
            ampersands.Add("&#182;", "¶");
            ampersands.Add("&para;", "¶");
            ampersands.Add("&#183;", "·");
            ampersands.Add("&middot;", "·");
            ampersands.Add("&#184;", "¸");
            ampersands.Add("&cedil;", "¸");
            ampersands.Add("&#185;", "¹");
            ampersands.Add("&sup1;", "¹");
            ampersands.Add("&#186;", "º");
            ampersands.Add("&ordm;", "º");
            ampersands.Add("&#187;", "»");
            ampersands.Add("&raquo;", "»");
            ampersands.Add("&#188;", "¼");
            ampersands.Add("&frac14;", "¼");
            ampersands.Add("&#189;", "½");
            ampersands.Add("&frac12;", "½");
            ampersands.Add("&#190;", "¾");
            ampersands.Add("&frac34;", "¾");
            ampersands.Add("&#191;", "¿");
            ampersands.Add("&iquest;", "¿");
            ampersands.Add("&#192;", "À");
            ampersands.Add("&Agrave;", "À");
            ampersands.Add("&#193;", "Á");
            ampersands.Add("&Aacute;", "Á");
            ampersands.Add("&#194;", "Â");
            ampersands.Add("&Acirc;", "Â");
            ampersands.Add("&#195;", "Ã");
            ampersands.Add("&Atilde;", "Ã");
            ampersands.Add("&#196;", "Ä");
            ampersands.Add("&Auml;", "Ä");
            ampersands.Add("&#197;", "Å");
            ampersands.Add("&Aring;", "Å");
            ampersands.Add("&#198;", "Æ");
            ampersands.Add("&AElig;", "Æ");
            ampersands.Add("&#199;", "Ç");
            ampersands.Add("&Ccedil;", "Ç");
            ampersands.Add("&#200;", "È");
            ampersands.Add("&Egrave;", "È");
            ampersands.Add("&#201;", "É");
            ampersands.Add("&Eacute;", "É");
            ampersands.Add("&#202;", "Ê");
            ampersands.Add("&Ecirc;", "Ê");
            ampersands.Add("&#203;", "Ë");
            ampersands.Add("&Euml;", "Ë");
            ampersands.Add("&#204;", "Ì");
            ampersands.Add("&Igrave;", "Ì");
            ampersands.Add("&#205;", "Í");
            ampersands.Add("&Iacute;", "Í");
            ampersands.Add("&#206;", "Î");
            ampersands.Add("&Icirc;", "Î");
            ampersands.Add("&#207;", "Ï");
            ampersands.Add("&Iuml;", "Ï");
            ampersands.Add("&#208;", "Ð");
            ampersands.Add("&ETH;", "Ð");
            ampersands.Add("&#209;", "Ñ");
            ampersands.Add("&Ntilde;", "Ñ");
            ampersands.Add("&#210;", "Ò");
            ampersands.Add("&Ograve;", "Ò");
            ampersands.Add("&#211;", "Ó");
            ampersands.Add("&Oacute;", "Ó");
            ampersands.Add("&#212;", "Ô");
            ampersands.Add("&Ocirc;", "Ô");
            ampersands.Add("&#213;", "Õ");
            ampersands.Add("&Otilde;", "Õ");
            ampersands.Add("&#214;", "Ö");
            ampersands.Add("&Ouml;", "Ö");
            ampersands.Add("&#215;", "×");
            ampersands.Add("&times;", "×");
            ampersands.Add("&#216;", "Ø");
            ampersands.Add("&Oslash;", "Ø");
            ampersands.Add("&#217;", "Ù");
            ampersands.Add("&Ugrave;", "Ù");
            ampersands.Add("&#218;", "Ú");
            ampersands.Add("&Uacute;", "Ú");
            ampersands.Add("&#219;", "Û");
            ampersands.Add("&Ucirc;", "Û");
            ampersands.Add("&#220;", "Ü");
            ampersands.Add("&Uuml;", "Ü");
            ampersands.Add("&#221;", "Ý");
            ampersands.Add("&Yacute;", "Ý");
            ampersands.Add("&#222;", "Þ");
            ampersands.Add("&THORN;", "Þ");
            ampersands.Add("&#223;", "ß");
            ampersands.Add("&szlig;", "ß");
            ampersands.Add("&#224;", "à");
            ampersands.Add("&agrave;", "à");
            ampersands.Add("&#225;", "á");
            ampersands.Add("&aacute;", "á");
            ampersands.Add("&#226;", "â");
            ampersands.Add("&acirc;", "â");
            ampersands.Add("&#227;", "ã");
            ampersands.Add("&atilde;", "ã");
            ampersands.Add("&#228;", "ä");
            ampersands.Add("&auml;", "ä");
            ampersands.Add("&#229;", "å");
            ampersands.Add("&aring;", "å");
            ampersands.Add("&#230;", "æ");
            ampersands.Add("&aelig;", "æ");
            ampersands.Add("&#231;", "ç");
            ampersands.Add("&ccedil;", "ç");
            ampersands.Add("&#232;", "è");
            ampersands.Add("&egrave;", "è");
            ampersands.Add("&#233;", "é");
            ampersands.Add("&eacute;", "é");
            ampersands.Add("&#234;", "ê");
            ampersands.Add("&ecirc;", "ê");
            ampersands.Add("&#235;", "ë");
            ampersands.Add("&euml;", "ë");
            ampersands.Add("&#236;", "ì");
            ampersands.Add("&igrave;", "ì");
            ampersands.Add("&#237;", "í");
            ampersands.Add("&iacute;", "í");
            ampersands.Add("&#238;", "î");
            ampersands.Add("&icirc;", "î");
            ampersands.Add("&#239;", "ï");
            ampersands.Add("&iuml;", "ï");
            ampersands.Add("&#240;", "ð");
            ampersands.Add("&eth;", "ð");
            ampersands.Add("&#241;", "ñ");
            ampersands.Add("&ntilde;", "ñ");
            ampersands.Add("&#242;", "ò");
            ampersands.Add("&ograve;", "ò");
            ampersands.Add("&#243;", "ó");
            ampersands.Add("&oacute;", "ó");
            ampersands.Add("&#244;", "ô");
            ampersands.Add("&ocirc;", "ô");
            ampersands.Add("&#245;", "õ");
            ampersands.Add("&otilde;", "õ");
            ampersands.Add("&#246;", "ö");
            ampersands.Add("&ouml;", "ö");
            ampersands.Add("&#247;", "÷");
            ampersands.Add("&divide;", "÷");
            ampersands.Add("&#248;", "ø");
            ampersands.Add("&oslash;", "ø");
            ampersands.Add("&#249;", "ù");
            ampersands.Add("&ugrave;", "ù");
            ampersands.Add("&#250;", "ú");
            ampersands.Add("&uacute;", "ú");
            ampersands.Add("&#251;", "û");
            ampersands.Add("&ucirc;", "û");
            ampersands.Add("&#252;", "ü");
            ampersands.Add("&uuml;", "ü");
            ampersands.Add("&#253;", "ý");
            ampersands.Add("&yacute;", "ý");
            ampersands.Add("&#254;", "þ");
            ampersands.Add("&thorn;", "þ");
            ampersands.Add("&#255;", "ÿ");
            ampersands.Add("&yuml;", "ÿ");
        }

        /// <summary>
        /// Данный метод заменяет в тексте все html-символы на текстовые
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string ReplaceAmpersandsChar(string text)
        {
            if (text == null)
                return text = "";
            foreach (KeyValuePair<string, string> pair in ampersands)
            {
                text = text.Replace(pair.Key, pair.Value);
            }
            return text;
        }

        #endregion

        #region Удаление товара

        public void DeleteProduct(CookieContainer cookie, List<string> getProduct)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/catalog/deleteproduct");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms = System.Text.Encoding.GetEncoding("utf-8").GetBytes("id=" + getProduct[0] + "&slug=" + getProduct[1] + "&categoryId=" + getProduct[2] + "&productGroup=" + getProduct[3] + "&name=" + getProduct[4] + "&serial=" + getProduct[5] + "&serialByUser=" + getProduct[6] + "&desc=" + getProduct[7] + "&descFull=" + getProduct[8] + "&cost=" + getProduct[9] + "&discountCost=" + getProduct[10] + "&seoMetaDesc=" + getProduct[11] + "&seoMetaKeywords=" + getProduct[12] + "&seoTitle=" + getProduct[13] + "&haveDetail=" + getProduct[14] + "&canMakeOrder=" + getProduct[15] + "&balance=100&showOnMain=" + getProduct[16] + "&isVisible=1&hasSale=0&avatar[id]=" + getProduct[17] + "&avatar[objectId]=" + getProduct[18] + "&avatar[timestamp]=" + getProduct[19] + "&avatar[type]=" + getProduct[20] + "&avatar[name]=" + getProduct[21] + "&avatar[desc]=" + getProduct[22] + "&avatar[ext]=" + getProduct[23] + "&avatar[formats][raw]=" + getProduct[24] + "&avatar[formats][W215]=" + getProduct[25] + "&avatar[formats][150x120]=" + getProduct[26] + "&avatar[formats][104x82]=" + getProduct[27] + "&avatar[formatParams][raw][fileSize]=" + getProduct[28] + "&avatar[alt]=" + getProduct[29] + "&avatar[isVisibleOnMain]=" + getProduct[30] + "&avatar[priority]=" + getProduct[31] + "&avatar[url]=" + getProduct[32] + "&avatar[filters][crop][left]=" + getProduct[33] + "&avatar[filters][crop][top]=" + getProduct[34] + "&avatar[filters][crop][right]=" + getProduct[35] + "&avatar[filters][crop][bottom]=" + getProduct[36] + "&customDays=" + getProduct[37] + "&isCustom=" + getProduct[38]);
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res1 = (HttpWebResponse)req.GetResponse();
            StreamReader ressr1 = new StreamReader(res1.GetResponseStream());
        }

        /// <summary>
        /// Удаляет товар через полученный url
        /// </summary>
        /// <param name="cookie netouse"></param>
        /// <param name="url product"></param>
        public void DeleteProduct(CookieContainer cookie, string url)
        {
            List<string> getProduct = GetProductList(cookie, url);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/api/catalog/deleteproduct");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] ms = System.Text.Encoding.GetEncoding("utf-8").GetBytes("id=" + getProduct[0] + "&slug=" + getProduct[1] + "&categoryId=" + getProduct[2] + "&productGroup=" + getProduct[3] + "&name=" + getProduct[4] + "&serial=" + getProduct[5] + "&serialByUser=" + getProduct[6] + "&desc=" + getProduct[7] + "&descFull=" + getProduct[8] + "&cost=" + getProduct[9] + "&discountCost=" + getProduct[10] + "&seoMetaDesc=" + getProduct[11] + "&seoMetaKeywords=" + getProduct[12] + "&seoTitle=" + getProduct[13] + "&haveDetail=" + getProduct[14] + "&canMakeOrder=" + getProduct[15] + "&balance=100&showOnMain=" + getProduct[16] + "&isVisible=1&hasSale=0&avatar[id]=" + getProduct[17] + "&avatar[objectId]=" + getProduct[18] + "&avatar[timestamp]=" + getProduct[19] + "&avatar[type]=" + getProduct[20] + "&avatar[name]=" + getProduct[21] + "&avatar[desc]=" + getProduct[22] + "&avatar[ext]=" + getProduct[23] + "&avatar[formats][raw]=" + getProduct[24] + "&avatar[formats][W215]=" + getProduct[25] + "&avatar[formats][150x120]=" + getProduct[26] + "&avatar[formats][104x82]=" + getProduct[27] + "&avatar[formatParams][raw][fileSize]=" + getProduct[28] + "&avatar[alt]=" + getProduct[29] + "&avatar[isVisibleOnMain]=" + getProduct[30] + "&avatar[priority]=" + getProduct[31] + "&avatar[url]=" + getProduct[32] + "&avatar[filters][crop][left]=" + getProduct[33] + "&avatar[filters][crop][top]=" + getProduct[34] + "&avatar[filters][crop][right]=" + getProduct[35] + "&avatar[filters][crop][bottom]=" + getProduct[36] + "&customDays=" + getProduct[37] + "&isCustom=" + getProduct[38]);
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res1 = (HttpWebResponse)req.GetResponse();
            StreamReader ressr1 = new StreamReader(res1.GetResponseStream());
        }

        #endregion

        public List<string> GetProductList(CookieContainer cookie, string urlTovar)
        {
            otv = "";
            if (!urlTovar.Contains("nethouse"))
                urlTovar = urlTovar.Replace(".ru/", ".nethouse.ru/");

            List<string> listTovar = new List<string>();
            otv = PostRequest(cookie, urlTovar);
            if (otv == "")
            {
                return listTovar = null;
            }

            string productId = new Regex("(?<=data-product-id=\").*?(?=\">)").Match(otv).ToString();
            if (productId == "")
                productId = new Regex("(?<=data-id=\").*?(?=\")").Match(otv).ToString();
            string article = new Regex("(?<=Артикул:)[\\w\\W]*?(?=</div>)").Match(otv).Value.Trim();
            if (article.Length > 128)
            {
                article = new Regex("(?<=Артикул:)[\\w\\W]*(?=</title>)").Match(otv).ToString().Trim();
            }
            string prodName = new Regex("(?<=<h1>).*(?=</h1>)").Match(otv).Value;
            prodName = ReplaceAmpersandsChar(prodName);
            string price = new Regex("(?<=<span class=\"product-price-data\" data-cost=\").*?(?=\">)").Match(otv).Value;
            if (price == "")
            {
                price = "0";
            }
            string imgId = new Regex("(?<=<div id=\"avatar-).*(?=\")").Match(otv).Value;
            string desc = new Regex("(?<=<div class=\"product__desc show-for-large user-inner\">)[\\w\\W]*?(?=</div>)").Match(otv).Value;
            desc = ReplaceAmpersandsChar(desc);
            string fulldesc = new Regex("(?<=data-ng-non-bindable>)[\\w\\W]*?(?=</div>)").Match(otv).Value.Trim();
            if (fulldesc.Length == 0)
            {
                fulldesc = new Regex("(?<=<div id=\"product-full-desc\")[\\w\\W]*?(?=</div>)").Match(otv).ToString();
                fulldesc = fulldesc.Remove(0, fulldesc.IndexOf('>') + 1);
            }
            fulldesc = ReplaceAmpersandsChar(fulldesc);
            string seometa = new Regex("(?<=<meta name=\"description\" content=\").*?(?=\" >)").Match(otv).Value;
            string keywords = new Regex("(?<=<meta name=\"keywords\" content=\").*?(?=\" >)").Match(otv).Value;
            string title = new Regex("(?<=<title>).*?(?=</title>)").Match(otv).Value;

            MatchCollection paramsTovar = new Regex("(?<=<label class=\"ptype-view-title infoDigits\">)[\\w\\W]*?(?=</select></li>)").Matches(otv);
            string parametrsTovar = "";

            otv = PostRequest(cookie, "https://bike18.nethouse.ru/api/catalog/getproduct?id=" + productId);
            string slug = new Regex("(?<=\",\"slug\":\").*?(?=\")").Match(otv).ToString();
            string reklama = "";
            string markers = new Regex("(?<=markers\":{\").*?(?=\")").Match(otv).ToString();
            if (markers == "1")
                reklama = "&markers[1]=1";
            else
            if (markers == "2")
                reklama = "&markers[2]=1";
            else
            if (markers == "3")
                reklama = "&markers[3]=1";
            else
            if (markers == "4")
                reklama = "&markers[4]=1";
            else
            if (markers == "5")
                reklama = "&markers[5]=1";
            else
            if (markers == "6")
                reklama = "&markers[6]=1";
            else
            if (markers == "7")
                reklama = "&markers[7]=1";

            string balance = new Regex("(?<=,\"balance\":).*?(?=,\")").Match(otv).ToString();
            if (balance.Contains("\""))
                balance = balance.Replace("\"", "");
            string productCastomGroup = new Regex("(?<=productCustomGroup\":).*?(?=,\")").Match(otv).ToString();
            string discountCoast = new Regex("(?<=discountCost\":\").*?(?=\",\")").Match(otv).Value;
            string serial = new Regex("(?<=serial\":\").*?(?=\")").Match(otv).Value;
            string categoryId = new Regex("(?<=\",\"categoryId\":\").*?(?=\")").Match(otv).Value;
            if (categoryId == "")
            {
                categoryId = new Regex("(?<=categoryId\":\").*?(?=\")").Match(otv).ToString();
            }
            string productGroup = new Regex("(?<=productGroup\":).*?(?=,\")").Match(otv).Value;
            string havenDetail = new Regex("(?<=haveDetail\".).*?(?=,\")").Match(otv).Value;
            string canMakeOrder = new Regex("(?<=canMakeOrder\".).*?(?=,\")").Match(otv).Value;
            canMakeOrder = canMakeOrder.Replace("false", "0");
            canMakeOrder = canMakeOrder.Replace("true", "1");
            string showOnMain = new Regex("(?<=showOnMain\".).*?(?=,\")").Match(otv).Value;
            string customDays = new Regex("(?<=,\"customDays\":\").*?(?=\")").Match(otv).Value;
            string isCustom = new Regex("(?<=\",\"isCustom\":).*?(?=,)").Match(otv).Value;
            string atribut = "";
            string atributes = new Regex("(?<=attributes\":{\").*?(?=,\"customDays)").Match(otv).Value;
            MatchCollection stringAtributes = new Regex("(?<=\":{\").*?(?=])").Matches(atributes);
            for (int i = 0; stringAtributes.Count > i; i++)
            {
                string id = new Regex("(?<=primaryKey\":).*?(?=,\")").Match(stringAtributes[i].ToString()).Value;
                string valueId = new Regex("(?<=\"valueId\":\").*?(?=\")").Match(stringAtributes[i].ToString()).Value;
                string valueText = new Regex("(?<=valueText\":).*?(?=})").Match(stringAtributes[i].ToString()).Value;
                string text = new Regex("(?<=\"text\":).*?(?=})").Match(stringAtributes[i].ToString()).Value;
                string checkBox = new Regex("(?<=checkbox\":).*?(?=})").Match(stringAtributes[i].ToString()).Value;

                if (valueId != "")
                {
                    atribut = atribut + "&attributes[" + i + "][primaryKey]=" + id + "&attributes[" + i + "][attributeId]=" + id + "&attributes[" + i + "][values][0][empty]=0&attributes[" + i + "][values][0][valueId]=" + valueId;
                }
                else
                {
                    if (text != "")
                        atribut = atribut + "&attributes[" + i + "][primaryKey]=" + id + "&attributes[" + i + "][attributeId]=" + id + "&attributes[" + i + "][values][0][empty]=0&attributes[" + i + "][values][0][text]=" + text;
                    if (checkBox != "")
                        atribut = atribut + "&attributes[" + i + "][primaryKey]=" + id + "&attributes[" + i + "][attributeId]=" + id + "&attributes[" + i + "][values][0][empty]=0&attributes[" + i + "][values][0][checkbox]=" + checkBox;
                }
            }
            atribut = atribut.Replace("true", "1").Replace("\"", "");
            string alsoBuy = new Regex("(?<=alsoBuy\":).*?(?=,\"markers)").Match(otv).ToString();
            alsoBuy = alsoBuy.Remove(alsoBuy.Length - 1, 1).Remove(0, 1);
            string[] alsoBuyArray = alsoBuy.Split(',');
            string alsoBuyStr = "";

            if (alsoBuyArray.Length > 0)
            {
                for (int i = 0; alsoBuyArray.Length > i; i++)
                {
                    alsoBuyStr += "&alsoBuy[" + i + "]=" + alsoBuyArray[i].ToString();
                }
            }

            otv = PostRequest(cookie, "https://bike18.nethouse.ru/api/catalog/productmedia?id=" + productId);
            string avatarId = new Regex("(?<=\"id\":\").*?(?=\")").Match(otv).Value;
            string objektId = new Regex("(?<=\"objectId\":\").*?(?=\")").Match(otv).Value;
            string timestamp = new Regex("(?<=\"timestamp\":\").*?(?=\")").Match(otv).Value;
            string type = new Regex("(?<=\"type\":\").*?(?=\")").Match(otv).Value;
            string name = new Regex("(?<=\",\"name\":\").*?(?=\")").Match(otv).Value;
            string descimg = new Regex("(?<=\"desc\":\").*?(?=\")").Match(otv).Value;
            string ext = new Regex("(?<=\"ext\":\").*?(?=\")").Match(otv).Value;
            string raw = new Regex("(?<=\"raw\":\").*?(?=\")").Match(otv).Value;
            string W215 = new Regex("(?<=\"W215\":\").*?(?=\")").Match(otv).Value;
            string srimg = new Regex("(?<=\"150x120\":\").*?(?=\")").Match(otv).Value;
            string minimg = new Regex("(?<=\"104x82\":\").*?(?=\")").Match(otv).Value;
            string filesize = new Regex("(?<=\"fileSize\":).*?(?=})").Match(otv).Value;
            string alt = new Regex("(?<=\"alt\":\").*?(?=\")").Match(otv).Value;
            string isvisibleonmain = new Regex("(?<=\"isVisibleOnMain\".).*?(?=,)").Match(otv).Value;
            string prioriti = new Regex("(?<=\"priority\":\").*?(?=\")").Match(otv).Value;
            string avatarurl = new Regex("(?<=\"url\":\").*?(?=\")").Match(otv).Value;
            string filtersleft = new Regex("(?<=\"left\":).*?(?=,)").Match(otv).Value;
            string filterstop = new Regex("(?<=\"top\":).*?(?=,)").Match(otv).Value;
            string filtersright = new Regex("(?<=\"right\":).*?(?=,)").Match(otv).Value;
            string filtersbottom = new Regex("(?<=\"bottom\":).*?(?=})").Match(otv).Value;

            listTovar.Add(productId);       //0
            listTovar.Add(slug);            //1
            listTovar.Add(categoryId);      //2
            listTovar.Add(productGroup);    //3
            listTovar.Add(prodName);        //4
            listTovar.Add(serial);          //5
            listTovar.Add(article);         //6
            listTovar.Add(desc);            //7
            listTovar.Add(fulldesc);        //8
            listTovar.Add(price);           //9
            listTovar.Add(discountCoast);   //10
            listTovar.Add(seometa);         //11
            listTovar.Add(keywords);        //12
            listTovar.Add(title);           //13
            listTovar.Add(havenDetail);     //14
            listTovar.Add(canMakeOrder);    //15 купить с сайта в 1 клик
            listTovar.Add(showOnMain);      //16
            listTovar.Add(avatarId);        //17
            listTovar.Add(objektId);        //18
            listTovar.Add(timestamp);       //19
            listTovar.Add(type);            //20
            listTovar.Add(name);            //21
            listTovar.Add(descimg);         //22
            listTovar.Add(ext);             //23
            listTovar.Add(raw);             //24
            listTovar.Add(W215);            //25
            listTovar.Add(srimg);           //26
            listTovar.Add(minimg);          //27
            listTovar.Add(filesize);        //28
            listTovar.Add(alt);             //29
            listTovar.Add(isvisibleonmain); //30
            listTovar.Add(prioriti);        //31
            listTovar.Add(avatarurl);       //32
            listTovar.Add(filtersleft);     //33
            listTovar.Add(filterstop);      //34
            listTovar.Add(filtersright);    //35
            listTovar.Add(filtersbottom);   //36
            listTovar.Add(customDays);      //37
            listTovar.Add(isCustom);        //38
            listTovar.Add(reklama);         //39
            listTovar.Add(atribut);         //40
            listTovar.Add(productCastomGroup); //41
            listTovar.Add(alsoBuyStr);      //42
            listTovar.Add(balance);         //43
            listTovar.Add(parametrsTovar);  //44

            return listTovar;
        }

        public void WriteFileInCSV(List<string> newProduct, string nameFile)
        {
            StreamWriter newProductcsv = new StreamWriter(nameFile + ".csv", true, Encoding.GetEncoding("windows-1251"));
            int count = newProduct.Count - 1;
            for (int i = 0; count > i; i++)
            {
                newProductcsv.Write(newProduct[i], Encoding.GetEncoding("windows-1251"));
                newProductcsv.Write(";");
            }
            newProductcsv.Write(newProduct[count], Encoding.GetEncoding("windows-1251"));
            newProductcsv.WriteLine();
            newProductcsv.Close();
        }

        /// <summary>
        /// Метод возвращает стоимость с учетом скидки указанной в процентах
        /// </summary>
        /// <param name="priceTovar"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public int ReturnPrice(double priceTovar, double discount)
        {
            priceTovar = priceTovar - (priceTovar * discount);
            priceTovar = Math.Round(priceTovar);
            int price = Convert.ToInt32(priceTovar);
            price = (price / 10) * 10;
            return price;
        }

        public string SaveTovar(CookieContainer cookie, List<string> getProduct)
        {
            string otv = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://bike18.nethouse.ru/api/catalog/saveproduct");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            string hasSale = "0";
            if (getProduct[10] != "")
                hasSale = "1";
            string descFull = getProduct[8].ToString();

            getProduct[8] = descFull;
            string request = "id=" + getProduct[0] + "&slug=" + getProduct[1] + "&categoryId=" + getProduct[2] + "&productCustomGroup=" + getProduct[41] + "&productGroup=" + getProduct[3] + "&name=" + getProduct[4] + "&serial=" + getProduct[5] + "&serialByUser=" + getProduct[6] + "&desc=" + getProduct[7] + "&descFull=" + getProduct[8] + "&cost=" + getProduct[9] + "&discountCost=" + getProduct[10] + "&seoMetaDesc=" + getProduct[11] + "&seoMetaKeywords=" + getProduct[12] + "&seoTitle=" + getProduct[13] + "&haveDetail=" + getProduct[14] + "&canMakeOrder=" + getProduct[15] + "&balance=" + getProduct[43] + "&showOnMain=" + getProduct[16] + "&isVisible=1&hasSale=" + hasSale + getProduct[44] + "&customDays=" + getProduct[37] + "&isCustom=" + getProduct[38] + getProduct[39] + getProduct[40] + getProduct[42] + "&alsoBuyLabel=%D0%9F%D0%BE%D1%85%D0%BE%D0%B6%D0%B8%D0%B5%20%D1%82%D0%BE%D0%B2%D0%B0%D1%80%D1%8B%20%D0%B2%20%D0%BD%D0%B0%D1%88%D0%B5%D0%BC%20%D0%BC%D0%B0%D0%B3%D0%B0%D0%B7%D0%B8%D0%BD%D0%B5";
            request = request.Replace("false", "0").Replace("true", "1").Replace("+", "%2B");

            byte[] ms = Encoding.GetEncoding("utf-8").GetBytes(request);
            req.ContentLength = ms.Length;
            Stream stre = req.GetRequestStream();
            stre.Write(ms, 0, ms.Length);
            stre.Close();
            HttpWebResponse res1 = (HttpWebResponse)req.GetResponse();
            StreamReader ressr1 = new StreamReader(res1.GetResponseStream());
            otv = ressr1.ReadToEnd();
            res1.Close();
            ressr1.Close();

            return otv;
        }

        /// <summary>
        /// Метод создает в директории с программой файл CSV, для новых товаров
        /// </summary>
        /// <param name="nameFile"></param>
        public void NewListUploadinBike18(string nameFile)
        {
            List<string> newProduct = new List<string>();
            newProduct.Add("id");                                                                               //id
            newProduct.Add("Артикул *");                                                 //артикул
            newProduct.Add("Название товара *");                                          //название
            newProduct.Add("Стоимость товара *");                                    //стоимость
            newProduct.Add("Стоимость со скидкой");                                       //со скидкой
            newProduct.Add("Раздел товара *");                                         //раздел товара
            newProduct.Add("Товар в наличии *");                                                    //в наличии
            newProduct.Add("Поставка под заказ *");                                                 //поставка
            newProduct.Add("Срок поставки (дни) *");                                           //срок поставки
            newProduct.Add("Краткий текст");                                 //краткий текст
            newProduct.Add("Текст полностью");                                          //полностью текст
            newProduct.Add("Заголовок страницы (title)");                               //заголовок страницы
            newProduct.Add("Описание страницы (description)");                                 //описание
            newProduct.Add("Ключевые слова страницы (keywords)");                                 //ключевые слова
            newProduct.Add("ЧПУ страницы (slug)");                                   //ЧПУ
            newProduct.Add("С этим товаром покупают");                              //с этим товаром покупают
            newProduct.Add("Рекламные метки");
            newProduct.Add("Показывать на сайте *");                                           //показывать
            newProduct.Add("Удалить *");                                    //удалить
            WriteFileInCSV(newProduct, nameFile);
        }

        /// <summary>
        /// Метод возвращает строку для сохранения товара в которой указаны схожие товары.
        /// На сайте производится поиск по имени товара, если в результате меньше 5 позиций то поиск производится по имени раздела товара
        /// </summary>
        /// <param name="tovarList"></param>
        /// <returns></returns>
        public string alsoBuyTovars(List<string> tovarList)
        {
            string name = tovarList[4].ToString();
            string otv = getRequest("https://bike18.ru/products/search?sort=0&balance=&categoryId=&min_cost=&max_cost=&page=1&text=" + name);
            MatchCollection searchTovars = new Regex("(?<=id=\"item).*?(?=\">)").Matches(otv);
            string alsoBuy = "";
            int count = 0;
            if (searchTovars.Count >= 5)
            {
                int countSearch = searchTovars.Count;
                if (countSearch > 5)
                    countSearch = 5;

                for (int i = 1; countSearch > i; i++)
                {

                    alsoBuy += "&alsoBuy[" + count + "]=" + searchTovars[i].ToString();
                    count++;
                }
            }
            else
            {
                otv = getRequest("https://bike18.ru/products/category/" + tovarList[2].ToString());
                string nameCategory = new Regex("(?<=<h1 class=\"category-name\">).*(?=</h1>)").Match(otv).ToString();

                otv = getRequest("https://bike18.ru/products/search/page/1?sort=0&balance=&categoryId=&min_cost=&max_cost=&text=" + nameCategory);
                searchTovars = new Regex("(?<=id=\"item).*?(?=\">)").Matches(otv);
                if (searchTovars.Count > 2)
                {
                    int countSearch = searchTovars.Count;
                    if (countSearch > 5)
                        countSearch = 5;

                    for (int i = 1; countSearch > i; i++)
                    {

                        alsoBuy += "&alsoBuy[" + count + "]=" + searchTovars[i].ToString();
                        count++;
                    }
                }
            }
            return alsoBuy;
        }

        /// <summary>
        /// Данный метод создает 301 редирект со старого url - товара на новый
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="oldCHPU"></param>
        /// <param name="newCHPU"></param>
        /// <returns></returns>
        public string Redirect(CookieContainer cookie, string oldCHPU, string newCHPU)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://bike18.nethouse.ru/redirect/savelink");
            req.Accept = "application/json, text/plain, */*";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = cookie;
            byte[] saveImg = Encoding.ASCII.GetBytes("fromlink=/products/" + oldCHPU + "&tolink=/products/" + newCHPU);
            req.ContentLength = saveImg.Length;
            Stream srSave = req.GetRequestStream();
            srSave.Write(saveImg, 0, saveImg.Length);
            srSave.Close();
            HttpWebResponse resSave = (HttpWebResponse)req.GetResponse();
            StreamReader ressrSave = new StreamReader(resSave.GetResponseStream());
            string otvSave = ressrSave.ReadToEnd();
            return otvSave;
        }

        public string searchTovar(string name, string searchString)
        {
            string otv = null;
            string urlTovarBike = null;

            otv = getRequest("https://bike18.ru/products/search/page/1?sort=0&balance=&categoryId=&min_cost=&max_cost=&text=" + searchString);
            urlTovarBike = new Regex("(?<=<div class=\"product-item__link\"><a href=\").*?(?=\">)").Match(otv).ToString();

            if (urlTovarBike == "")
                urlTovarBike = null;

            return urlTovarBike;
        }

        public string Discount()
        {
            string discount = "<p style=\"text-align: right;\"><span style=\"font-weight: bold; font-weight: bold;\"> 1. <a href=\"https://bike18.ru/oplata-dostavka\">Выгодные условия доставки по всей России!</a></span></p><p style=\"text-align: right;\"><span style=\"font-weight: bold; font-weight: bold;\"> 2. <a href=\"https://bike18.ru/stock\">Нашли дешевле!? 110% разницы Ваши!</a></span></p><p style=\"text-align: right;\"><span style=\"font-weight: bold; font-weight: bold;\"> 3. <a href=\"https://bike18.ru/service\">Также обращайтесь в наш сервис центр в Ижевске!</a></span></p>";
            return discount;
        }

    }
}
