using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace prjCooking.Models
{
    public class MailSend
    {
        //測試
        // 信件資訊
        private MimeMessage _message;
        // 信件內容 預設純文字內容
        private TextPart _content;
        // 寄送方資訊
        private MailboxAddress _form;
        // 接收方資訊
        private MailboxAddress _to;

        public MailSend()
        {
            _message = new MimeMessage();
            _content = new TextPart("plain");
            _form = new MailboxAddress("", "");
            _to = new MailboxAddress("", "");
            SMTPServer = @"smtp.gmail.com";
            SMTPServerPort = 465;
            SMTPUseSSL = true;
        }

        /// <summary>
        /// 寄送方姓名
        /// </summary>
        public string MailSendFromName {
            get {
                return _form.Name;
            }

            set {
                _form.Name = value;
            }
        }

        /// <summary>
        /// 寄送方mail地址
        /// </summary>
        public string MailSendFromAddress
        {
            get {
                return _form.Address;
            }

            set {
                _form.Address = value;
            }
        }

        /// <summary>
        /// 接收方姓名
        /// </summary>
        public string MailSendToName
        {
            get
            {
                return _to.Name;
            }

            set
            {
                _to.Name = value;
            }
        }

        /// <summary>
        /// 接收方mail地址
        /// </summary>
        public string MailSendToAddress
        {
            get
            {
                return _to.Address;
            }

            set
            {
                _to.Address = value;
            }
        }

        /// <summary>
        /// 設定信件主旨
        /// </summary>
        public string MailTitle {
            get {
                return _message.Subject;
            }

            set {
                _message.Subject = value;
            }
        }

        /// <summary>
        /// 設定信件內容
        /// </summary>
        public string MailContent {
            get {
                return ((TextPart)_message.Body).Text;
            }

            set {
                _content.Text = value;
                _message.Body = _content;
            }
        }

        /// <summary>
        /// 設定寄送方帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 設定寄送方密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 設定SMTP Server 預設Gmail
        /// </summary>
        public string SMTPServer { get; set; }

        /// <summary>
        /// 設定SMTP Server Port 詳細請看 G-mail https://ppt.cc/f1EHGx
        /// </summary>
        public int SMTPServerPort { get; set; }

        /// <summary>
        /// 設定SMTP Server 是否啟用SSL
        /// </summary>
        public Boolean SMTPUseSSL { get; set; }

        /// <summary>
        /// 設定讀取文字檔路徑
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 寄送信件
        /// </summary>
        public void SendMaill()
        {
            using (SmtpClient client = new SmtpClient())
            {
                _message.From.Add(_form);
                _message.To.Add(_to);
                client.Connect(SMTPServer, SMTPServerPort, SMTPUseSSL);
                client.Authenticate(Account, Password);
                client.Send(_message);
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 從檔案取得帳號密碼
        /// </summary>
        public void FromFileAccountInfo()
        {
            // 路徑為任意位置
            // 檔案請準備以下格式的文字檔:
            /*
             your account(換行)
             your password
             */
            string accountInfo = "";
            string[] info;

            using (StreamReader reader = File.OpenText(Path))
            {
                accountInfo = reader.ReadToEnd();
                info = accountInfo.Split('\n');
                Account = info[0].Replace("\r", "");
                Password = info[1];
                MailSendFromAddress = Account;
            }
        }
    }
}