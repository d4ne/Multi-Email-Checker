using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EmailChecker
{
    public class checkMails
    {

        public string checkHotmail(string handle, string proxy)
        {
            string post = "{\"signInName\":\"" + handle + "@hotmail.com\",\"uaid\":\"b6dc6bf0d79245fb8b776dbe94e189f0\",\"includeSuggestions\":true,\"uiflvr\":1001,\"scid\":100118,\"hpgid\":\"Signup_MemberNamePage_Client\",\"tcxt\":\"CWgjfuI/FE8WuKOZZi9UnI7CE+0v8yzCkM3LPpIIILLJGZZ2319TSb2C78RDi7XT+Pl/yEzj/TmoicNGcAV4oEhT1uTWiS5vYhMKnPqbs9j0pk0jwuqkKuUllHI7QPukfP3kG9H15Bj26S6HdvtLb9KwMbheHfmGj+AELC6J8oXVEIZaHDq8Da1X0lUoJnQg7EpAW2NsnP17OWQTTzcvi+xIevEjaov0jRhBghG0Dr3JFHuPhNRw2Wh2SFDt7Z9hfOm9y/+5le4Jfj3H5aJ3IMhtlg/njAh/P6oS+e8wbL6UGNPV4DkDyv4Dteg+QKwNIjwf4Ev8ObePpnZTAn4+1gtUQlXi2l4516Y6xEy1DtxVBa9Re2msYjNQX6OrNeJvbFCVvstuAzS4KP1w/v7rdSRUaPgeb8F1PqPIDdWGBGaiwP412n2eG1T4KWoPSEZ42ivYZ2Kbetf1uEBHs7d3iMoyQ6UTkltmJGFh/qalrnw=:1:3\"}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://signup.live.com/API/CheckAvailableSigninNames?contextid=75D51F5651519EE0&bk=1507030034&ru=https%3a%2f%2flogin.live.com%2flogin.srf%3fcontextid%3d75D51F5651519EE0%26mkt%3dDE-DE%26lc%3d1031%26bk%3d1507030034&uiflavor=web&uaid=b6dc6bf0d79245fb8b776dbe94e189f0&mkt=DE-DE&lc=1031&lic=1") as HttpWebRequest;

            if (proxy != "localhost")
            {
                request.Proxy = new WebProxy(proxy);
            }

            request.Method = "POST";
            request.Accept = "*/*";
            request.Headers.Add("Origin", "https://signup.live.com");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
            request.Headers.Add("canary", "V+SgCrwkkiampPRw58Nsj30BJE7/IJ+DF87OlG9OQeseBZEpgywkBcXM3p7zhBF0EYIZVzw2TA3P/GwqjtDy2UFZUi5xRY67fXg2ryghr4myZ4/usz2yNeA5nCWrUp7fqu/E2id6ISPn6HuWyk1B37OoBwA2j0TIZdzXFLTTfhzMCrQGhVnyOiYFgyRFosDMgb4CjvB4tGb1TJtPGvCKw2TLjWl4eigvBfRgK/ALy6fRI+xs4RDYSQCjz1tIabLc:1:3c");
            request.Host = "signup.live.com";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.KeepAlive = true;
            request.Headers.Add("Cookie", "wla42=; MSCC=1507030021; mkt=de-DE; AVC=v=1.44.1.1196&t=10/04/2017 15:20:47; amsc=tKQAOxQF+8Zt92TnarAPNZ7MBRVKd5D5h3cty+z+uDrk/Fs1P7VvN/iwHl+gAmsHdmfFrNzDF+Weh9cJk4EqQ00sHQc7q4eOdoS67NY1BR4+u8fWQGkNHLXGdwoOb4RsYNOtVwaaOrrHzb8WdClMQN1TLSdYZwwqossdEU/X191JOmSX2lEhBcFhADkrMsZVaM7HyL+QuVnwqyDqMWtfGdLwlmhdGEgwDks3G6UOZhy4GGP5F/ztnWXTTfx+vgA3:1:3c; mkt1=de-DE; DID=1770");
            request.Referer = "https://signup.live.com/signup?contextid=75D51F5651519EE0&bk=1507030034&ru=https%3a%2f%2flogin.live.com%2flogin.srf%3fcontextid%3d75D51F5651519EE0%26mkt%3dDE-DE%26lc%3d1031%26bk%3d1507030034&uiflavor=web&uaid=b6dc6bf0d79245fb8b776dbe94e189f0&mkt=DE-DE&lc=1031&lic=1";

            byte[] postBytes = Encoding.ASCII.GetBytes(post);
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (html.Contains("\"isAvailable\":false"))
            {
                return "false";
            }
            else if(html.Contains("1220"))
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public string checkYahoo(string handle, string proxy)
        {
            if(handle.Length < 4)
            {
                return "carackters";
            }
            else
            {
                string post = "browser-fp-data=%7B%22language%22%3A%22de%22%2C%22color_depth%22%3A24%2C%22resolution%22%3A%7B%22w%22%3A1920%2C%22h%22%3A1080%7D%2C%22available_resolution%22%3A%7B%22w%22%3A1920%2C%22h%22%3A1040%7D%2C%22timezone_offset%22%3A-120%2C%22session_storage%22%3A1%2C%22local_storage%22%3A1%2C%22indexed_db%22%3A1%2C%22open_database%22%3A1%2C%22cpu_class%22%3A%22unknown%22%2C%22navigator_platform%22%3A%22Win32%22%2C%22do_not_track%22%3A%22unknown%22%2C%22canvas%22%3A%22canvas%20winding%3Ayes~canvas%22%2C%22webgl%22%3A1%2C%22adblock%22%3A0%2C%22has_lied_languages%22%3A0%2C%22has_lied_resolution%22%3A0%2C%22has_lied_os%22%3A0%2C%22has_lied_browser%22%3A0%2C%22touch_support%22%3A%7B%22points%22%3A0%2C%22event%22%3A0%2C%22start%22%3A0%7D%2C%22plugins%22%3A%7B%22count%22%3A4%2C%22hash%22%3A%2263899fc39ae54819a22a20e37bb2d2d1%22%7D%2C%22fonts%22%3A%7B%22count%22%3A35%2C%22hash%22%3A%220b9c90628ff20d0d70ae34bcc57ea4cd%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1507032957576%2C%22render%22%3A1507033041359%7D%7D&specId=yidReg&cacheStored=true&crumb=za9qfJLgRb9&acrumb=DGepjH7X&c=&sessionIndex=&done=https%3A%2F%2Fmail.yahoo.com%2F%3F.intl%3Dde%26amp%3B.lang%3Dde-DE%26amp%3B.partner%3Dnone%26amp%3B.src%3Dfp&googleIdToken=&authCode=&tos0=yahoo_freereg%7Cde%7Cde-DE&tos1=yahoo_comms_atos%7Cde%7Cde-DE&firstName=&lastName=&yid=" + handle + "&password=&shortCountryCode=DE&phone=&mm=&dd=&yyyy=&freeformGender=";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://login.yahoo.com/account/module/create?validateField=yid") as HttpWebRequest;

                request.Method = "POST";
                request.Accept = "*/*";
                request.Headers.Add("Origin", "https://login.yahoo.com");
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
                request.Host = "login.yahoo.com";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.KeepAlive = true;
                request.Headers.Add("Cookie", "B=a5hdl75ct5gr0&b=3&s=lm; AS=v=1&s=DGepjH7X&d=C59d4d0f9|EUaFUr3.2cLT2JrwDutLTnBVkng.HwrhhYYUDk0y7LkoVYkrKt2sXXgseZaK66lynj3D2fKZ5ft4jhC0Yala5cmoOIF9iBAMDrq2uZpv4Gd8.NJo3Retb0xYUmURiufFt6.z1H11rORfnG.OdlHrAtx31J1zomosTjpCxtA5qjP3F9wLD6pbgaa9U8iqwWrCZlj3HY2exFqGyYLELMaaj0EYu9RWcejbc8qw9G7B_8tuGDgCf_1AQzh9aaSf6McQF1m6vcYSYx9jhiSl4GZhrCJjWcpPhq49KpNuMi0ZSV1W6l_gVtFG8m.FaAYeMh4CkZFoA05g4c4oJ2Y.yrL0RR3ey26J5WGyo0624AGNkaJ5udiaSVvmjDfo98axvrhQ.r6aRLxkajRSODsZMIJ5Va98OQ7VADO82dJJKBtGS37YHmkPNwxqodAHaCWaNcPUF.MRvFAQu9SseU24.So5BSCx_h7S2kC_14JeRuDGKREIkVVc5b1dpQ8BxgUqiK4.0jDsEUh2qdNzKKo3nUL6_W0JvY5FGAil24KBJmsx27kSsIdmrVrj2p4uksLHvLdvEUwcA2AzL.B_ixFznbQwcdpZL8ZFycIJjfGf6UFS8GzGTrC6xACYTOQ570ulTsDlETz9Q9DOzndNyxrCyYfmUN2MMJb.RR5ELQwDnstpxxbYAYjGrVzxBuKVkuQr74PzSvXYIVecbI_ZZJ8qlSaN0znRfwf3RmyDPpHMyCR.BMbiIKkZh_OmUxQbgk5WtuobL66WEpSu8zihi1Lx1h90PlBNo4msb6Yi2P.llcZnPyjdcwz1GTEH4DMzkadcGIO8Qckh2R2s9siixIPqmx3r_Zc.5PJO4Q--~A");
                request.Referer = "https://login.yahoo.com/account/create?src=ym&intl=de&partner=none&done=https%3A%2F%2Fmail.yahoo.com%2F%3F.intl%3Dde%26amp%3B.lang%3Dde-DE%26amp%3B.partner%3Dnone%26amp%3B.src%3Dfp&specId=yidReg";

                byte[] postBytes = Encoding.ASCII.GetBytes(post);
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (html.Contains("\"error\":\"IDENTIFIER_EXISTS\""))
                {
                    return "false";
                }
                else
                {
                    return "true";
                }
            }
        }

        public string checkGmail(string handle, string proxy)
        {
            if(handle.Length < 6)
            {
                return "charackters";
            }
            else
            {
                string post = "{\"input01\":{\"Input\":\"GmailAddress\",\"GmailAddress\":\"" + handle + "\",\"FirstName\":\"\",\"LastName\":\"\"},\"Locale\":\"de\"}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/InputValidator?resource=SignUp&service=mail") as HttpWebRequest;

                if (proxy != "localhost")
                {
                    request.Proxy = new WebProxy(proxy);
                }

                request.Method = "POST";
                request.Accept = "*/*";
                request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                request.Host = "accounts.google.com";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
                request.ContentType = "application/json; charset=utf-8";
                request.KeepAlive = true;
                request.Referer = "https://accounts.google.com/SignUp?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ltmpl=default";

                byte[] postBytes = Encoding.ASCII.GetBytes(post);
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (html.Contains("taken"))
                {
                    return "false";
                }
                else if (html.Contains("try again"))
                {
                    return "notAllowed";
                }
                else
                {
                    return "true";
                }
            }
        }

    }
}
